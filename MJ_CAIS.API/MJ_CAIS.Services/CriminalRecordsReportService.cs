using AutoMapper;
using EO.Pdf;
using MJ_CAIS.DTO.ExternalServicesHost;
using MJ_CAIS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using TL.Signer;

namespace MJ_CAIS.Services
{
    public class CriminalRecordsReportService : ICriminalRecordsReportService
    {
        private readonly IPdfSigner _pdfSignerService;

        public CriminalRecordsReportService(IMapper mapper, IPdfSigner pdfSignerService)
        {
            _pdfSignerService = pdfSignerService;
        }

        public CriminalRecordsReportType GetCriminalRecordsReport(CriminalRecordsExtendedRequestType value)
        {
            return new CriminalRecordsReportType()
            {
                ReportCriteria = value.CriminalRecordsRequest,
                ReportDate = DateTime.UtcNow,
                ReportResult = new ReportResultType()
                {
                    PersonData = new CriminalRecordsPersonDataType()
                    {
                        AFISNumber = "",
                        BirthDate = new DateType()
                        {
                            DateMonthDay = new MonthDayType()
                            {
                                DateDay = "1",
                                DateMonth = "9"
                            },
                            DateYear = "1983"
                        },
                        BirthPlace = new PlaceType()
                        {
                            City = new CityType()
                            {
                                CityName = "София",
                                EKATTECode  = "68134"
                            },
                            Country = new CountryType()
                            {
                                CountryName = "България"
                            }

                        },
                        
                    },
                    BulletinsList = new BulletinsList()
                    {
                        Bulletin = new BulletinType[]
                        {
                            new BulletinType()
                            {
                                
                            }
                        }
                    }
                }
            };
        }

        public CriminalRecordsPDFResult GetCriminalRecordsReportPDF(CriminalRecordsExtendedRequestType value)
        {
            var response = GetCriminalRecordsReport(value);
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
