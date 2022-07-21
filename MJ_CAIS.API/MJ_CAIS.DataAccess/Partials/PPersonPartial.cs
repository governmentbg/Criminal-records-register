namespace MJ_CAIS.DataAccess.Entities
{
    public partial class PPerson
    {
        public override bool Equals(object? obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType())) return false;

            var personToCompare = (PPerson)obj;

            if (this.Firstname != personToCompare.Firstname) return false;
            if (this.Surname != personToCompare.Surname) return false;
            if (this.Familyname != personToCompare.Familyname) return false;
            if (this.Fullname != personToCompare.Fullname) return false;
            if (this.Sex != personToCompare.Sex) return false;
            if (this.BirthDate != personToCompare.BirthDate) return false;
            if (this.BirthDatePrec != personToCompare.BirthDatePrec) return false;
            if (this.BirthPlaceOther != personToCompare.BirthPlaceOther) return false;
            if (this.MotherFirstname != personToCompare.MotherFirstname) return false;
            if (this.FatherSurname != personToCompare.FatherSurname) return false;
            if (this.MotherSurname != personToCompare.MotherSurname) return false;
            if (this.MotherFamilyname != personToCompare.MotherFamilyname) return false;
            if (this.MotherFullname != personToCompare.MotherFullname) return false;
            if (this.FatherFirstname != personToCompare.FatherFirstname) return false;
            if (this.FatherFamilyname != personToCompare.FatherFamilyname) return false;
            if (this.FatherFullname != personToCompare.FatherFullname) return false;
            if (this.BirthCityId != personToCompare.BirthCityId) return false;
            if (this.BirthCountryId != personToCompare.BirthCountryId) return false;
            if (this.FirstnameLat != personToCompare.FirstnameLat) return false;
            if (this.SurnameLat != personToCompare.SurnameLat) return false;
            if (this.FamilynameLat != personToCompare.FamilynameLat) return false;
            if (this.FullnameLat != personToCompare.FullnameLat) return false;

            return true;
        }
    }
}
