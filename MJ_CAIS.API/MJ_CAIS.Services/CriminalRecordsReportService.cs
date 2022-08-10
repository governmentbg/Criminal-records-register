using AutoMapper;
using EO.Pdf;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ExternalServicesHost;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using TL.Signer;

namespace MJ_CAIS.Services
{
    public class CriminalRecordsReportService : ICriminalRecordsReportService
    {
        private readonly IPdfSigner _pdfSignerService;
        private readonly IBulletinRepository _bulletinRepository;
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;

        public CriminalRecordsReportService(IMapper mapper, IPdfSigner pdfSignerService, IBulletinRepository bulletinRepository, IPersonRepository personRepository)
        {
            _mapper = mapper;
            _pdfSignerService = pdfSignerService;
            _bulletinRepository = bulletinRepository;
            _personRepository = personRepository;
        }

        public async Task<CriminalRecordsReportType> GetCriminalRecordsReportAsync(CriminalRecordsExtendedRequestType value)
        {
            var pidTypeCode = value.CriminalRecordsRequest.IdentifierType == IdentifierType.SUID ?
                        "SYS" :
                        value.CriminalRecordsRequest.IdentifierType.ToString();
            var personDb =
                await _bulletinRepository.GetPersonIdByPidAsync(
                    value.CriminalRecordsRequest.PID,
                    pidTypeCode
                );
            var person = _mapper.Map<CriminalRecordsPersonDataType>(personDb);
            var pid = value.CriminalRecordsRequest.PID;
            var pidId = personDb.PPersonIds.FirstOrDefault(x => x.Pid == pid && x.PidType.Code == pidTypeCode)?.Id;
            var bulletins = await _bulletinRepository.GetBulletinsByPidIdAsync(pidId);
            var bulletinsList = await bulletins.ToListAsync();

            var bullArray = new BulletinType[bulletinsList.Count];

            for (int i = 0; i < bulletinsList.Count; i++)
            {
                bullArray[i] = _mapper.Map<BulletinType>(bulletinsList[i]);
            }

            var result = new CriminalRecordsReportType()
            {
                ReportCriteria = value.CriminalRecordsRequest,
                ReportDate = DateTime.Now,
                ReportResult = new ReportResultType
                {
                    PersonData = person,
                    BulletinsList = new BulletinsList
                    {
                        Bulletin = bullArray,
                    }
                }
            };

            return result;
        }

        public async Task<CriminalRecordsPDFResult> GetCriminalRecordsReportPDFAsync(CriminalRecordsExtendedRequestType value)
        {
            var response = await GetCriminalRecordsReportAsync(value);
            var xslt = GetTransformation();
            var html = ApplyTransformation(XmlSerialize(response), xslt);
            var pdf = ConvertToPDFAndSign(html, "cais.mjs.bg");
            return new CriminalRecordsPDFResult()
            {
                HasError = false,
                ResultData = pdf
            };
        }

        private string GetTransformation()
        {
            return File.ReadAllText(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + @"ExternalServicesHost\ESCSC.xslt");
        }

        public async Task<PersonIdentifierSearchResponseType> PersonIdentifierSearchAsync(PersonIdentifierSearchExtendedRequestType value)
        {
            //var dbContext = _personRepository.GetDbContext();

            var firstname = value.PersonIdentifierSearchRequest.Firstame?.ToUpper();
            var surname = value.PersonIdentifierSearchRequest.Surname?.ToUpper();
            var familyname = value.PersonIdentifierSearchRequest.Familyname?.ToUpper();
            var birthCountry = value.PersonIdentifierSearchRequest.BirthCountry?.ToUpper();
            var birthdate = value.PersonIdentifierSearchRequest.Birthdate;
            var birthDatePrec = value.PersonIdentifierSearchRequest.BirthDatePrec;
            var birthplace = value.PersonIdentifierSearchRequest.Birthplace?.ToUpper();
            var fullname = value.PersonIdentifierSearchRequest.Fullname?.ToUpper();
            var birthdateFrom = new DateTime(birthdate.Year, birthdate.Month, 1);
            var birthdateTo = birthdateFrom.AddMonths(1).AddDays(-1);
            var birthdateYear = birthdate.Year;

            IQueryable<string> personIds = _personRepository.GetPersonIDsByPersonData(firstname, surname, familyname, birthCountry, birthdate, birthDatePrec, birthplace, fullname, birthdateFrom, birthdateTo, birthdateYear);

            List<PPerson> res = await _personRepository.GetPersonByID(personIds);

            var result = _mapper.Map<List<PPerson>, PersonIdentifierSearchResponseType>(res);

            result.ReportCriteria = value.PersonIdentifierSearchRequest;
            result.ReportDate = DateTime.Now;
            return result;
        }



        public static string ApplyTransformation(string xmlString, string xslt)
        {
            XslCompiledTransform transformation = new XslCompiledTransform();
            XmlReader xmlReader = XmlReader.Create(new StringReader(xslt));

            transformation.Load(xmlReader);

            XmlReader serviceResultXmlReader = new XmlTextReader(new StringReader(xmlString));
            Stream transformedHtmlStream = new MemoryStream();
            transformation.Transform(serviceResultXmlReader, new XsltArgumentList(), transformedHtmlStream);
            transformedHtmlStream.Position = 0;
            serviceResultXmlReader.Close();

            StreamReader reader = new StreamReader(transformedHtmlStream);
            string transformedResult = reader.ReadToEnd();

            return transformedResult;
        }

        public static string XmlSerialize(object obj)
        {
            if (obj != null)
            {
                using (MemoryStream ms = new MemoryStream())
                using (StreamReader sr = new StreamReader(ms))
                {
                    XmlSerializer serializer = new XmlSerializer(obj.GetType());
                    serializer.Serialize(ms, obj);
                    ms.Seek(0, SeekOrigin.Begin);
                    return sr.ReadToEnd();
                }
            }
            else
            {
                return string.Empty;
            }
        }

        public byte[] ConvertToPDFAndSign(string html, string signingCertificateName)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                HtmlToPdf.ConvertHtml(html, ms);
                ms.Position = 0;

                var signedPDF = _pdfSignerService.SignPdf(ms.ToArray(), signingCertificateName);
                return signedPDF;
            }
        }

        public async Task<CriminalRecordsForPeriodResponseType> GetCriminalRecordsReportForPeriodAsync(CriminalRecordsForPeriodRequestType value)
        {
            var bulletins = await _bulletinRepository.GetBulletinsForPeriodAsync(value.ValidFrom, value.ValidTo);
            var bulletinsList = await bulletins.ToListAsync();

            var bullArray = new BulletinType[bulletinsList.Count];

            for (int i = 0; i < bulletinsList.Count; i++)
            {
                bullArray[i] = _mapper.Map<BulletinType>(bulletinsList[i]);
            }

            var result = new CriminalRecordsForPeriodResponseType()
            {
                ReportCriteria = value,
                ReportDate = DateTime.Now,
                ReportResult = new BulletinsList
                {
                    Bulletin = bullArray
                }
            };

            return result;
        }
    }
}
