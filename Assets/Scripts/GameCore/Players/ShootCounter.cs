using System;
using UnityEngine;

namespace GameCore.Players
{
    [RequireComponent(typeof(Garbage))]
    public class ShootCounter : MonoBehaviour
    {
        public event Action<int> ChangeCounter = null!;

        private Garbage garbage;
        private int count = 0;
        
        private void Awake()
        {
            garbage = GetComponent<Garbage>()!;
            garbage.OnShoot += OnShoot;
        }

        private void OnDestroy()
        {
            garbage.OnShoot -= OnShoot;
        }

        private void OnShoot()
        {
            count++;
            ChangeCounter.Invoke(count);
        }
    }
}