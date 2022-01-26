namespace MJ_CAIS.DTO.Common
{
    public class TransactionDTO<T> where T : class
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public T NewValue { get; set; }
    }
}
