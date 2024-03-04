using UnityEngine;

namespace GameCore.Players.Inputs
{
    public interface IGarbageInput
    {
        public bool IsTouch();
        
        public Vector2 StartTouchPosition();
        
        public Vector2 EndTouchPosition();
    }
}