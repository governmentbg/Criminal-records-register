namespace AutomaticStepsExecutor
{
    public class AutomaticStepResult
    {
        public int NumberOfProcessedEntities { get; set; }
        public int NumberOfFailedEntities { get; set; }
        public int NumberOfSuccessfulEntities { get; set; }


        public string? GetLogInfo()
        {
            string result = "";
            foreach(var property in this.GetType().GetProperties())
            {
                result += property.Name;
                result += " = ";
                result += property.GetValue(this).ToString();
                result += ";";

            }
            return result;
        }
    }
}