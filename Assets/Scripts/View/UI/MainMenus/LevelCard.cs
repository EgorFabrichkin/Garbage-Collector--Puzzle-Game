#nullable enable

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace View.UI.MainMenus
{
    [RequireComponent(typeof(Image))]
    public class LevelCard : MonoBehaviour, IPointerClickHandler
    {
        public event Action<string> OnLevelInfo = null!; 
        
        [SerializeField] private LevelInfo levelInfo = null!;
        
        private void Awake()
        {
            levelInfo.EnsureNotNull("Level Info not specified");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnLevelInfo.Invoke(levelInfo.ID);
        }
    }
}