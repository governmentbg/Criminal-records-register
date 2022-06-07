namespace MJ_CAIS.PersonValidator
{
    public class PersonInfoResponse
    {
        public PersonData[] personData { get; set; }

        public PersonInfoResponse(PersonData[] personData)
        {
            this.personData = personData;
        }
    }

    public class PersonData
    {
        public Person person { get; set; }
        public Score[] scores { get; set; }
    }

    public class Person
    {
        public Name name { get; set; }
        public Mother mother { get; set; }
        public Father father { get; set; }
        public string personalNumber { get; set; }
        public string gender { get; set; }
        public Birthdate birthDate { get; set; }
        public Birthplace birthPlace { get; set; }
    }

    public class Name
    {
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string familyName { get; set; }
    }

    public class Mother
    {
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string familyName { get; set; }
    }

    public class Father
    {
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string familyName { get; set; }
    }

    public class Birthdate
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
    }

    public class Birthplace
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class Score
    {
        public string soundhash { get; set; }
        public string transliterate { get; set; }
        public float score { get; set; }
    }
}
