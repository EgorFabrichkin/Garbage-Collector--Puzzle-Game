using UnityEngine;

namespace GameCore.Players.Inputs
{
    public abstract class GarbageInputBase : MonoBehaviour, IGarbageInput
    {
        public abstract bool IsTouch();
        
        public abstract Vector2 StartTouchPosition();

        public abstract Vector2 EndTouchPosition();
    }
}