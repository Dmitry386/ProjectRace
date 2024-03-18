namespace DVUnityUtilities.Other.StateMachines.Events
{
    public class StateChangeEventArgs
    {
        public DefaultStateMachine StateMachine;
        public string OldState;
        public string NewState;
        public object[] Args;

        public bool IsHaveNewState()
        {
            return OldState != null && NewState != OldState;
        }
    }
}