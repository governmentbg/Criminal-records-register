using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.Common.XmlData;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.AbstractMessageType;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.DTO.EcrisMessage;
using MJ_CAIS.DTO.EcrisService;
using MJ_CAIS.DTO.Fbbc;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;
using System.Text;
using System.Xml;

namespace MJ_CAIS.Services
{
    public class EcrisMessageService :
        BaseAsyncService<EcrisMessageDTO, EcrisMessageDTO, EcrisMessageGridDTO, EEcrisMessage, string, CaisDbContext>,
        IEcrisMessageService
    {
        private const string EcrisFbbcMessageType = FbbcConstants.MessageType.CodeECRIS;
        private readonly IBulletinRepository _bulletinRepository;
        private readonly CaisDbContext _dbContext;
        private readonly IDDocContentRepository _dDocContentRepository;
        private readonly IDDocumentRepository _dDocumentRepository;
        private readonly IEcrisMessageRepository _ecrisMessageRepository;
        private readonly IFbbcRepository _fbcRepository;
        private readonly INomenclatureDetailRepository _nomenclatureDetailRepository;


        public EcrisMessageService(IMapper mapper,
            IEcrisMessageRepository ecrisMessageRepository,
            IBulletinRepository bulletinRepository,
            IFbbcRepository fbbcRepository,
            INomenclatureDetailRepository nomenclatureDetailRepository,
            IDDocumentRepository dDocumentRepository,
            IDDocContentRepository dDocContentRepository,
            CaisDbContext dbContext)
            : base(mapper, ecrisMessageRepository)
        {
            _ecrisMessageRepository = ecrisMessageRepository;
            _dbContext = dbContext;
            _bulletinRepository = bulletinRepository;
            _fbcRepository = fbbcRepository;
            _nomenclatureDetailRepository = nomenclatureDetailRepository;
            _dDocumentRepository = dDocumentRepository;
            _dDocContentRepository = dDocContentRepository;
        }

        public virtual async Task<IgPageResult<EcrisMessageGridDTO>> SelectAllWithPaginationAsync(
            ODataQueryOptions<EcrisMessageGridDTO> aQueryOptions, string statusId)
        {
            var baseQuery = _ecrisMessageRepository.CustomGetAll().Where(x => x.EcrisMsgStatus == statusId);
            var resultQuery = await ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<EcrisMessageGridDTO>();
            PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }

        public async Task<IQueryable<BulletinGridDTO>> GetEcrisBulletinsByIdAsync(string ecrisMessageId)
        {
            var ecrisMessage = await SelectAsync(ecrisMessageId);
            if (ecrisMessage == null)
            {
                return new List<BulletinGridDTO>().AsQueryable();
            }

            var result = _bulletinRepository.SelectAll()
                .Where(x => x.Egn == ecrisMessage.Identifier)
                .ProjectTo<BulletinGridDTO>(mapperConfiguration);

            return result;
        }

        public Task ChangeStatusAsync(string aInDto, string statusId)
        {
            throw new NotImplementedException();
        }

        public async Task<EcrisRequestDTO> GetEcrisRequestByIdAsync(string ecrisMessageId)
        {
            var ecrisMessage = await _dDocumentRepository.SelectByEcrisIdAsync(ecrisMessageId);
            if (ecrisMessage == null)
            {
                throw new BusinessLogicException($"�� � ������� �������� � ID: {ecrisMessage}");
            }

            var doc = new XmlDocument();
            var xml = Encoding.UTF8.GetString(ecrisMessage.DocContent.Content);
            var msg = XmlUtils.DeserializeXml<AbstractMessageType>(
                "<?xml version=\"1.0\" encoding=\"utf-8\"?><AbstractMessageType xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:q1=\"http://ec.europa.eu/ECRIS-RI/domain-v1.0\" xsi:type=\"q1:NotificationMessageType\"><q1:MessageIdentifier>RI-NOT-000000001507915</q1:MessageIdentifier><q1:MessageEcrisIdentifier>PL-BG-NOT-000000000012055</q1:MessageEcrisIdentifier><q1:MessageSendingMemberState>PL</q1:MessageSendingMemberState><q1:MessageReceivingMemberState>BG</q1:MessageReceivingMemberState><q1:MessageType>NOT</q1:MessageType><q1:MessageVersionTimestamp>2022-05-06T16:22:15.724+03:00</q1:MessageVersionTimestamp><q1:MessagePerson><q1:PersonName><q1:Forename languageCode=\"pl\">ALEKO</q1:Forename><q1:Surname languageCode=\"pl\">BONEV</q1:Surname></q1:PersonName><q1:PersonSex>1</q1:PersonSex><q1:PersonBirthDate><DateYear xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">1985</DateYear><DateMonthDay xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\"><DateMonth>--12</DateMonth><DateDay>---03</DateDay></DateMonthDay></q1:PersonBirthDate><q1:PersonBirthPlace xsi:type=\"q1:PlaceType\"><q1:PlaceCountryReference>CO-00-100-BGR</q1:PlaceCountryReference><q1:PlaceTownReference>CI-00-BGR-T0006</q1:PlaceTownReference><q1:PlaceTownName><MultilingualTextLinguisticRepresentation languageCode=\"pl\" xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">STARA ZAGORA</MultilingualTextLinguisticRepresentation></q1:PlaceTownName></q1:PersonBirthPlace><q1:PersonNationalityReference>CO-00-100-BGR</q1:PersonNationalityReference><q1:PersonFatherForename languageCode=\"pl\">BESZKOV</q1:PersonFatherForename><q1:PersonMotherForename languageCode=\"pl\">DINKE</q1:PersonMotherForename><q1:PersonAddress><q1:AddressPlace><q1:PlaceCountryReference>CO-00-616-POL</q1:PlaceCountryReference><q1:PlaceCountrySubdivisionReference>CS-00-PL-MZ</q1:PlaceCountrySubdivisionReference><q1:PlaceTownReference>CI-00-POL-T0001</q1:PlaceTownReference><q1:PlaceTownName><MultilingualTextLinguisticRepresentation languageCode=\"pl\" xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">WARSZAWA</MultilingualTextLinguisticRepresentation></q1:PlaceTownName></q1:AddressPlace></q1:PersonAddress></q1:MessagePerson><q1:NotificationMessageConviction><q1:ConvictionID>PL-C-000000000066709</q1:ConvictionID><q1:ConvictionConvictingCountryReference>CO-00-616-POL</q1:ConvictionConvictingCountryReference><q1:ConvictionFileNumber>IIK101/16</q1:ConvictionFileNumber><q1:ConvictionDecisionDate>2016-12-02</q1:ConvictionDecisionDate><q1:ConvictionDecisionFinalDate>2017-01-01</q1:ConvictionDecisionFinalDate><q1:ConvictionDecidingAuthority><q1:DecidingAuthorityName><MultilingualTextLinguisticRepresentation languageCode=\"pl\" xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">SĄD REJONOWY W OTWOCKU</MultilingualTextLinguisticRepresentation></q1:DecidingAuthorityName></q1:ConvictionDecidingAuthority><q1:ConvictionNonCriminalRuling>No</q1:ConvictionNonCriminalRuling><q1:ConvictionIsTransmittable>Yes</q1:ConvictionIsTransmittable><q1:ConvictionDecision><q1:DecisionID>D-00001</q1:DecisionID><q1:DecisionChangeTypeReference>DCH-00-X</q1:DecisionChangeTypeReference><q1:DecisionDeleteConvictionFromRegister>No</q1:DecisionDeleteConvictionFromRegister></q1:ConvictionDecision><q1:ConvictionDecision><q1:DecisionID>D-00002</q1:DecisionID><q1:DecisionChangeTypeReference>DCH-00-N</q1:DecisionChangeTypeReference><q1:DecisionDate>2022-04-27</q1:DecisionDate><q1:DecisionDecidingAuthority><q1:DecidingAuthorityName><MultilingualTextLinguisticRepresentation languageCode=\"pl\" xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">SĄD REJONOWY W OTWOCKU</MultilingualTextLinguisticRepresentation></q1:DecidingAuthorityName></q1:DecisionDecidingAuthority><q1:DecisionDeleteConvictionFromRegister>No</q1:DecisionDeleteConvictionFromRegister></q1:ConvictionDecision><q1:ConvictionOffence><q1:NationalCategoryTitle><MultilingualTextLinguisticRepresentation languageCode=\"pl\" xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">Paserstwo towarami akcyzowymi objętymi obowiązkiem oznaczania znakiem akcyzy a wydanymi bez należytego oznakowania znakami akcyzy, bezprawnie wyprowadzonymi ze składu podatkowego bez oznaczenia znakami akcyzy lub towarami, wobec których naruszono warunki zwolnienia od oznaczania wyrobu znakami akcyzy - mała wartość podatku narażonego na uszczuplenie</MultilingualTextLinguisticRepresentation></q1:NationalCategoryTitle><q1:OffenceID>O-00001</q1:OffenceID><q1:OffenceCommonCategoryReference>O-00-160400</q1:OffenceCommonCategoryReference><q1:OffenceApplicableLegalProvisions>art. 65 § 3 Ust. z dn. 10.09.1999 r. KKS (Dz.U.2013.186) w zw z</q1:OffenceApplicableLegalProvisions><q1:OffenceStartDate><DateYear xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">2015</DateYear><DateMonthDay xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\"><DateMonth>--11</DateMonth><DateDay>---17</DateDay></DateMonthDay></q1:OffenceStartDate><q1:OffenceEndDate><DateYear xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">2015</DateYear><DateMonthDay xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\"><DateMonth>--11</DateMonth><DateDay>---17</DateDay></DateMonthDay></q1:OffenceEndDate><q1:OffencePlace><q1:PlaceCountryReference>CO-00-616-POL</q1:PlaceCountryReference><q1:PlaceCountrySubdivisionReference>CS-00-PL-MZ</q1:PlaceCountrySubdivisionReference><q1:PlaceTownName><MultilingualTextLinguisticRepresentation languageCode=\"pl\" xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">OTWOCK</MultilingualTextLinguisticRepresentation></q1:PlaceTownName></q1:OffencePlace><q1:OffenceNumberOfOccurrences>1</q1:OffenceNumberOfOccurrences><q1:OffenceIsContinuous>No</q1:OffenceIsContinuous><q1:OffenceLevelOfCompletionReference>LC-00-C</q1:OffenceLevelOfCompletionReference><q1:OffenceLevelOfParticipationReference>LP-00-M</q1:OffenceLevelOfParticipationReference><q1:OffenceResponsibilityExemption>No</q1:OffenceResponsibilityExemption><q1:OffenceRecidivism>No</q1:OffenceRecidivism></q1:ConvictionOffence><q1:ConvictionSanction><q1:NationalCategoryTitle><MultilingualTextLinguisticRepresentation languageCode=\"pl\" xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">grzywna w stawkach dziennych</MultilingualTextLinguisticRepresentation></q1:NationalCategoryTitle><q1:SanctionID>S-00001</q1:SanctionID><q1:SanctionTypeReference>N-00-0</q1:SanctionTypeReference><q1:SanctionCommonCategoryReference>S-00-008002</q1:SanctionCommonCategoryReference><q1:SanctionMultiplier>1</q1:SanctionMultiplier><q1:SanctionIsSpecificToMinor>No</q1:SanctionIsSpecificToMinor><q1:SanctionNumberOfFines>50</q1:SanctionNumberOfFines><q1:SanctionAmountOfIndividualFine><PositiveDecimalUnit xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">70</PositiveDecimalUnit></q1:SanctionAmountOfIndividualFine><q1:SanctionCurrencyOfFineReference>CX-00-985-PLN</q1:SanctionCurrencyOfFineReference></q1:ConvictionSanction><q1:ConvictionSanction><q1:NationalCategoryTitle><MultilingualTextLinguisticRepresentation languageCode=\"pl\" xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">grzywna w stawkach dziennych</MultilingualTextLinguisticRepresentation></q1:NationalCategoryTitle><q1:SanctionID>S-00002</q1:SanctionID><q1:SanctionTypeReference>N-00-0</q1:SanctionTypeReference><q1:SanctionCommonCategoryReference>S-00-008002</q1:SanctionCommonCategoryReference><q1:SanctionMultiplier>1</q1:SanctionMultiplier><q1:SanctionIsSpecificToMinor>No</q1:SanctionIsSpecificToMinor><q1:SanctionExecutionPeriod><q1:PeriodEndDate>2022-04-27</q1:PeriodEndDate></q1:SanctionExecutionPeriod><q1:SanctionNumberOfFines>50</q1:SanctionNumberOfFines><q1:SanctionAmountOfIndividualFine><PositiveDecimalUnit xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">70</PositiveDecimalUnit></q1:SanctionAmountOfIndividualFine><q1:SanctionCurrencyOfFineReference>CX-00-985-PLN</q1:SanctionCurrencyOfFineReference></q1:ConvictionSanction><q1:ConvictionRelationship xsi:type=\"q1:DecisionToOffencesRelationshipType\"><q1:Decision>D-00001</q1:Decision><q1:Offence>O-00001</q1:Offence></q1:ConvictionRelationship><q1:ConvictionRelationship xsi:type=\"q1:DecisionToSanctionsRelationshipType\"><q1:Decision>D-00001</q1:Decision><q1:Sanction>S-00001</q1:Sanction></q1:ConvictionRelationship><q1:ConvictionRelationship xsi:type=\"q1:SanctionToOffencesRelationshipType\"><q1:Sanction>S-00001</q1:Sanction><q1:Offence>O-00001</q1:Offence></q1:ConvictionRelationship><q1:ConvictionRelationship xsi:type=\"q1:DecisionToSanctionsRelationshipType\"><q1:Decision>D-00002</q1:Decision><q1:Sanction>S-00002</q1:Sanction></q1:ConvictionRelationship><q1:ConvictionRelationship xsi:type=\"q1:SanctionToSanctionsRelationshipType\" relationshipTypeReference=\"SSR-00-02\"><q1:SourceSanction>S-00002</q1:SourceSanction><q1:DestinationSanction>S-00001</q1:DestinationSanction></q1:ConvictionRelationship></q1:NotificationMessageConviction><q1:NotificationMessageUpdatedConvictionReference><q1:ECRISConvictionReference>PL-C-000000000066709</q1:ECRISConvictionReference></q1:NotificationMessageUpdatedConvictionReference></AbstractMessageType>");
            var result = new EcrisRequestDTO();

            var requestMessage = (RequestMessageType)msg;

            await getRequestingAuthorityByCode(requestMessage);
            await getPersonCountryByCode(requestMessage);
            await getRequestPurposeCategoryByCode(requestMessage);
            var memberState = await getSendingMemberStateName(requestMessage);
            var receivingMemberState = await getReceivingMemberStateName(requestMessage);


            result = mapper.Map<EcrisRequestDTO>((RequestMessageType)msg);
            result.SendingMemberState = memberState.Name; //can't be done in mapper
            result.ReceivingMemberState = receivingMemberState.Name; //can't be done in mapper

            return result;
        }

        public async Task<IQueryable<FbbcGridDTO>> GetEcrisFbbcsByIdAsync(string ecrisMessageId)
        {
            var ecrisMessage = await SelectAsync(ecrisMessageId);
            if (ecrisMessage == null)
            {
                return new List<FbbcGridDTO>().AsQueryable();
            }

            var docType = _nomenclatureDetailRepository.GetAllFbbcDocTypes()
                .FirstOrDefault(x => x.Code == EcrisFbbcMessageType);

            var result = _fbcRepository.SelectAll()
                .Where(x => x.DocTypeId == docType.Id && x.Egn == ecrisMessage.Identifier)
                .ProjectTo<FbbcGridDTO>(mapperConfiguration);

            return result;
        }


        public async Task<IQueryable<EcrisMsgNationalityDTO>> GetNationalitiesAsync(string aId)
        {
            var nationalities = await _ecrisMessageRepository.SelectAllNationalitiesAsync();
            var filteredNationalities = nationalities.Where(x => x.EEcrisMsgId == aId);
            return filteredNationalities.ProjectTo<EcrisMsgNationalityDTO>(mapperConfiguration);
        }

        public async Task<IQueryable<EcrisMsgNameDTO>> GetNamesAsync(string aId)
        {
            var names = await _ecrisMessageRepository.SelectAllNamesAsync();
            var filteredNames = names.Where(x => x.EEcrisMsgId == aId);
            return filteredNames.ProjectTo<EcrisMsgNameDTO>(mapperConfiguration);
        }


        public async Task IdentifyAsync(string aInDto, string graoPersonId)
        {
            var ecrisMessage = await _ecrisMessageRepository.SingleOrDefaultAsync<EEcrisMessage>(x => x.Id == aInDto);
            //await dbContext.EEcrisMessages
            //.FirstOrDefaultAsync(x => x.Id == aInDto);
            var ecrisIdentif =
                await _ecrisMessageRepository.SingleOrDefaultAsync<EEcrisIdentification>(x =>
                    x.EcrisMsgId == aInDto && x.GraoPersonId == graoPersonId);
            //await dbContext.EEcrisIdentifications
            //    .Where(x => x.EcrisMsgId == aInDto && x.GraoPersonId == graoPersonId)
            //    .FirstOrDefaultAsync();

            if (ecrisMessage == null)
            {
                throw new ArgumentException($"Ecris message with id: {aInDto} is missing");
            }

            ecrisMessage.EcrisMsgStatus = "Identified";
            ecrisIdentif.Approved = 1;

            await _ecrisMessageRepository.SaveChangesAsync();
        }

        public async Task<IQueryable<GraoPersonGridDTO>> GetGraoPeopleAsync(string aId)
        {
            return await _ecrisMessageRepository.GetGraoPeopleAsync(aId);
        }

        public async Task<EcrisNotificationDTO> GetEcrisNotificationByIdAsync(string ecrisMessageId)
        {
            var ecrisMessage = await _dDocumentRepository.SelectByEcrisIdAsync(ecrisMessageId);
            if (ecrisMessage == null)
            {
                throw new BusinessLogicException($"�� � ������� �������� � ID: {ecrisMessage}");
            }

            var doc = new XmlDocument();
            var xml = Encoding.UTF8.GetString(ecrisMessage.DocContent.Content);
            var msg = XmlUtils.DeserializeXml<AbstractMessageType>(
                "<?xml version=\"1.0\" encoding=\"utf-8\"?><AbstractMessageType xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:q1=\"http://ec.europa.eu/ECRIS-RI/domain-v1.0\" xsi:type=\"q1:NotificationMessageType\"><q1:MessageIdentifier>RI-NOT-000000001507915</q1:MessageIdentifier><q1:MessageEcrisIdentifier>PL-BG-NOT-000000000012055</q1:MessageEcrisIdentifier><q1:MessageSendingMemberState>PL</q1:MessageSendingMemberState><q1:MessageReceivingMemberState>BG</q1:MessageReceivingMemberState><q1:MessageType>NOT</q1:MessageType><q1:MessageVersionTimestamp>2022-05-06T16:22:15.724+03:00</q1:MessageVersionTimestamp><q1:MessagePerson><q1:PersonName><q1:Forename languageCode=\"pl\">ALEKO</q1:Forename><q1:Surname languageCode=\"pl\">BONEV</q1:Surname></q1:PersonName><q1:PersonSex>1</q1:PersonSex><q1:PersonBirthDate><DateYear xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">1985</DateYear><DateMonthDay xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\"><DateMonth>--12</DateMonth><DateDay>---03</DateDay></DateMonthDay></q1:PersonBirthDate><q1:PersonBirthPlace xsi:type=\"q1:PlaceType\"><q1:PlaceCountryReference>CO-00-100-BGR</q1:PlaceCountryReference><q1:PlaceTownReference>CI-00-BGR-T0006</q1:PlaceTownReference><q1:PlaceTownName><MultilingualTextLinguisticRepresentation languageCode=\"pl\" xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">STARA ZAGORA</MultilingualTextLinguisticRepresentation></q1:PlaceTownName></q1:PersonBirthPlace><q1:PersonNationalityReference>CO-00-100-BGR</q1:PersonNationalityReference><q1:PersonFatherForename languageCode=\"pl\">BESZKOV</q1:PersonFatherForename><q1:PersonMotherForename languageCode=\"pl\">DINKE</q1:PersonMotherForename><q1:PersonAddress><q1:AddressPlace><q1:PlaceCountryReference>CO-00-616-POL</q1:PlaceCountryReference><q1:PlaceCountrySubdivisionReference>CS-00-PL-MZ</q1:PlaceCountrySubdivisionReference><q1:PlaceTownReference>CI-00-POL-T0001</q1:PlaceTownReference><q1:PlaceTownName><MultilingualTextLinguisticRepresentation languageCode=\"pl\" xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">WARSZAWA</MultilingualTextLinguisticRepresentation></q1:PlaceTownName></q1:AddressPlace></q1:PersonAddress></q1:MessagePerson><q1:NotificationMessageConviction><q1:ConvictionID>PL-C-000000000066709</q1:ConvictionID><q1:ConvictionConvictingCountryReference>CO-00-616-POL</q1:ConvictionConvictingCountryReference><q1:ConvictionFileNumber>IIK101/16</q1:ConvictionFileNumber><q1:ConvictionDecisionDate>2016-12-02</q1:ConvictionDecisionDate><q1:ConvictionDecisionFinalDate>2017-01-01</q1:ConvictionDecisionFinalDate><q1:ConvictionDecidingAuthority><q1:DecidingAuthorityName><MultilingualTextLinguisticRepresentation languageCode=\"pl\" xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">SĄD REJONOWY W OTWOCKU</MultilingualTextLinguisticRepresentation></q1:DecidingAuthorityName></q1:ConvictionDecidingAuthority><q1:ConvictionNonCriminalRuling>No</q1:ConvictionNonCriminalRuling><q1:ConvictionIsTransmittable>Yes</q1:ConvictionIsTransmittable><q1:ConvictionDecision><q1:DecisionID>D-00001</q1:DecisionID><q1:DecisionChangeTypeReference>DCH-00-X</q1:DecisionChangeTypeReference><q1:DecisionDeleteConvictionFromRegister>No</q1:DecisionDeleteConvictionFromRegister></q1:ConvictionDecision><q1:ConvictionDecision><q1:DecisionID>D-00002</q1:DecisionID><q1:DecisionChangeTypeReference>DCH-00-N</q1:DecisionChangeTypeReference><q1:DecisionDate>2022-04-27</q1:DecisionDate><q1:DecisionDecidingAuthority><q1:DecidingAuthorityName><MultilingualTextLinguisticRepresentation languageCode=\"pl\" xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">SĄD REJONOWY W OTWOCKU</MultilingualTextLinguisticRepresentation></q1:DecidingAuthorityName></q1:DecisionDecidingAuthority><q1:DecisionDeleteConvictionFromRegister>No</q1:DecisionDeleteConvictionFromRegister></q1:ConvictionDecision><q1:ConvictionOffence><q1:NationalCategoryTitle><MultilingualTextLinguisticRepresentation languageCode=\"pl\" xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">Paserstwo towarami akcyzowymi objętymi obowiązkiem oznaczania znakiem akcyzy a wydanymi bez należytego oznakowania znakami akcyzy, bezprawnie wyprowadzonymi ze składu podatkowego bez oznaczenia znakami akcyzy lub towarami, wobec których naruszono warunki zwolnienia od oznaczania wyrobu znakami akcyzy - mała wartość podatku narażonego na uszczuplenie</MultilingualTextLinguisticRepresentation></q1:NationalCategoryTitle><q1:OffenceID>O-00001</q1:OffenceID><q1:OffenceCommonCategoryReference>O-00-160400</q1:OffenceCommonCategoryReference><q1:OffenceApplicableLegalProvisions>art. 65 § 3 Ust. z dn. 10.09.1999 r. KKS (Dz.U.2013.186) w zw z</q1:OffenceApplicableLegalProvisions><q1:OffenceStartDate><DateYear xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">2015</DateYear><DateMonthDay xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\"><DateMonth>--11</DateMonth><DateDay>---17</DateDay></DateMonthDay></q1:OffenceStartDate><q1:OffenceEndDate><DateYear xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">2015</DateYear><DateMonthDay xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\"><DateMonth>--11</DateMonth><DateDay>---17</DateDay></DateMonthDay></q1:OffenceEndDate><q1:OffencePlace><q1:PlaceCountryReference>CO-00-616-POL</q1:PlaceCountryReference><q1:PlaceCountrySubdivisionReference>CS-00-PL-MZ</q1:PlaceCountrySubdivisionReference><q1:PlaceTownName><MultilingualTextLinguisticRepresentation languageCode=\"pl\" xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">OTWOCK</MultilingualTextLinguisticRepresentation></q1:PlaceTownName></q1:OffencePlace><q1:OffenceNumberOfOccurrences>1</q1:OffenceNumberOfOccurrences><q1:OffenceIsContinuous>No</q1:OffenceIsContinuous><q1:OffenceLevelOfCompletionReference>LC-00-C</q1:OffenceLevelOfCompletionReference><q1:OffenceLevelOfParticipationReference>LP-00-M</q1:OffenceLevelOfParticipationReference><q1:OffenceResponsibilityExemption>No</q1:OffenceResponsibilityExemption><q1:OffenceRecidivism>No</q1:OffenceRecidivism></q1:ConvictionOffence><q1:ConvictionSanction><q1:NationalCategoryTitle><MultilingualTextLinguisticRepresentation languageCode=\"pl\" xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">grzywna w stawkach dziennych</MultilingualTextLinguisticRepresentation></q1:NationalCategoryTitle><q1:SanctionID>S-00001</q1:SanctionID><q1:SanctionTypeReference>N-00-0</q1:SanctionTypeReference><q1:SanctionCommonCategoryReference>S-00-008002</q1:SanctionCommonCategoryReference><q1:SanctionMultiplier>1</q1:SanctionMultiplier><q1:SanctionIsSpecificToMinor>No</q1:SanctionIsSpecificToMinor><q1:SanctionNumberOfFines>50</q1:SanctionNumberOfFines><q1:SanctionAmountOfIndividualFine><PositiveDecimalUnit xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">70</PositiveDecimalUnit></q1:SanctionAmountOfIndividualFine><q1:SanctionCurrencyOfFineReference>CX-00-985-PLN</q1:SanctionCurrencyOfFineReference></q1:ConvictionSanction><q1:ConvictionSanction><q1:NationalCategoryTitle><MultilingualTextLinguisticRepresentation languageCode=\"pl\" xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">grzywna w stawkach dziennych</MultilingualTextLinguisticRepresentation></q1:NationalCategoryTitle><q1:SanctionID>S-00002</q1:SanctionID><q1:SanctionTypeReference>N-00-0</q1:SanctionTypeReference><q1:SanctionCommonCategoryReference>S-00-008002</q1:SanctionCommonCategoryReference><q1:SanctionMultiplier>1</q1:SanctionMultiplier><q1:SanctionIsSpecificToMinor>No</q1:SanctionIsSpecificToMinor><q1:SanctionExecutionPeriod><q1:PeriodEndDate>2022-04-27</q1:PeriodEndDate></q1:SanctionExecutionPeriod><q1:SanctionNumberOfFines>50</q1:SanctionNumberOfFines><q1:SanctionAmountOfIndividualFine><PositiveDecimalUnit xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">70</PositiveDecimalUnit></q1:SanctionAmountOfIndividualFine><q1:SanctionCurrencyOfFineReference>CX-00-985-PLN</q1:SanctionCurrencyOfFineReference></q1:ConvictionSanction><q1:ConvictionRelationship xsi:type=\"q1:DecisionToOffencesRelationshipType\"><q1:Decision>D-00001</q1:Decision><q1:Offence>O-00001</q1:Offence></q1:ConvictionRelationship><q1:ConvictionRelationship xsi:type=\"q1:DecisionToSanctionsRelationshipType\"><q1:Decision>D-00001</q1:Decision><q1:Sanction>S-00001</q1:Sanction></q1:ConvictionRelationship><q1:ConvictionRelationship xsi:type=\"q1:SanctionToOffencesRelationshipType\"><q1:Sanction>S-00001</q1:Sanction><q1:Offence>O-00001</q1:Offence></q1:ConvictionRelationship><q1:ConvictionRelationship xsi:type=\"q1:DecisionToSanctionsRelationshipType\"><q1:Decision>D-00002</q1:Decision><q1:Sanction>S-00002</q1:Sanction></q1:ConvictionRelationship><q1:ConvictionRelationship xsi:type=\"q1:SanctionToSanctionsRelationshipType\" relationshipTypeReference=\"SSR-00-02\"><q1:SourceSanction>S-00002</q1:SourceSanction><q1:DestinationSanction>S-00001</q1:DestinationSanction></q1:ConvictionRelationship></q1:NotificationMessageConviction><q1:NotificationMessageUpdatedConvictionReference><q1:ECRISConvictionReference>PL-C-000000000066709</q1:ECRISConvictionReference></q1:NotificationMessageUpdatedConvictionReference></AbstractMessageType>");
            var result = mapper.Map<EcrisNotificationDTO>((NotificationMessageType)msg);
            return result;
        }

        public async Task<EcrisResponseDTO> GetEcrisResponseByIdAsync(string ecrisMessageId)
        {
            var ecrisMessage = await _dDocumentRepository.SelectByEcrisIdAsync(ecrisMessageId);
            if (ecrisMessage == null)
            {
                throw new BusinessLogicException($"�� � ������� �������� � ID: {ecrisMessage}");
            }

            var doc = new XmlDocument();
            var xml = Encoding.UTF8.GetString(ecrisMessage.DocContent.Content);
            var msg = XmlUtils.DeserializeXml<AbstractMessageType>("<?xml version=\"1.0\" encoding=\"utf-8\"?><AbstractMessageType xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:q1=\"http://ec.europa.eu/ECRIS-RI/domain-v1.0\" xsi:type=\"q1:RequestResponseMessageType\"><q1:MessageIdentifier>RI-RRS-000000001382773</q1:MessageIdentifier><q1:MessageEcrisIdentifier>BG-BE-RRS-000000000019318</q1:MessageEcrisIdentifier><q1:MessageResponseTo><q1:MessageIdentifier>RI-REQ-000000001369858</q1:MessageIdentifier><q1:MessageEcrisIdentifier>BE-BG-REQ-000000000019444</q1:MessageEcrisIdentifier></q1:MessageResponseTo><q1:MessageSendingMemberState>BG</q1:MessageSendingMemberState><q1:MessageReceivingMemberState>BE</q1:MessageReceivingMemberState><q1:MessageType>RRS</q1:MessageType><q1:MessageVersionTimestamp>2021-12-07T12:59:36.399+02:00</q1:MessageVersionTimestamp><q1:MessageLastModifiedByUser>IVA</q1:MessageLastModifiedByUser><q1:AuthoringLanguage>bg</q1:AuthoringLanguage><q1:MessagePerson><q1:PersonName><q1:Forename languageCode=\"bg\">VELI</q1:Forename><q1:Surname languageCode=\"bg\">CHOBAN</q1:Surname><q1:SecondSurname languageCode=\"bg\">ALI</q1:SecondSurname></q1:PersonName><q1:PersonSex>1</q1:PersonSex><q1:PersonBirthDate><DateYear xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">1987</DateYear><DateMonthDay xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\"><DateMonth>--04</DateMonth><DateDay>---29</DateDay></DateMonthDay></q1:PersonBirthDate><q1:PersonBirthPlace xsi:type=\"q1:PlaceType\"><q1:PlaceCountryReference>CO-00-100-BGR</q1:PlaceCountryReference><q1:PlaceTownName><MultilingualTextLinguisticRepresentation languageCode=\"bg\" translated=\"false\" xmlns=\"http://ec.europa.eu/ECRIS-RI/commons-v1.0\">Kotel</MultilingualTextLinguisticRepresentation></q1:PlaceTownName></q1:PersonBirthPlace><q1:PersonNationalityReference>CO-00-100-BGR</q1:PersonNationalityReference><q1:PersonIdentityNumber>8704295867</q1:PersonIdentityNumber></q1:MessagePerson><q1:RequestResponseMessageRequestResponseTypeReference>RRT-00-00</q1:RequestResponseMessageRequestResponseTypeReference></AbstractMessageType>\r\n");
            var result = mapper.Map<EcrisResponseDTO>((RequestResponseMessageType)msg);
            return result;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }

        private async Task<EEcrisAuthority> getSendingMemberStateName(RequestMessageType requestMessage)
        {
            EEcrisAuthority? memberState = null;
            if (requestMessage.MessageSendingMemberState != null)
            {
                memberState =
                    await
                        _dbContext.EEcrisAuthorities
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x =>
                                x.MemberStateCode == requestMessage.MessageSendingMemberState.ToString());
            }

            return memberState;
        }

