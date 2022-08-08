

namespace MJ_CAIS.DTO.AbstractMessageType
{
    public class EcrisNotificationDTO
    {
        public string Id { get; set; }
        public string SendingMemberState { get; set; }
        public string ReceivingMemberState { get; set; }

        #region Актуализирани присъди
        public string EcrisId { get; set; }
        #endregion

        #region Основни данни за лицето
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string LastNameSecond { get; set; }
        public string FullName { get; set; }
        public string Nationality { get; set; }
        public string Birthday { get; set; }
        public string CountryPerson { get; set; }
        public string MunicipalityPerson { get; set; }
        public string CityPerson { get; set; }
        public string PersonId { get; set; }
        public string Sex { get; set; }
        #endregion

        #region Информация за родителите
        public string FatherName { get; set; }
        public string FatherFamilyName { get; set; }
        public string FatherFamilyNameSecond { get; set; }
        public string MotherName { get; set; }
        public string MotherFamilyName { get; set; }
        public string MotherFamilyNameSecond { get; set; }
        #endregion

        #region Информация за адреса
        public string Country { get; set; }
        public string Municipality { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public string FullAdress { get; set; }
        public string AdressNumber { get; set; }
        #endregion

        #region Информация за осъждане
        public string ConvictionID { get; set; }
        public string ConvictionCountry { get; set; }
        public string ConvictionDecidingAuthorityName { get; set; }
        public string ConvictionDecisionDate { get; set; }
        public string ConvictionFileNumber { get; set; }
        public string ConvictionNonCriminalRuling { get; set; }
        public string ConvictionRemarks { get; set; }

        //Код на съдебния орган:ADD
        public string ConvictionDecisionFinalDate { get; set; }

        //Задържане - Крайна дата: 
        public string ConvictionIsTransmittable { get; set; }
        #endregion

        #region Санкция
        public ConvictionSanction[] ConvictionSanctions { get; set; }
        #endregion

        #region Решения
        public EcrisNotificationDecision[] Decisions { get; set; }
        #endregion
        //#region Присъди
        //public EcrisNotificationDecision[] Decisions { get; set; }
        //#endregion

    }



}
