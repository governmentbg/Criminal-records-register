using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MJ_CAIS.WebSetup.Utils.CustomModelBinders
{
    public class CustomDateTimeModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueResult);

            object actualValue = null;
            var attemptedValue = valueResult.FirstValue;

            if (string.IsNullOrEmpty(attemptedValue))
            {
                return Task.CompletedTask;
            }

            try
            {
                DateTime.TryParse(attemptedValue, out DateTime dateValue);
                actualValue = dateValue.ToLocalTime();
            }
            catch (FormatException e)
            {
                bindingContext.ModelState.TryAddModelError(
                    modelName, e.Message);
                return Task.CompletedTask;
            }

            bindingContext.Result = ModelBindingResult.Success(actualValue);
            return Task.CompletedTask;
        }
    }
}
