namespace MJ_CAIS.DTO.Common
{
    public class MultipleChooseDTO<TPrimaryKey, TForeignKey>
    {
        public IEnumerable<TPrimaryKey> SelectedPrimaryKeys { get; set; } = new List<TPrimaryKey>();

        public IEnumerable<TForeignKey> SelectedForeignKeys { get; set; } = new List<TForeignKey> ();

        public bool IsChanged { get; set; }

        public MultipleChooseDTO(IEnumerable<TPrimaryKey> primaryKeys, IEnumerable<TForeignKey> foreignKeys)
        {
            this.SelectedPrimaryKeys = primaryKeys;
            this.SelectedForeignKeys = foreignKeys;
        }

        public MultipleChooseDTO()
        {

        }
    }

    public class MultipleChooseNumberDTO : MultipleChooseDTO<decimal, decimal>
    {
        public MultipleChooseNumberDTO(IEnumerable<decimal> primaryKeys, IEnumerable<decimal> foreignKeys)
            : base(primaryKeys, foreignKeys)
        {
        }

        public MultipleChooseNumberDTO() : base()
        {

        }
    }

    public class MultipleChooseDTO : MultipleChooseDTO<string, string>
    {
        public MultipleChooseDTO(IEnumerable<string> primaryKeys, IEnumerable<string> foreignKeys)
            : base(primaryKeys, foreignKeys)
        {
        }

        public MultipleChooseDTO() : base()
        {

        }
    }
}
