namespace MJ_CAIS.DTO.Person
{
    public class PersonIdTypeDTO
    {
        public PersonIdTypeDTO()
        {

        }

        public PersonIdTypeDTO(string? pid, string? type, string? issuer)
        {
            this.Pid = pid;
            this.Type = type;
            this.Issuer = issuer;
        }

        public string? Pid { get; set; }
        public string? Type { get; set; }
        public string? Issuer { get; set; }
    }
}
