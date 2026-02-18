using DevExpress.ExpressApp;
using DevExpress.ExpressApp.StateMachine;
using DevExpress.ExpressApp.StateMachine.NonPersistent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniCRM_KenilKhadela.Module.BusinessObjects
{
    public class LeadStatusStateMachine : StateMachine<Lead>, IStateMachineUISettings
    {

        private IState startState;

        public LeadStatusStateMachine(IObjectSpace objectSpace): base(objectSpace)
        {
            startState = new State(this, "Open", Lead.LeadProcessStageEnum.Open);
            IState qualified=new State(this, "Qualified", Lead.LeadProcessStageEnum.Qualified);
            IState proposed = new State(this, "Propose", Lead.LeadProcessStageEnum.Propose);
            IState developed= new State(this, "Develop", Lead.LeadProcessStageEnum.Develop);
            IState closed = new State(this, "Closed", Lead.LeadProcessStageEnum.Closed);

            startState.Transitions.Add(new Transition(qualified));
            qualified.Transitions.Add(new Transition(developed));
            qualified.Transitions.Add(new Transition(startState));
            developed.Transitions.Add(new Transition(proposed));
            developed.Transitions.Add(new Transition(qualified));
            proposed.Transitions.Add(new Transition(closed));
            proposed.Transitions.Add(new Transition(developed));
            closed.Transitions.Add(new Transition(proposed));

            States.Add(startState);
            States.Add(qualified);
            States.Add(developed);
            States.Add(proposed);
            States.Add(closed);
        }

        public override string Name => "Change Lead Status";
        public override string StatePropertyName => nameof(Lead.LeadProcessStage);
        public override IState StartState => startState;

        public bool ExpandActionsInDetailView => false;
    }
}