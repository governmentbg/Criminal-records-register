using AutoMapper;
using EO.Pdf;
using Microsoft.EntityFrameworkCore;
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

        public CriminalRecordsReportService(IMapper mapper, IPdfSigner pdfSignerService, IBulletinRepository bulletinRepository)
        {
            _mapper = mapper;
            _pdfSignerService = pdfSignerService;
            _bulletinRepository = bulletinRepository;
        }

        public async Task<CriminalRecordsReportType> GetCriminalRecordsReportAsync(CriminalRecordsExtendedRequestType value)
        {
            var personDb = await _bulletinRepository.GetPersonIdByPidAsync(value.CriminalRecordsRequest.PID, value.CriminalRecordsRequest.IdentifierType.ToString());
            var person = _mapper.Map<CriminalRecordsPersonDataType>(personDb);

            var pidId = personDb.PPersonIds.FirstOrDefault(x => x.Pid == value.CriminalRecordsRequest.PID && x.PidType.Code == value.CriminalRecordsRequest.IdentifierType.ToString())?.Id;
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
                ReportDate = DateTime.UtcNow,
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

        public PersonIdentifierSearchResponseType PersonIdentifierSearch(PersonIdentifierSearchExtendedRequestType value)
        {
            throw new NotImplementedException();
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
    }
}
