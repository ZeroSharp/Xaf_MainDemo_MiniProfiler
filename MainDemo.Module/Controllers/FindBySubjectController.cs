using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using StackExchange.Profiling;
using System;

namespace MainDemo.Module.Controllers {
	public partial class FindBySubjectController : ViewController {
		public FindBySubjectController()
			: base() {
			InitializeComponent();
			RegisterActions(components);
		}
		private void FindBySubjectAction_Execute(object sender, ParametrizedActionExecuteEventArgs e) {
            var profiler = MiniProfiler.Current;
            using (profiler.Step("FindBySubject_Execute()")) // doesn't matter if profiler is null
            {
                IObjectSpace objectSpace = Application.CreateObjectSpace();
                string paramValue = e.ParameterCurrentValue as string;
                if (!string.IsNullOrEmpty(paramValue))
                {
                    paramValue = "%" + paramValue + "%";
                }
                object obj = objectSpace.FindObject(((ListView)View).ObjectTypeInfo.Type,
                    new BinaryOperator("Subject", paramValue, BinaryOperatorType.Like));
                if (obj != null)
                {
                    e.ShowViewParameters.CreatedView = Application.CreateDetailView(objectSpace, obj);
                }
            }
		}
	}
}
