using HDBH.Language;
using System.Web.Mvc;
public static class HTMLButton
{
    private static string cssCreateButton = "btn btn-primary";
    private static string cssUpdateButton = "btn btn-success";
    private static string cssRefreshButton = "btn btn-info";
    private static string cssCancelButton = "btn btn-warning";
    private static string cssRemoveButton = "btn btn-danger";
    private static string cssSearchButton = "btn btn-search";
    private static string cssExportButton = "btn btn-ExportExcel";

    private static string createIco = "<i class=\"fa fa-plus\" aria-hidden=\"true\"></i>&nbsp;";
    private static string updateIco = "<i class=\"fa fa-pencil\" aria-hidden=\"true\"></i>&nbsp;";
    private static string refeshIco = "<i class=\"fa fa-refresh\" aria-hidden=\"true\"></i>&nbsp;";
    private static string cancelIco = "<i class=\"fa fa-ban\" aria-hidden=\"true\"></i>&nbsp;";
    private static string removeIco = "<i class=\"fa fa-trash\" aria-hidden=\"true\"></i>&nbsp;";

    private static string searchIco = "<i class=\"fa fa-search\" aria-hidden=\"true\"></i>&nbsp;";
    private static string exportIco = "<i class=\"fa fa-download\" aria-hidden=\"true\"></i>&nbsp;";

    public static MvcHtmlString CreateButton(this HtmlHelper helper,  string ButtonName = "", string ButtonText = "", string cssClass = "", string ico = "")
    {
        TagBuilder tag = new TagBuilder("Button");
        tag.MergeAttribute("name", TagBuilder.CreateSanitizedId(ButtonName));//This will remove spaces and restricted characters.
        tag.GenerateId(ButtonName);// This will generate ID for button based on Name property.
        if(string.IsNullOrEmpty(ButtonText))
        {
            ButtonText = Localization.Get("lbl_AddNew");
        }

        tag.InnerHtml = (!string.IsNullOrEmpty(ico) ? ico : createIco) + ButtonText; // To set Button Text
        tag.AddCssClass(cssCreateButton + " " + cssClass);// To set CSS Class attribute
        return MvcHtmlString.Create(tag.ToString());
    }

    public static MvcHtmlString UpdateButton(this HtmlHelper helper, string ButtonName = "", string ButtonText = "", string cssClass = "")
    {
        TagBuilder tag = new TagBuilder("Button");
        tag.MergeAttribute("name", TagBuilder.CreateSanitizedId(ButtonName));//This will remove spaces and restricted characters.
        tag.GenerateId(ButtonName);// This will generate ID for button based on Name property.
        if (string.IsNullOrEmpty(ButtonText))
        {
            ButtonText = Localization.Get("lbl_Update");
        }
        tag.InnerHtml = updateIco + ButtonText; // To set Button Text
        tag.AddCssClass(cssUpdateButton + " " + cssClass);// To set CSS Class attribute
        return MvcHtmlString.Create(tag.ToString());
    }
    public static MvcHtmlString RefreshButton(this HtmlHelper helper, string ButtonName = "", string ButtonText = "", string cssClass = "")
    {
        TagBuilder tag = new TagBuilder("Button");
        tag.MergeAttribute("name", TagBuilder.CreateSanitizedId(ButtonName));//This will remove spaces and restricted characters.
        tag.GenerateId(ButtonName);// This will generate ID for button based on Name property.
        if (string.IsNullOrEmpty(ButtonText))
        {
            ButtonText = Localization.Get("lbl_Refresh");
        }
        tag.InnerHtml = refeshIco + ButtonText; // To set Button Text
        tag.AddCssClass(cssRefreshButton + " " + cssClass);// To set CSS Class attribute
        return MvcHtmlString.Create(tag.ToString());
    }
    public static MvcHtmlString CancelButton(this HtmlHelper helper, string ButtonName = "", string ButtonText = "", string cssClass = "", bool isDisabled = false)
    {
        TagBuilder tag = new TagBuilder("Button");
        tag.MergeAttribute("name", TagBuilder.CreateSanitizedId(ButtonName));//This will remove spaces and restricted characters.
        tag.GenerateId(ButtonName);// This will generate ID for button based on Name property.
        if (string.IsNullOrEmpty(ButtonText))
        {
            ButtonText = Localization.Get("lbl_Cancel");
        }
        tag.InnerHtml = cancelIco + ButtonText; // To set Button Text
        tag.AddCssClass(cssCancelButton + " " + cssClass);// To set CSS Class attribute
        if(isDisabled)
        {
            tag.MergeAttribute("disabled", "True");
        }
      
        return MvcHtmlString.Create(tag.ToString());
    }
    public static MvcHtmlString DeleteButton(this HtmlHelper helper,  string ButtonName = "", string ButtonText = "", string cssClass = "")
    {
        TagBuilder tag = new TagBuilder("Button");
        tag.MergeAttribute("name", TagBuilder.CreateSanitizedId(ButtonName));//This will remove spaces and restricted characters.
        tag.GenerateId(ButtonName);// This will generate ID for button based on Name property.
        if (string.IsNullOrEmpty(ButtonText))
        {
            ButtonText = Localization.Get("lbl_Delete");
        }
        tag.InnerHtml = removeIco + ButtonText; // To set Button Text
        tag.AddCssClass(cssRemoveButton + " " + cssClass);// To set CSS Class attribute
        return MvcHtmlString.Create(tag.ToString());
    }

