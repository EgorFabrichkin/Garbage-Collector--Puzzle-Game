#nullable enable

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace View.UI.Popups
{
    public class WinPopup : MonoBehaviour
    {
        [SerializeField] private Canvas canvas = null!;
        [SerializeField] private Button button = null!;

        private void Awake()
        {
            canvas.EnsureNotNull("Canvas in WinPopup not specified");
            button.EnsureNotNull("Button in WinPopup not specified")
                .onClick.AddListener(
                    () => SceneManager.LoadScene(0)
                );
            gameObject.SetActive(false);
        }

        public void RenderPopup()
        {
            gameObject.SetActive(true);
        }
    }
}