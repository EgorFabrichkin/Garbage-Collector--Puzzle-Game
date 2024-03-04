#nullable enable

using System;
using GameCore;
using GameCore.Achievements;
using GameCore.Baskets;
using GameCore.Players;
using UnityEngine;
using Utils;
using View.UI;

namespace EntryPoints
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private Garbage garbage = null!;
        [SerializeField] private Basket basket = null!;
        [SerializeField] private Fence[] fences = null!;
        [SerializeField] private Achievement[] achievements = null!;
        [SerializeField] private GameplayInterfaceManager gameplayInterfaceManager = null!;

        private ShootCounter _shootCounter = null!;

        private void Awake()
        {
            garbage.EnsureNotNull("Garbage not found");
            basket.EnsureNotNull("Basket not found");
            if (fences.Length == 0)
                throw new Exception("Fences not found");
            if (achievements.Length < 3)
                throw new Exception("Achievements not found");
            gameplayInterfaceManager.EnsureNotNull("Interface Manager not found");
            _shootCounter = garbage.GetComponent<ShootCounter>()!;
        }

        private void OnEnable()
        {
            _shootCounter.ChangeCounter += OnChangeShootCounter;
            basket.OnGarbageInBasket += OnGarbageInBasket;
            foreach (var fence in fences)
                fence.OnGarbageIsCrash += OnGarbageIsCrash;
            foreach (var achievement in achievements)
                achievement.OnCollected += OnCollectedAchievement;
        }

        private void OnDestroy()
        {
            _shootCounter.ChangeCounter -= OnChangeShootCounter;
            basket.OnGarbageInBasket -= OnGarbageInBasket;
            foreach (var fence in fences)
                fence.OnGarbageIsCrash -= OnGarbageIsCrash;
            foreach (var achievement in achievements)
                achievement.OnCollected -= OnCollectedAchievement;
        }
        
        private void OnChangeShootCounter(int value)
        {
            gameplayInterfaceManager.InitHUD(value);
        }

        private void OnGarbageInBasket()
        {
            gameplayInterfaceManager.InitWinPopup();
        }
        
        private void OnGarbageIsCrash()
        {
            garbage.enabled = false;
            gameplayInterfaceManager.InitFailPopup();
        }

        private int count = 0;
        
        private void OnCollectedAchievement()
        {
            count++;
            Debug.Log($"{count}");
            gameplayInterfaceManager.ChangeAchievementCount(count);
        }
    }
}
