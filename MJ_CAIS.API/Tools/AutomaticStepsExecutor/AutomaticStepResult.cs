namespace AutomaticStepsExecutor
{
    public class AutomaticStepResult
    {
        int NumberOfProcessedEntities { get; set; }
        int NumberOfFailedEntities { get; set; }
        int NumberOfSuccessfulEntities { get; set; }


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
            return "";
        }
    }
}