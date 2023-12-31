using UnityEngine;

namespace UISystem
{
    [DefaultExecutionOrder(-3)]
    public class MainUI : MonoBehaviour
    {
        [SerializeField] private GameUI GameUI;
        [SerializeField] private LevelWinUI LevelEndUI;

        public static MainUI Instance { get => instance; set => instance = value; }
        private static MainUI instance;

        private void Awake()
        {
            if (instance)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            Application.targetFrameRate = 60;
        }

        public void SetLevelCount(int levelCount)
        {
            GameUI.SetLevelCount(levelCount);
        }

        public void LevelWin()
        {
            LevelEndUI.gameObject.SetActive(true);
        }
    }
}
