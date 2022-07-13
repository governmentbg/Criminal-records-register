namespace MJ_CAIS.Common.Validators
{
    public static class EgnValidator
    {
        private static readonly byte[] Egn = new byte[10];
        private static readonly byte[] Weights = new byte[9] { 2, 4, 8, 5, 10, 9, 7, 3, 6 };

        public static bool IsValid(string personalIdNumber)
        {
            if (string.IsNullOrEmpty(personalIdNumber)) return true;
            if (personalIdNumber.Length != 10) return false;

            foreach (char digit in personalIdNumber)
            {
                if (!char.IsDigit(digit)) return false;
            }

            ulong egn = Convert.ToUInt64(personalIdNumber);
            if (egn < 9952319999)
            {

                for (int i = 9; i >= 0; i--)
                {
                    Egn[i] = (byte)(egn % 10);
                    egn /= 10;
                }
            }

            DateTime testDate;
            try
            {
                testDate = new DateTime(GetYear(), GetMonth(), GetDay());
            }
            catch
            {
                return false;
            }

            if (testDate > DateTime.Now) return false;

            int checksum = 0;
            for (int i = 0; i < 9; i++)
            {
                checksum += Egn[i] * Weights[i];
            }

            int remainder = checksum % 11;
            if (remainder == 10)
            {
                remainder = 0;
            }

            if (remainder != Egn[9])
            {
                return false;
            }

            return true;
        }

        private static int GetYear()
        {
            int year = Egn[0] * 10 + Egn[1];
            int month = Egn[2] * 10 + Egn[3];
            if (month > 40)
            {
                return year += 2000;
            }

            if (month > 20)
            {
                return year += 1800;
            }

            return year += 1900;
        }

        private static int GetMonth()
        {
            int month = Egn[2] * 10 + Egn[3];
            if (month > 40)
            {
                return month -= 40;
            }

            if (month > 20)
            {
                return month -= 20;
            }

            return month;
        }

        private static int GetDay() => Egn[4] * 10 + Egn[5];
    }
}
