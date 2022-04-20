using MJ_CAIS.CodeGenerator.Utils;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.EntityTransform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var rootPath = ClassGenerator.GetCurrentProjectPath();
            CodeTransformer.TransformEntities(rootPath, Nomenclatures);
            CodeTransformer.TransformDbContext(rootPath);
        }

        public static List<string> Nomenclatures = new List<string>
        {
            nameof(BCaseType),
            nameof(BDecisionChType),
            nameof(BDecisionType),
            nameof(BEcrisOffCategory),
            nameof(BEcrisOffLvlPart),
            nameof(BEcrisStanctCateg),
            nameof(BIdDocCategory),
            nameof(BOffenceCategory),
            nameof(BOffenceLvlCompletion),
            nameof(BSanctionActivity),
            nameof(BSanctionCategory),
            nameof(BSanctProbCategory),
            nameof(BSanctProbMeasure),
            nameof(DDocType),
            nameof(GCountry),
            nameof(GCountrySubdivision),
            nameof(GCsAuthority),
            nameof(GDecidingAuthority),
            nameof(GBgDistrict),
            nameof(GBgMunicipality),
            nameof(GCity),
            nameof(EEcrisAuthority),
            nameof(BFormOfGuilt),
        };
    }
}