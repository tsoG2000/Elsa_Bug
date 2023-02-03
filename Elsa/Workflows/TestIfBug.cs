using Elsa;
using Elsa.Activities.ControlFlow;
using Elsa.Builders;
using ITS.Callback.Web.Elsa.Activities;

namespace ITS.Callback.Web.Elsa.Workflows
{
    public class TestIfBug : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .StartWith<SetOutputTrueOrFalse>().WithId("SetOutputTrueOrFalse").WithName("SetOutputTrueOrFalse")
               .If(
                    condition: context => context.GetInput<bool>(),
                    activity: ifActivity =>
                    {
                        ifActivity
                            .When(OutcomeNames.True)
                            .Then<DoneActivity>().WithId("DoneActivity").WithName("DoneActivity");

                        ifActivity
                            .When(OutcomeNames.False)
                            .Then<ThrowExActivity>().WithId("ThrowExActivity").WithName("ThrowExActivity")
                            .ThenNamed("SetOutputTrueOrFalse");
                    }
                );
        }
    }
}
