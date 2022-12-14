using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.DataAccess
{
    public partial class CaisDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserContext _userContext;
        private string? _currentUserId = null;

        public CaisDbContext(DbContextOptions<CaisDbContext> options, IHttpContextAccessor httpContextAccessor, IUserContext userContext)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _userContext = userContext;
        }

        public string? CurrentUserId
        {
            get
            {
                if (_currentUserId == null)
                {
                    var identity = _httpContextAccessor?.HttpContext?.User?.Identity;
                    if (identity != null)
                    {
                        var claims = _httpContextAccessor.HttpContext?.User?.Claims;
                        var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                        if (claimId == null || string.IsNullOrEmpty(claimId.Value))
                        {
                            if (!string.IsNullOrEmpty(_userContext?.UserId))
                            {
                                _currentUserId = _userContext.UserId;
                            }
                            else
                            {
                                _currentUserId = null;
                            }
                        }
                        else
                        {
                            _currentUserId = claimId.Value;
                        }
                    }
                    else if (!string.IsNullOrEmpty(_userContext?.UserId))
                    {
                        _currentUserId = _userContext.UserId;
                    }
                    else
                    {
                        _currentUserId = null;
                    }
                }

                return _currentUserId;
            }
        }

        /// <summary>
        /// Override the SaveChanges method to automatically add audit information
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            IDbContextTransaction dbTransaction = null;
            try
            {
                dbTransaction = this.Database.BeginTransaction();

                var trackedEntities = this.ChangeTracker.Entries()
                    .Where(t => t.State == EntityState.Added || t.State == EntityState.Modified)
                    .ToList();

                this.SimpleAudit();
                var result = base.SaveChanges();

                this.UpdateVersions(trackedEntities);
                base.SaveChanges();

                dbTransaction.Commit();
                //туй го добавям, щото става голямо объркване със State И EntityState.
                //След SaveChanges State=Unchanged, докато EntityState остава такъв, каквъто е бил преди SaveChanges().
                //В някои случаи това е ГОЛЯМ проблем
                //Трябва да се види, дали няма нужда и при Exception да се прави нещо с тези EntityState-ове
                foreach (var dbEntityEntry in this.ChangeTracker.Entries())
                {
                    if ((dbEntityEntry.Entity is BaseEntity) && ((BaseEntity)dbEntityEntry.Entity).EntityState != Common.Enums.EntityStateEnum.Unchanged)
                    {
                        ((BaseEntity)dbEntityEntry.Entity).EntityState = Common.Enums.EntityStateEnum.Unchanged;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                dbTransaction?.Rollback();
                throw;
            }
            finally
            {
                dbTransaction.Dispose();
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            IDbContextTransaction dbTransaction = null;
            try
            {
                dbTransaction = await this.Database.BeginTransactionAsync(cancellationToken);

                var trackedEntities = this.ChangeTracker.Entries()
                    .Where(t => t.State == EntityState.Added || t.State == EntityState.Modified)
                    .ToList();

                this.SimpleAudit();
                var result = await base.SaveChangesAsync(cancellationToken);

                this.UpdateVersions(trackedEntities);
                await base.SaveChangesAsync(cancellationToken);

                //туй го добавям, щото става голямо объркване със State И EntityState.
                //След SaveChanges State=Unchanged, докато EntityState остава такъв, каквъто е бил преди SaveChanges().
                //В някои случаи това е ГОЛЯМ проблем
                //Трябва да се види, дали няма нужда и при Exception да се прави нещо с тези EntityState-ове
                //todo: Надя , Може би this.ChangeTracker.Clear(); след извикването на SaveChanges ще свърши работа
                foreach (var dbEntityEntry in this.ChangeTracker.Entries())
                {
                    if ((dbEntityEntry.Entity is BaseEntity) && ((BaseEntity)dbEntityEntry.Entity).EntityState != Common.Enums.EntityStateEnum.Unchanged)
                    {
                        ((BaseEntity)dbEntityEntry.Entity).EntityState = Common.Enums.EntityStateEnum.Unchanged;
                    }
                }


                await dbTransaction.CommitAsync(cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync(cancellationToken);
                throw;
            }
            finally
            {
                await dbTransaction.DisposeAsync();
            }
        }

        public async Task<int> SaveChangesWithoutTransactionAsync(CancellationToken cancellationToken = default)
        {
            var trackedEntities = this.ChangeTracker.Entries()
                .Where(t => t.State == EntityState.Added || t.State == EntityState.Modified)
                .ToList();

            this.SimpleAudit();
            var result = await base.SaveChangesAsync(cancellationToken);

            this.UpdateVersions(trackedEntities);
            await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        private void SimpleAudit()
        {
            // Add audit info for create statements
            foreach (var entity in this.ChangeTracker.Entries()
                     .Where(t => t.State == EntityState.Added))
            {
                if (entity.Entity.GetType().GetProperty("CreatedBy") != null)
                {
                    entity.Property("CreatedBy").CurrentValue = CurrentUserId;
                    entity.Property("CreatedOn").CurrentValue = DateTime.Now;
                }
            }

            //Add audit info for update statements
            foreach (var entity in this.ChangeTracker.Entries()
                     .Where(t => t.State == EntityState.Modified))
            {
                if (entity.Entity.GetType().GetProperty("UpdatedBy") != null)
                {
                    entity.Property("UpdatedBy").CurrentValue = CurrentUserId;
                    entity.Property("UpdatedOn").CurrentValue = DateTime.Now;
                }
            }
        }

        private void UpdateVersions(List<EntityEntry> trackedEntities)
        {
            foreach (var entity in trackedEntities)
            {
                if (entity.Entity.GetType().GetProperty("Version") != null)
                {
                    var version = entity.Property("Version").CurrentValue;
                    decimal nextVersion = version == null ? 1 : Convert.ToDecimal(version) + 1;

                    entity.Property("Version").CurrentValue = nextVersion;
                }
            }
        }
    }
}
