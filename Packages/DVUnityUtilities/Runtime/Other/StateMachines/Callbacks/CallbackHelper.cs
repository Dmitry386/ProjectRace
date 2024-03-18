using DVUnityUtilities.Other.StateMachines.Definitions;
using DVUnityUtilities.Other.StateMachines.Events;
using System.Collections.Generic;

namespace DVUnityUtilities.Other.StateMachines.Callbacks
{
    internal static class CallbackHelper
    {
        public static void HandleStateChanged(StateChangeEventArgs args, List<StateChangedAction> stateActions)
        {
            foreach (var stateAction in stateActions)
            {
                HandleStateChanged(args, stateAction);
            }
        }

        public static void HandleStateChanged(StateChangeEventArgs args, StateChangedAction stateAction)
        {
            if (stateAction.OnStateStatus == StateChangeStatus.Inactive)
            {
                if (stateAction.StateName == args.OldState)
                {
                    ExecuteStateActions(stateAction);
                }
            }
            else if (stateAction.OnStateStatus == StateChangeStatus.Active)
            {
                if (stateAction.StateName == args.NewState)
                {
                    ExecuteStateActions(stateAction);
                }
            }
        }

        public static void ExecuteStateActions(StateChangedAction stateAction)
        {
            stateAction.ToDo?.Invoke();
        }

        public static void ExecuteAllStateActions(List<StateChangedAction> actions, DefaultStateMachine sm)
        {
            foreach (var action in actions)
            {
                if (action.OnStateStatus == StateChangeStatus.Active)
                {
                    if (sm.IsState(action.StateName))
                    {
                        CallbackHelper.ExecuteStateActions(action);
                    }
                }
                else if (action.OnStateStatus == StateChangeStatus.Inactive)
                {
                    if (!sm.IsState(action.StateName))
                    {
                        CallbackHelper.ExecuteStateActions(action);
                    }
                }
            }
        }
    }
}