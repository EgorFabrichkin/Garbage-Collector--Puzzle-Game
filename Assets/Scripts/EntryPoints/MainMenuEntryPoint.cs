#nullable enable

using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using View.UI.MainMenus;

namespace EntryPoints
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private LevelCard[] levelCards = null!;

        private void Awake()
        {
            if (levelCards.Length == 0)
                throw new Exception("Level Card not found");

            foreach (var level in levelCards)
            {
                level.OnLevelInfo += OnLevelInfo;
            }
        }

        private void OnDestroy()
        {
            foreach (var level in levelCards)
            {
                level.OnLevelInfo -= OnLevelInfo;
            }
        }

        private void OnLevelInfo(string levelID)
        {
            SceneManager.LoadScene(levelID);
        }
    }
}