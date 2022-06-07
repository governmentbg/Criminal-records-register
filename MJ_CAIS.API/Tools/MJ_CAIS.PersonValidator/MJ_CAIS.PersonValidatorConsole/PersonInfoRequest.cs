namespace MJ_CAIS.PersonValidator
{
    public class PersonInfoRequest
    {
        public string year { get; set; }
        public string month { get; set; }
        public string day { get; set; }
        public string gender { get; set; }
        public string fname { get; set; }
        public string sname { get; set; }
        public string lname { get; set; }
        public string threshold { get; set; }

        public Dictionary<string, string?> GetKeyValuePairs()
        {
            var properties = this.GetType().GetProperties();
            var result = new Dictionary<string, string?>();

            foreach (var property in properties)
            {
                var value = property.GetValue(this)?.ToString();
                result.Add(property.Name, value);
            }

            return result;
        }
    }

    public enum GenderType
    {
        male,
        female,
    }
}
