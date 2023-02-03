using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Services;
using Elsa.Services.Models;
using log4net;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace ITS.Callback.Web.Elsa.Activities
{
    [Activity(
           DisplayName = "SetOutputTrueOrFalse",
           Description = "SetOutputTrueOrFalse")]
    public class SetOutputTrueOrFalse : Activity
    {
        [ActivityOutput] public bool Output { get; set; }

        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            string outputFromContext = context.GetVariable<string>("ContextOutput");
            Output = string.IsNullOrEmpty(outputFromContext) ? false : outputFromContext.Equals("true", StringComparison.InvariantCultureIgnoreCase);
            return Done();
        }
    }
}
