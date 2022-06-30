﻿namespace MJ_CAIS.DTO.Inquiry
{
    public class InquiryBulletinGridDTO : BaseGridDTO
    {
        public string? RegistrationNumber { get; set; }
        public string? BulletinType { get; set; }
        public string? FirstName { get; set; }
        public string? SurName { get; set; }
        public string? FamilyName { get; set; }
        public string? StatusId { get; set; }
        public string? StatusName { get; set; }
        public string? Ln { get; set; }
        public string? Lnch { get; set; }
        public string? Egn { get; set; }
    }
}