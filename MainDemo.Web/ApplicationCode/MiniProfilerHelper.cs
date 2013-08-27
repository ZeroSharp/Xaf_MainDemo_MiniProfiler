using System;
using System.Collections.Generic;
using System.Web;
using DevExpress.ExpressApp.Web.TestScripts;
using DevExpress.Xpo.DB;
using StackExchange.Profiling;

namespace MainDemo.Web
{
    public static class MiniProfilerHelper
    {
        public static bool IsEnabled()
        {
            // While we are testing let's always return true
            return true;

            // We should not profile if we are EasyTesting
            if (TestScriptsManager.EasyTestEnabled == true)
                return false;

            // We could choose to profile only local requests
            if (HttpContext.Current.Request.IsLocal)
                return true;

            // Or base on cookie
            HttpCookie miniProfileCookie = HttpContext.Current.Request.Cookies["MainDemoMiniProfiler"];
            return miniProfileCookie != null && miniProfileCookie.Value != "0";
        }

        public static void RegisterPathsToIgnore()
        {
            List<String> ignoredByMiniProfiler = new List<String>(MiniProfiler.Settings.IgnoredPaths);
            // these are a substring search so wildcards are not supported
            ignoredByMiniProfiler.Add("SessionKeepAliveReconnect.aspx");
            ignoredByMiniProfiler.Add("TemplateScripts.js");
            ignoredByMiniProfiler.Add("EasyTestJavaScripts.js");
            ignoredByMiniProfiler.Add("MoveFooter.js");
            ignoredByMiniProfiler.Add("DX_PivotGrid_Styles.css");
            ignoredByMiniProfiler.Add("ImageResource.axd");
            ignoredByMiniProfiler.Add("TestControls.axd");
            MiniProfiler.Settings.IgnoredPaths = ignoredByMiniProfiler.ToArray();
        }

        public static string GetTableNamesFromStatements(BaseStatement[] baseStatements)
        {
            var profiler = MiniProfiler.Current;
            if (profiler != null)
            {
                string[] tableNamesArray = new string[baseStatements.Length];
                for (int i = 0; i < baseStatements.Length; i++)
                {
                    tableNamesArray[i] = baseStatements[i].TableName;
                }
                return String.Join<String>(", ", tableNamesArray);
            }
            return null;
        }
    }
}
