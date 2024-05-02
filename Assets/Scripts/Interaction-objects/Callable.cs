using UnityEngine;

namespace Interaction_objects
{
    public abstract class Callable : MonoBehaviour
    {
        public abstract void BeginInteraction();
        public abstract void StayInInteraction();
        public abstract void EndInteractions();
    }
}