using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBProject
{
    public partial class Person 
    {
        
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public string? Fullname { get; set; }
        public decimal? Sex { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthPlaceOther { get; set; }
       
        
        public string? MotherFullname { get; set; }
        
        public string? FatherFullname { get; set; }
        public string? BirthCityId { get; set; }
        public string? BirthCountryId { get; set; }

        public string? Egn { get; set; }

        public string? Lnch { get; set; }

        public string? NationalityCode1 { get; set; }
        public string? NationalityCode2 { get; set; }
        public string? NationalityCode3 { get; set; }
        public string? NationalityCode4 { get; set; }
        public string? NationalityCode5 { get; set; }


        public string? HasError { get; set; }
        public string? ErrorMessage { get; set; }


    }
}