    public static MvcHtmlString ResetButton(this HtmlHelper helper,  string ButtonName = "", string ButtonText = "", string cssClass = "" )
    {
        TagBuilder tag = new TagBuilder("Button");
        tag.MergeAttribute("name", TagBuilder.CreateSanitizedId(ButtonName));//This will remove spaces and restricted characters.
        tag.GenerateId(ButtonName);// This will generate ID for button based on Name property.
        if (string.IsNullOrEmpty(ButtonText))
        {
            ButtonText = Localization.Get("lbl_Reset");
        }
        tag.InnerHtml = refeshIco + ButtonText; // To set Button Text
        tag.AddCssClass(cssRefreshButton + " " + cssClass);// To set CSS Class attribute
        return MvcHtmlString.Create(tag.ToString());
    }

    public static MvcHtmlString SearchButton(this HtmlHelper helper, string ButtonName = "", string ButtonText = "", string cssClass = "")
    {
        TagBuilder tag = new TagBuilder("Button");
        tag.MergeAttribute("name", TagBuilder.CreateSanitizedId(ButtonName));//This will remove spaces and restricted characters.
        tag.GenerateId(ButtonName);// This will generate ID for button based on Name property.
        if (string.IsNullOrEmpty(ButtonText))
        {
            ButtonText = Localization.Get("lbl_search");
        }
        tag.InnerHtml = searchIco + ButtonText; // To set Button Text
        tag.AddCssClass(cssSearchButton + " " + cssClass);// To set CSS Class attribute
        return MvcHtmlString.Create(tag.ToString());
    }    

    public static MvcHtmlString ExportButton(this HtmlHelper helper, string ButtonName = "", string ButtonText = "", string cssClass = "")
    {
        TagBuilder tag = new TagBuilder("Button");
        tag.MergeAttribute("name", TagBuilder.CreateSanitizedId(ButtonName));//This will remove spaces and restricted characters.
        tag.GenerateId(ButtonName);// This will generate ID for button based on Name property.
        if (string.IsNullOrEmpty(ButtonText))
        {
            ButtonText = Localization.Get("lbl_exportExcel");
        }
        tag.InnerHtml = exportIco + ButtonText; // To set Button Text
        tag.AddCssClass(cssExportButton + " " + cssClass);// To set CSS Class attribute
        return MvcHtmlString.Create(tag.ToString());
    }
}