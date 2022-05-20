using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MJ_CAIS.Common;
using System.Linq.Expressions;
using System.Reflection;

namespace MJ_CAIS.WebSetup.Utils.CustomHtmlHelpers
{
    public static class CustomEditors
    {
        private const string COMBO_TEMPLATE_NAME = "TLDropDown";
        private const string DATETIME_TEMPLATE_NAME = "DateTime";

        public static IHtmlContent TLDropDownListFor<TModel, TProperty>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, List<SelectListItem> data, bool required = false, string changeHandler = null, string renderedHandler = null, bool disabled = false)
        {
            return TLDropDownListFor(html, expression, data, null, required, changeHandler, renderedHandler, disabled);
        }

        public static IHtmlContent TLDropDownListFor<TModel, TProperty>(
            this IHtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            List<SelectListItem> data,
            object htmlAttributes,
            bool required = false,
            string changeHandler = null,
            string renderedHandler = null,
            bool disabled = false)
        {
            string labelValue = ReflectionExtensions.GetPropertyDisplayName(expression);
            string requiredMessage = string.Format(CommonResources.MsgRequired, labelValue);

            IDictionary<string, object> attributes = InitializeCustomAttributes(htmlAttributes/*, Globals.CustomComboClassAttr*/);

            return html.EditorFor(expression, COMBO_TEMPLATE_NAME, new
            {
                Readonly = disabled,
                Data = data,
                IsRequired = required,
                ChangeHandler = changeHandler,
                RenderedHandler = renderedHandler,
                RequiredMessage = requiredMessage,
                Attributes = attributes
            });
        }

        public static IHtmlContent TLDateTimeFor<TModel, TProperty>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, bool hasTime = false, bool required = false, string changeHandler = null, string renderedHandler = null, bool disabled = false)
        {
            return TLDateTimeFor(html, expression, null, hasTime, required, changeHandler, renderedHandler, disabled);
        }

        public static IHtmlContent TLDateTimeFor<TModel, TProperty>(
            this IHtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes,
            bool hasTime = false,
            bool required = false,
            string changeHandler = null,
            string renderedHandler = null,
            bool disabled = false)
        {
            string labelValue = ReflectionExtensions.GetPropertyDisplayName(expression);
            string requiredMessage = string.Format(CommonResources.MsgRequired, labelValue);

            IDictionary<string, object> attributes = InitializeCustomAttributes(htmlAttributes/*, Globals.CustomDateTimeClassAttr*/);

            return html.EditorFor(expression, DATETIME_TEMPLATE_NAME, new
            {
                Readonly = disabled,
                HasTime = hasTime,
                IsRequired = required,
                ChangeHandler = changeHandler,
                RenderedHandler = renderedHandler,
                RequiredMessage = requiredMessage,
                Attributes = attributes,
            });
        }

        private static IDictionary<string, object> InitializeCustomAttributes(object htmlAttributes, string? className = null)
        {
            Dictionary<string, object> attributes;

            if (htmlAttributes != null)
            {
                attributes = new Dictionary<string, object>(ConvertAnomymousToDictionary(htmlAttributes));
            }
            else
            {
                attributes = new Dictionary<string, object>();
            }

            if (!string.IsNullOrEmpty(className))
            {
                if (!attributes.ContainsKey("class"))
                {
                    attributes.Add("class", className);
                }
                else
                {
                    string cssClass = attributes["class"] as string;
                    cssClass += " " + className;
                    attributes["class"] = cssClass;
                }
            }

            return attributes;
        }

        private static IDictionary<string, object> ConvertAnomymousToDictionary(object htmlAttributes)
        {
            var attributes = new Dictionary<string, object>();
            var properties = htmlAttributes.GetType().GetProperties();
            foreach (var prop in properties)
            {
                attributes.Add(prop.Name.Replace('_', '-'), prop.GetValue(htmlAttributes));
            }
            return attributes;
        }

        public static IHtmlContent EditorFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, bool disabled)
        {
            return EditorFor(html, expression, null, disabled: disabled);
        }

        public static IHtmlContent EditorFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object additionalViewData, bool disabled)
        {
            IDictionary<string, object> attributes = new Dictionary<string, object>();

            if (additionalViewData != null)
            {
                var htmlAttributes = additionalViewData.GetType().GetProperty("htmlAttributes", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (htmlAttributes != null)
                {
                    attributes = AddReadOnlyAttributes(htmlAttributes.GetValue(additionalViewData), disabled);
                }
            }

            return html.EditorFor(expression, new { htmlAttributes = attributes, });
        }

        #region Radio buttons 

        public static IHtmlContent RadioButtonFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object value, bool disabled)
        {
            return RadioButtonFor(htmlHelper, expression, value, null, disabled: disabled);
        }

        public static IHtmlContent RadioButtonFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object value, object htmlAttributes, bool disabled)
        {
            IDictionary<string, object> attributes = AddReadOnlyAttributes(htmlAttributes, disabled);
            return htmlHelper.RadioButtonFor(expression, value, attributes);
        }

        #endregion

        #region TexBoxFor

        public static IHtmlContent TextBoxFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, bool disabled)
        {
            return TextBoxFor(htmlHelper, expression, null, disabled: disabled);
        }

        public static IHtmlContent TextBoxFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes, bool disabled)
        {
            IDictionary<string, object> attributes = AddReadOnlyAttributes(htmlAttributes, disabled);
            return htmlHelper.TextBoxFor(expression, attributes);
        }

        #endregion

        private static IDictionary<string, object> AddReadOnlyAttributes(object htmlAttributes, bool disabled)
        {
            Dictionary<string, object> attributes;

            if (htmlAttributes != null)
            {
                attributes = new Dictionary<string, object>(ConvertAnomymousToDictionary(htmlAttributes));
            }
            else
            {
                attributes = new Dictionary<string, object>();
            }

            if (disabled)
            {
                if (!attributes.ContainsKey("readonly"))
                {
                    attributes.Add("readonly", null);
                }

                if (!attributes.ContainsKey("disabled"))
                {
                    attributes.Add("disabled", null);
                }

                attributes["readonly"] = "readonly";
                attributes["disabled"] = "disabled";
            }

            return attributes;
        }
    }
}
