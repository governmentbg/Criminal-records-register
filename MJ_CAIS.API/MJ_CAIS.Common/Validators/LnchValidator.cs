namespace MJ_CAIS.Common.Validators
{
    public static class LnchValidator
    {
        public static bool IsValid(string personalForeignNumber)
        {
            if (string.IsNullOrEmpty(personalForeignNumber)) return true;
            if (personalForeignNumber.Length != 10) return false;

            foreach (char digit in personalForeignNumber)
            {
                if (!char.IsDigit(digit))
                {
                    return false;
                }
            }

            int[] weights = new int[] { 21, 19, 17, 13, 11, 9, 7, 3, 1 };
            int totalControlSum = 0;

            for (int i = 0; i < 9; i++)
            {
                totalControlSum += weights[i] * (personalForeignNumber[i] - '0');
            }

            int controlDigit = totalControlSum % 10;

            int lastDigitFromIDNumber = int.Parse(personalForeignNumber.Substring(9));
            if (lastDigitFromIDNumber != controlDigit)
            {
                return false;
            }

            return true;
        }
    }
}
