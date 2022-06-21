using MJ_CAIS.Common;

namespace MJ_CAIS.WebPortal.Public.Utils
{
    public static class ButtonTemplates
    {
        private const string CommonAnchorHrefTemplate = "<a href=\"{0}\" title=\"{1}\" class=\"{2}\"><i class=\"{3}\"></i></a>";

        public static string PreviewButton(string url)
        {
            return EditButton(url, CommonResources.btnPreview, "grid-button button-margin", "fas fa-eye");
        }

        public static string EditButton(string url, string title, string classAttr, string iconClass)
        {
            return string.Format(CommonAnchorHrefTemplate, url, title, classAttr, iconClass);
        }
    }
}
