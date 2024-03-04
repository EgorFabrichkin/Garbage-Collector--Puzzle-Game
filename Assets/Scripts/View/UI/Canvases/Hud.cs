using UnityEngine;
using UnityEngine.UI;

namespace View.UI.Canvases
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private Text shootCounter;
        [SerializeField] private Text achievementCounter;

        public void ChangeShootCount(int count)
        {
            shootCounter.text = $"{count}";
        }
        
        public void ChangeAchievementCount(int count)
        {
            achievementCounter.text = $"{count}";
        }
    }
}