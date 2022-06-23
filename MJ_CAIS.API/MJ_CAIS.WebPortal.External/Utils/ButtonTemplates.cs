using MJ_CAIS.Common;

namespace MJ_CAIS.WebPortal.External.Utils
{
    public class ButtonTemplates
    {
        private const string CommonAnchorHrefTemplate = "<a href=\"{0}\" title=\"{1}\" class=\"{2}\"><i class=\"{3}\"></i></a>";

        public const string GridBoolTemplateElement = "ELEMENT_TEMPLATE";
        public const string GridBoolTemplate = "<input type='checkbox' disabled='disabled' class='custom-checkbox' {{if ${ELEMENT_TEMPLATE} }}checked='checked'{{/if}} ></input>";

        public static string PreviewButton(string url)
        {
            return EditButton(url, CommonResources.btnPreview, "grid-button button-margin", "fas fa-eye");
        }

        public static string EditButton(string url)
        {
            return EditButton(url, CommonResources.btnEdit, "grid-button button-margin", "fa fa-edit");
        }

        public static string EditButton(string url, string title, string classAttr, string iconClass)
        {
            return string.Format(CommonAnchorHrefTemplate, url, title, classAttr, iconClass);
        }
    }
}
