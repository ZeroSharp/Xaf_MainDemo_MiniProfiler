using System;
using System.Collections.Generic;
using System.Web.UI;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Templates;
using DevExpress.ExpressApp.Web.Templates.ActionContainers;
using StackExchange.Profiling;

public partial class Default : BaseXafPage {

    protected override void OnLoad(EventArgs e)
    {
        var profiler = MiniProfiler.Current;
        using (profiler.Step("ASP.NET: Page_Load(Default)"))
        {
            base.OnLoad(e);
        }
    }

    protected override ContextActionsMenu CreateContextActionsMenu() {
        return new ContextActionsMenu(this, "Edit", "RecordEdit", "ObjectsCreation", "ListView", "Reports");
    }
    public override Control InnerContentPlaceHolder {
        get {
            return Content;
        }
    }
}
