namespace MJ_CAIS.Services.Contracts.Utils
{
    public class IgPageResult<T>
    {
        public int Total { get; set; }

        public int PerPage { get; set; }

        public int CurrentPage { get; set; }

        public int LastPage { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}
