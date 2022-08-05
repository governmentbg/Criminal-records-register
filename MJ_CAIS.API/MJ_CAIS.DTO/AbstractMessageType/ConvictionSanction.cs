

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

        //����� ���� ��������� � ���������:
        //���������� - ������� ����:
        //���������� - ������ ����:
        //���������� - ���������������

        public string SanctionAmountOfIndividualFine { get; set; }
        public string Remarks { get; set; }

        //���:
        //���������� ���:
        //������� - ����� ���������������:
        public string SanctionIsSpecificToMinor { get; set; }
        public string SanctionNumberOfFines { get; set; }
        public string SanctionCurrencyOfFine { get; set; }
    }
}
