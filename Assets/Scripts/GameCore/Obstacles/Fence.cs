#nullable enable

using System;
using System.Collections;
using GameCore.Players;
using UnityEngine;

namespace GameCore
{
    [RequireComponent(typeof(Collider2D))]
    public class Fence : MonoBehaviour
    {
        public event Action OnGarbageIsCrash = null!;
        
        [SerializeField] private Collider2D triggerCollider = null!;
        [SerializeField] private float delay = 1f;
        
        private void Awake()
        {
            triggerCollider = GetComponent<Collider2D>()!;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            StartCoroutine(GarbageIsCrash());
        }

        private IEnumerator GarbageIsCrash()
        {
            yield return new WaitForSeconds(delay);
            OnGarbageIsCrash.Invoke();
        }
    }
}