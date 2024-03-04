#nullable enable

using System;
using System.Collections;
using UnityEngine;

namespace GameCore.Baskets
{
    [RequireComponent(typeof(Collider2D))]
    public class Basket : MonoBehaviour
    {
        public event Action OnGarbageInBasket = null!;

        [SerializeField] private Collider2D triggerCollider = null!;
        [SerializeField] private float delay = 1f;
        
        private void Awake()
        {
            if (!triggerCollider.isTrigger)
            {
                throw new Exception("Basket must be trigger");
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            StartCoroutine(GarbageInBasket());
        }

        private IEnumerator GarbageInBasket()
        {
            yield return new WaitForSeconds(delay);
            OnGarbageInBasket.Invoke();
        }
    }
}