#nullable enable

using System;
using UnityEngine;

namespace GameCore.Achievements
{
    [RequireComponent(typeof(Collider2D))]
    public class Achievement : MonoBehaviour
    {
        public event Action OnCollected = null!; 

        [SerializeField] private Collider2D triggerCollider = null!;
        
        private void Awake()
        {
            triggerCollider = GetComponent<Collider2D>()!;
            
            if (!triggerCollider.isTrigger)
                throw new Exception("Achievement must be trigger");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnCollected.Invoke();
        }
    }
}