using LevelDataSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private TextMeshProUGUI levelCountText;

        private const string LevelStr = "Level";

        private void OnEnable()
        {
            restartButton.onClick.AddListener(LevelManager.Instance.ResetLevel);
        }

        private void OnDisable()
        {
            restartButton.onClick.RemoveAllListeners();
        }

        public void SetLevelCount(int levelCount)
        {
            levelCountText.SetText(LevelStr + " " + levelCount);
        }
    }
}
