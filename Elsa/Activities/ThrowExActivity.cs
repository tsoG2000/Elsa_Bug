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
        DisplayName = "ThrowExActivity",
        Description = "ThrowExActivity")]
    public class ThrowExActivity : Activity
    {
        [ActivityOutput] public bool Output { get; set; }

        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            throw new Exception("AAAAAAA");   //COMMENT THIS LINE AFTER THE FIRST PASS
            context.SetVariable("ContextOutput", "true");
            return Done();
        }
    }
}
