using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.ExternalWebServices.DTO
{
    public class FieldsForPopulation
    {
        public List<FieldForPopulation> fields;
        public void AddFields(FieldsForPopulation valuesToAdd)
        {
            fields.AddRange(valuesToAdd.fields);
        }
    }

    public class FieldForPopulation
    {
        public string FieldName;
        public int Priority;
        public object Value;
        public string SourceFieldName;
        public string TransformationFunctionName = "Identity";
        public bool IsValueSet;
        public bool IsNavigation;
        public FieldForPopulation NavigationField;

    }
}
