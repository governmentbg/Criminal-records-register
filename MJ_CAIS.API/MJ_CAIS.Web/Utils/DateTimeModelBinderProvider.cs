using Microsoft.AspNetCore.Mvc.ModelBinding;
using NodaTime;
using NodaTime.Text;
using System.Globalization;

namespace MJ_CAIS.Web.Utils
{
    public class DateTimeModelBinderProvider : IModelBinderProvider
    {
        // You could make this a property to allow customization
        internal static readonly DateTimeStyles SupportedStyles = DateTimeStyles.AdjustToUniversal | DateTimeStyles.AllowWhiteSpaces;
        private readonly ILogger<DateTimeModelBinderProvider> _logger;

        public DateTimeModelBinderProvider(ILogger<DateTimeModelBinderProvider> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var modelType = context.Metadata.UnderlyingOrModelType;
            var loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();
            if (modelType == typeof(DateTime))
            {
                return new UtcAwareDateTimeModelBinder(SupportedStyles, loggerFactory);
            }

            return null;
        }
    }
    public class UtcAwareDateTimeModelBinder : IModelBinder
    {
        private readonly DateTimeStyles _supportedStyles;
        private readonly ILogger _logger;

        public UtcAwareDateTimeModelBinder(DateTimeStyles supportedStyles, ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            _supportedStyles = supportedStyles;
            _logger = loggerFactory.CreateLogger<UtcAwareDateTimeModelBinder>();
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                // no entry
                return Task.CompletedTask;
            }

            var modelState = bindingContext.ModelState;
            modelState.SetModelValue(modelName, valueProviderResult);

            var metadata = bindingContext.ModelMetadata;
            var type = metadata.UnderlyingOrModelType;

            var value = valueProviderResult.FirstValue;
            var culture = valueProviderResult.Culture;

            object model;
            if (string.IsNullOrWhiteSpace(value))
            {
                model = null;
            }
            else if (type == typeof(DateTime))
            {
                model = DateTimeConverter.ParseDate(value, _logger);
            }
            else
            {
                // unreachable
                throw new NotSupportedException();
            }

            // When converting value, a null model may indicate a failed conversion for an otherwise required
            // model (can't set a ValueType to null). This detects if a null model value is acceptable given the
            // current bindingContext. If not, an error is logged.
            if (model == null && !metadata.IsReferenceOrNullableType)
            {
                modelState.TryAddModelError(
                    modelName,
                    metadata.ModelBindingMessageProvider.ValueMustNotBeNullAccessor(
                        valueProviderResult.ToString()));
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Success(model);
            }

            return Task.CompletedTask;
        }
    }
}
