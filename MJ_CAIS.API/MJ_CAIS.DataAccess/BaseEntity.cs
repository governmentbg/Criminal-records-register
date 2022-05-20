using MJ_CAIS.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MJ_CAIS.DataAccess
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            this.EntityState = EntityStateEnum.Unchanged;
        }

        public BaseEntity(string primaryKeyName) : this()
        {
            this.PrimaryKeyName = primaryKeyName;
        }

        //public string Id { get; set; } = null!;

        [ConcurrencyCheck]
        public decimal? Version { get; set; }

        [NotMapped]
        public EntityStateEnum EntityState { get; set; }

        [NotMapped]
        public List<string> ModifiedProperties { get; set; }

        [NotMapped]
        public string PrimaryKeyName { get; private set; }

        public static string GenerateNewId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}