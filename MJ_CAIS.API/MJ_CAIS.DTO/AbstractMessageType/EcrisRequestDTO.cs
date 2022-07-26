

namespace MJ_CAIS.DTO.AbstractMessageType
{
    public class EcrisRequestDTO
    {
        public string Id { get; set; }
        public string EcrisId { get; set; }
        public string SendingMemberState { get; set; }
        public string ReceivingMemberState { get; set; }

        #region Запитващ орган
        public string RequestAuthorityName { get; set; }
        public string RequestAuthorityType { get; set; }
        public string RequestAuthorityCode { get; set; }
        #endregion

        #region Основни данни за лицето
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string LastNameSecond { get; set; }
        public string FullName { get; set; }
        public string Nationality { get; set; }
        public string CountryPerson { get; set; }
        public string MunicipalityPerson { get; set; }
        public string CityPerson { get; set; }
        public string PersonId { get; set; }
        public string Sex { get; set; }
        #endregion

        #region Предишни имена
        public string FirstNameFormer { get; set; }
        public string MiddleNameFormer { get; set; }
        public string LastNameFormer { get; set; }
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

        #region Цел
        public string RequestPurposeCategory { get; set; }
        public string RequestPurpose { get; set; }
        public string ConcernedPеrsonConsent { get; set; }
        public string MessageUrgency { get; set; }
        #endregion

        #region MyRegion
        public string AccusationOffenceCategory { get; set; }
        public string MessageAccusation { get; set; }
        public string CaseRefereranceNumber { get; set; }
        #endregion

    }



}
