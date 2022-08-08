

namespace MJ_CAIS.DTO.AbstractMessageType
{
    public class EcrisResponseDTO
    {
        public string Id { get; set; }

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

    }



}
