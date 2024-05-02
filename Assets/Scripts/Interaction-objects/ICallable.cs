namespace Interaction_objects
{
    public interface ICallable
    {
        public void BeginInteraction();
        public void StayInInteraction();
        public void EndInteractions();
    }
}