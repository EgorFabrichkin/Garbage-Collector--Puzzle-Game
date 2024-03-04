#nullable enable

using UnityEngine;
using Utils;
using View.UI.Canvases;
using View.UI.Popups;

namespace View.UI
{
    public class GameplayInterfaceManager : MonoBehaviour
    {
        [SerializeField] private Hud hud = null!;
        [SerializeField] private WinPopup winPopup = null!;
        [SerializeField] private FailPopup failPopup = null!;
        
        private void Awake()
        {
            hud.EnsureNotNull("HUD not specified");
            winPopup.EnsureNotNull("WinPopup not specified");
            failPopup.EnsureNotNull("FailPopup not specified");
        }

        public void InitHUD(int count)
        {
            hud.ChangeShootCount(count);
        }

        public void ChangeAchievementCount(int count)
        {
            hud.ChangeShootCount(count);
        }

        public void InitWinPopup()
        {
            winPopup.RenderPopup();
        }

        public void InitFailPopup()
        {
            failPopup.RenderPopup();
        }
    }
}