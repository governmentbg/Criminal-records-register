using MJ_CAIS.Common.Enums;
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

        public string Id { get; set; }

        //[Comment("Потребител който е insert-нал записа")]
        //[CustomStringLength(MaxLengthEnum.Name)]
        //public string SysInsUsername { get; set; }

        //[Comment("Дата на insert на записа")]
        //public DateTime? SysInsDate { get; set; }

        //[Comment("Потребител който е update-нал записа")]
        //[CustomStringLength(MaxLengthEnum.Name)]
        //public string SysUpdUsername { get; set; }

        //[Comment("Дата на update на записа")]
        //public DateTime? SysUpdDate { get; set; }

        [NotMapped]
        public EntityStateEnum EntityState { get; set; }

        [NotMapped]
        public List<string> ModifiedProperties { get; set; }

        [NotMapped]
        public string PrimaryKeyName { get; private set; }
    }
}