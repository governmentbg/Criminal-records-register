using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Entities;

namespace MJ_CAIS.DataAccess
{
    // TODO: Remove from here, only for sample usage
    public partial class CaisDbContext : DbContext
    {
        public DbSet<Bulletin> Bulletins { get; set; }
    }
}