        private async Task<EEcrisAuthority> getReceivingMemberStateName(RequestMessageType requestMessage)
        {
            EEcrisAuthority? memberState = null;
            if (requestMessage.MessageSendingMemberState != null)
            {
                memberState =
                    await
                        _dbContext.EEcrisAuthorities
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x =>
                                x.MemberStateCode == requestMessage.MessageReceivingMemberState[0].ToString());
            }

            return memberState;
        }

        private async Task getRequestPurposeCategoryByCode(RequestMessageType requestMessage)
        {
            if (requestMessage.RequestMessageRequestPurposeCategoryReference.Value != null)
            {
                var requestPurposeCategory =
                    await
                        _dbContext.EEcrisNomenclatures
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x =>
                                x.EcrisTechId == requestMessage.RequestMessageRequestPurposeCategoryReference.Value);

                if (requestPurposeCategory != null)
                {
                    requestMessage.RequestMessageRequestPurposeCategoryReference.Value = requestPurposeCategory.NameBg;
                }
            }
        }

        private async Task getPersonCountryByCode(RequestMessageType requestMessage)
        {
            if (requestMessage.MessagePerson.PersonBirthPlace.PlaceCountryReference.Value != null)
            {
                var country = await _dbContext.GCountries.AsNoTracking().FirstOrDefaultAsync(x =>
                    x.Id ==
                    requestMessage.MessagePerson.PersonBirthPlace.PlaceCountryReference.Value);

                if (country != null)
                {
                    requestMessage.MessagePerson.PersonBirthPlace.PlaceCountryReference.Value = country.Name;
                }
            }
        }

        private async Task getRequestingAuthorityByCode(RequestMessageType requestMessage)
        {
            if (requestMessage.RequestMessageRequestingAuthority.RequestingAuthorityTypeReference.Value != null)
            {
                var authorityType =
                    await
                        _dbContext.EEcrisNomenclatures
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x =>
                                x.EcrisTechId == requestMessage.RequestMessageRequestingAuthority
                                    .RequestingAuthorityTypeReference.Value);
                if (authorityType != null)
                {
                    requestMessage.RequestMessageRequestingAuthority
                        .RequestingAuthorityTypeReference.Value = authorityType.NameBg;
                }
            }
        }
    }
}