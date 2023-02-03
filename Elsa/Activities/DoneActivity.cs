using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Services;
using Elsa.Services.Models;
using log4net;
using System.Reflection;
using System.Threading.Tasks;

namespace ITS.Callback.Web.Elsa.Activities
{
    [Activity(
        DisplayName = "DoneActivity",
        Description = "DoneActivity")]
    public class DoneActivity : Activity
    {
        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            return Done();
        }
    }
}
