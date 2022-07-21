namespace MJ_CAIS.ExternalWebServices.Schemas.PersonValidator
{
    public class PersonInfoRequest
    {
        public string year { get; set; }
        public string month { get; set; }
        public string day { get; set; }
        public PersonInfoGenderType gender { get; set; }
        public string fname { get; set; }
        public string? sname { get; set; }
        public string? lname { get; set; }
        public string threshold { get; set; }

        public string fullname { get { return fname + " " + sname + " " + lname; } }
        public Dictionary<string, string?> GetKeyValuePairs()
        {
            var properties = this.GetType().GetProperties();
            var result = new Dictionary<string, string?>();

            foreach (var property in properties)
            {
                var value = property.GetValue(this)?.ToString();
                if (value == null)
                {
                    result.Add(property.Name, "");
                }
                else
                {
                    result.Add(property.Name, value);
                }

            }

            return result;
        }
    }

    public enum PersonInfoGenderType
    {
        male,
        female,
    }
}
