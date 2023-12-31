using UnityEngine;
using UnityEngine.UI;
using LevelDataSystem;

namespace UISystem
{
    public class LevelWinUI : MonoBehaviour
    {
        [SerializeField] private Button nextLevelButton;

        private void OnEnable()
        {
            nextLevelButton.onClick.AddListener(LevelManager.Instance.LevelWin);
            nextLevelButton.onClick.AddListener(()=>gameObject.SetActive(false));
        }

        private void OnDisable()
        {
            nextLevelButton.onClick.RemoveAllListeners();
        }
    }
}
