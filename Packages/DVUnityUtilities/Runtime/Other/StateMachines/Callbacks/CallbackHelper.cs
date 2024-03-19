using DVUnityUtilities.Other.StateMachines.Definitions;
using DVUnityUtilities.Other.StateMachines.Events;
using System.Collections.Generic;

namespace DVUnityUtilities.Other.StateMachines.Callbacks
{
    internal static class CallbackHelper
    {
        public static void HandleStateChanged(StateChangeEventArgs args, List<StateChangedAction> stateActions)
        {
            ExecuteAllStateActions(stateActions, args.StateMachine);
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