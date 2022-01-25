using Microsoft.AspNetCore.OData.Query.Validator;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace MJ_CAIS.Services.Contracts.Utils
{
    public class CustomQueryValidator<V> : FilterQueryValidator
    {
        protected override void ValidateNavigationPropertyNode(QueryNode sourceNode, IEdmNavigationProperty navigationProperty, ODataValidationSettings settings)
        {
            // Source:
            // https://github.com/OData/WebApi/blob/0338075c9940fe29c6fd18cd3f9e0433fd82d745/src/Microsoft.AspNet.OData.Shared/Query/Validators/FilterQueryValidator.cs#L379

            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            // In original source, the navigation property was being checked for filterable.
            // We don't have complex object as properties in DTOs, only collections in tree grid data.
            // So, no property validation check for filterable for this property.

            // recursion
            if (sourceNode != null)
            {
                ValidateQueryNode(sourceNode, settings);
            }
        }

        protected override void ValidateSingleValuePropertyAccessNode(SingleValuePropertyAccessNode accessNode, ODataValidationSettings settings)
        {
            string propertyName = null;
            if (accessNode != null)
            {
                if (accessNode.Source.GetType() == typeof(SingleNavigationNode))
                {
                    var source = ((SingleNavigationNode)accessNode.Source);
                    string navigationPropertyName = "";
                    int count = 0;
                    foreach (var segment in source.NavigationSource.Path.PathSegments)
                    {
                        if (count > 0)
                        {
                            navigationPropertyName += (segment + '/');
                        }

                        count++;
                    }

                    propertyName = navigationPropertyName + accessNode.Property.Name;
                }
                else
                {
                    propertyName = accessNode.Property.Name;
                }
            }

            var allowedProps = this.GetAllowedProperties();
            if (propertyName != null && !allowedProps.Contains(propertyName))
            {
                string allowedPropsAsStr = string.Join(", ", allowedProps);
                throw new ODataException($"Filter on {propertyName} is not allowed. Allowed columns: {allowedPropsAsStr}");
            }
        }

        //This method has not been tested with collections.
        public virtual HashSet<string> GetAllowedProperties()
        {
            var allowedProperties = new HashSet<string>();

            var allBaseTypeProperties = typeof(V).GetProperties()
                .Where(p => !p.PropertyType.IsClass || p.PropertyType.Name == "String");

            var classProperties = typeof(V).GetProperties()
                .Where(p => p.PropertyType.IsClass && p.PropertyType.Name != "String")
                .ToList();

            var baseClassPropertyNames = allBaseTypeProperties.Select(p => p.Name);

            foreach (var currentClassPropertyName in baseClassPropertyNames)
            {
                allowedProperties.Add(currentClassPropertyName);
            }

            for (var i = 0; i < classProperties.Count; i++)
            {
                var currentClass = classProperties[i];

                var allCurrentClassProperties = currentClass.PropertyType
                    .GetProperties();

                var currentClassChildClasses = allCurrentClassProperties
                    .Where(p => p.PropertyType.IsClass && p.PropertyType.Name != "String")
                    .ToList();

                classProperties.AddRange(currentClassChildClasses);

                var currentClassPropertyNames = allCurrentClassProperties.Where(p => !p.PropertyType.IsClass || p.PropertyType.Name == "String")
                    .Select(p => p.Name);

                foreach (var currentClassPropertyName in currentClassPropertyNames)
                {
                    allowedProperties.Add($"{currentClass.Name}/{currentClassPropertyName}");
                }
            }

            return allowedProperties;
        }
    }
}
