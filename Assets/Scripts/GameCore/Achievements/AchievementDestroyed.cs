using System;
using UnityEngine;

namespace GameCore.Achievements
{
    [RequireComponent(typeof(Achievement))]
    public class AchievementDestroyed : MonoBehaviour
    {
        [SerializeField] private float delay = 1f;

        private Achievement _achievement;
        
        private void Awake()
        {
            _achievement = GetComponent<Achievement>();
            _achievement.OnCollected += OnCollected;
        }

        private void OnDestroy()
        {
            _achievement.OnCollected -= OnCollected;
        }

        private void OnCollected()
        {
            Destroy(gameObject, delay);
        }
    }
}