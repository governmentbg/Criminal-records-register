using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ExternalServicesHost;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;

namespace MJ_CAIS.Services
{
    public class EISSIntegrationService : IEISSIntegrationService
    {
        private readonly IBulletinRepository _bulletinRepository;
        private readonly INomenclatureDetailRepository _nomenclatureDetailRepository;
        private readonly IMapper _mapper;

        public EISSIntegrationService(IMapper mapper, IBulletinRepository bulletinRepository, INomenclatureDetailRepository nomenclatureDetailRepository)
        {
            _mapper = mapper;
            _bulletinRepository = bulletinRepository;
            _nomenclatureDetailRepository = nomenclatureDetailRepository;
        }

        public async Task SendBulletinsDataAsync(SendBulletinsDataRequestType value)
        {
            var ekatteCodes = value.BulletinsList.Bulletin.
                Select(x => x.Person.BirthPlace.City.EKATTECode)
                .Where(x => !string.IsNullOrEmpty(x))
                .Distinct()
                .ToList();

            var countries = await _nomenclatureDetailRepository.GetCountries().ToListAsync();
            var authQuery = await _nomenclatureDetailRepository.GetDecidingAuthoritiesForBulletinsAsync();
            var auths = await authQuery.ToListAsync();

            var authEkatte = await _bulletinRepository.GetAuthIdByEkkateAsync(ekatteCodes);

            var bulletinToBeSaved = new List<BBulletin>();
            foreach (var item in value.BulletinsList.Bulletin)
            {
                var currentBull = _mapper.Map<BBulletin>(item);
                currentBull.StatusId = BulletinConstants.Status.NewEISS;
                currentBull.CsAuthorityId = !string.IsNullOrWhiteSpace(currentBull.BirthCity?.EkatteCode) && authEkatte.ContainsKey(currentBull.BirthCity.EkatteCode) ?
                    authEkatte[currentBull.BirthCity.EkatteCode] :
                    "660";

                currentBull.Id = BaseEntity.GenerateNewId();
                currentBull.BirthCountryId = GetCountryId(item.Person?.BirthPlace?.Country, countries);
                currentBull.DecidingAuthId = GetAuthId(item.Conviction?.Decision?.DecidingAuthority, auths);
                currentBull.CaseAuthId = GetAuthId(item.Conviction?.CriminalCase?.CaseAuthority, auths);
                currentBull.BulletinAuthorityId = GetAuthId(item.IssuerData?.BulletinCreatorAuthority, auths);
                currentBull.Locked = true;

                if (item.Person?.PersonNationality != null)
                {
                    currentBull.BPersNationalities = new List<BPersNationality>();
                    foreach (var nationality in item.Person.PersonNationality)
                    {
                        var nat = _mapper.Map<BPersNationality>(nationality);
                        nat.CountryId = GetCountryId(nationality, countries);
                        currentBull.BPersNationalities.Add(nat);
                    }
                }

                if (item.Conviction?.ConvictionDecisions != null)
                {
                    currentBull.BDecisions = new List<BDecision>();
                    foreach (var decision in item.Conviction.ConvictionDecisions)
                    {
                        var dec = _mapper.Map<BDecision>(decision);
                        dec.DecisionAuthId = GetAuthId(decision.Decision?.DecidingAuthority, auths);
                        currentBull.BDecisions.Add(dec);
                    }
                }

                if (item.Conviction?.ConvictionOffence != null)
                {
                    currentBull.BOffences = new List<BOffence>();
                    foreach (var offence in item.Conviction.ConvictionOffence)
                    {
                        var off = _mapper.Map<BOffence>(offence);
                        off.OffPlaceCountryId = GetCountryId(offence.OffencePlace?.Country, countries);
                        currentBull.BOffences.Add(off);
                    }
                }

                bulletinToBeSaved.Add(currentBull);
            }

            await _bulletinRepository.SaveBulletinsAsync(bulletinToBeSaved);
        }

        public async Task SendFinesDataAsync(SendFineDataRequestType value)
        {
            var dbContext = _bulletinRepository.GetDbContext();
            var isinData = new List<EIsinDatum>();
            // todo:
            foreach (var fine in value.FineDataList.Fine)
            {
                var curertnIsinData = _mapper.Map<EIsinDatum>(fine);
                curertnIsinData.EntityState = EntityStateEnum.Added;
                curertnIsinData.Status = IsinDataConstants.Status.New;

                if (!string.IsNullOrEmpty(fine.PersonData?.Identifier) && fine.PersonData.IdentifierType == IdentifierType.EGN)
                {

                    var egn = fine.PersonData.Identifier;
                    var decisionTypeId = fine.ConvictionData.ActTypeCodeSpecified ? fine.ConvictionData.ActTypeCode.ToString() : null;
                    var decisionDate = fine.ConvictionData.ActDateSpecified ? fine.ConvictionData.ActDate : (DateTime?)null;
                    var decisionNumber = fine.ConvictionData.ActNumber;
                    var decidingAuthId = fine.ConvictionData.ActDecidingAuthorityCode;
                    var caseNumber = fine.ConvictionData.CaseNumber;
                    var caseYearParsed = decimal.TryParse(fine.ConvictionData.CaseYear, out decimal caseYear);
                    var caseTypeId = fine.ConvictionData.CaseTypeCodeSpecified ? fine.ConvictionData.CaseTypeCode.ToString() : null;
                    var caseAuthId = fine.ConvictionData.CaseDecidingAuthorityCode;

                    var bulletinId = await dbContext.BBulletins.AsNoTracking()
                                    .Include(x => x.PBulletinIds)
                                        .ThenInclude(x => x.PersonId)
                                    .Where(x => x.PBulletinIds.Any(x => x.Person.Pid == egn) &&
                                           (x.DecisionTypeId == decisionTypeId || string.IsNullOrEmpty(decisionTypeId)) &&
                                           (x.DecisionDate == decisionDate || !decisionDate.HasValue) &&
                                           x.DecisionNumber == decisionNumber &&
                                           x.DecidingAuthId == decidingAuthId &&
                                           x.CaseNumber == caseNumber &&
                                           (x.CaseYear == caseYear || !caseYearParsed) &&
                                           (x.CaseTypeId == caseTypeId || string.IsNullOrEmpty(caseTypeId)) &&
                                            x.CaseAuthId == caseAuthId)
                                    .Select(x => x.Id)
                                    .FirstOrDefaultAsync();

                    if (!string.IsNullOrEmpty(bulletinId))
                    {
                        curertnIsinData.Status = IsinDataConstants.Status.Identified;
                        curertnIsinData.BulletinId = bulletinId;
                    }
                }
            }

            dbContext.EIsinData.AddRange(isinData);
            await dbContext.SaveChangesAsync();
        }

        private string? GetCountryId(CountryType? country, List<GCountry> countries)
        {
            if (country == null) return null;
            return countries.FirstOrDefault(x => country.CountryISONumber == x.Iso31662Number || country.CountryISOAlpha3 == x.Iso31662Code)?.Id;
        }

        private string? GetAuthId(DecidingAuthorityType? auth, List<GDecidingAuthority> authorities)
        {
            if (auth == null) return null;
            return authorities.FirstOrDefault(x => auth.DecidingAuthorityCodeEIK == x.Code?.ToString())?.Id;
        }
    }
}
