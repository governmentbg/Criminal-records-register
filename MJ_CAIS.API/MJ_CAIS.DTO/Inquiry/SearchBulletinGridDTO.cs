﻿namespace MJ_CAIS.DTO.Inquiry
{
    public class SearchBulletinGridDTO
    {
        #region Search properties

        public string? RegistrationNumber { get; set; }
        public string? BulletinType { get; set; }
        public DateTime? BulletinReceivedDate { get; set; }
        public string? CaseTypeId { get; set; }
        public string? CaseNumber { get; set; }
        public decimal? CaseYear { get; set; }
        public string? DecidingAuthId { get; set; }
        public string? DecisionNumber { get; set; }
        public DateTime? DecisionDate { get; set; }
        public DateTime? DecisionFinalDate { get; set; }
        public string? DecisionTypeId { get; set; }
        public string? StatusId { get; set; }
        public string? OffenceCatId { get; set; }
        public string? SanctCategoryId { get; set; }
        public decimal? FineAmount { get; set; }

        #endregion

        public string? FirstName { get; set; }
        public string? SurName { get; set; }
        public string? FamilyName { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
