using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        private string? _currentUserId = null;

        public CaisDbContext(DbContextOptions<CaisDbContext> options, IHttpContextAccessor httpContextAccessor) 
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? CurrentUserId
        {
            get
            {
                if (_currentUserId == null)
                {
                    var identity = _httpContextAccessor.HttpContext?.User?.Identity;
                    if (identity != null)
                    {
                        var claims = _httpContextAccessor.HttpContext?.User?.Claims;
                        var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                        if (claimId == null || string.IsNullOrEmpty(claimId.Value))
                        {
                            _currentUserId = null;
                        }
                        else
                        {
                            _currentUserId = claimId.Value;
                        }
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
            this.SimpleAudit();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.SimpleAudit();
            return base.SaveChangesAsync(cancellationToken);
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
                    entity.Property("CreatedOn").CurrentValue = DateTime.UtcNow;
                }
            }

            //Add audit info for update statements
            foreach (var entity in this.ChangeTracker.Entries()
                     .Where(t => t.State == EntityState.Modified))
            {
                if (entity.Entity.GetType().GetProperty("UpdatedBy") != null)
                {
                    entity.Property("UpdatedBy").CurrentValue = CurrentUserId;
                    entity.Property("UpdatedOn").CurrentValue = DateTime.UtcNow;
                }
            }
        }
    }
}
