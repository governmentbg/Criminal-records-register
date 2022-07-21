using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MJ_CAIS.WebSetup.Utils.CustomModelBinders
{
    public class CustomDateTimeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(DateTime) || context.Metadata.ModelType == typeof(DateTime?))
                return new CustomDateTimeModelBinder();

            return null;
        }
    }
}
