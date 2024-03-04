using UnityEngine;

namespace View.UI.MainMenus
{
    [CreateAssetMenu(fileName = "Level Info", menuName = "GamePlay/ New Level Info")]
    public class LevelInfo : ScriptableObject
    {
        [SerializeField] private string id;

        public string ID => id;
    }
}