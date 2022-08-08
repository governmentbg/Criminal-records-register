

namespace MJ_CAIS.DTO.AbstractMessageType
{
    public class ConvictionSanction
    {
        public string CommonCategory { get; set; }
        public string Alternative { get; set; }
        public string NationalCategoryTitle { get; set; }
        public string ConvictionStartDate { get; set; }
        public string ConvictionEndDate { get; set; }
        public string ConvictionDuration { get; set; }

        //Колко пъти санкцията е приложена:
        //Изпълнение - Начална дата:
        //Изпълнение - Крайна дата:
        //Изпълнение - Продължителност

        public string SanctionAmountOfIndividualFine { get; set; }
        public string Remarks { get; set; }

        //Тип:
        //Национален код:
        //Присъда - Точна продължителност:
        public string SanctionIsSpecificToMinor { get; set; }
        public string SanctionNumberOfFines { get; set; }
        public string SanctionCurrencyOfFine { get; set; }
    }
}
