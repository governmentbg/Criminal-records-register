namespace MJ_CAIS.DTO.Common
{
    public class LookupDTO
    {
        public string? Id { get; set; }

        public string? DisplayName { get; set; }

        public LookupDTO()
        {
        }

        public LookupDTO(string? id, string? displayName)
        {
            this.Id = id;
            this.DisplayName = displayName;
        }
    }
}
