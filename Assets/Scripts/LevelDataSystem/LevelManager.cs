using UnityEngine;
using FieldSystem;
using System.Collections.Generic;
using static FieldSystem.FieldsManager;

namespace LevelDataSystem
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Transform levelParent;
        [SerializeField] private List<LevelData> levels;

        private FieldsManager fieldsManager = new FieldsManager();
        private int levelIndex = 0;

        private void Awake()
        {
            levelIndex = LoadLevelIndex();

            if(levelIndex >= 0 && levelIndex < levels.Count)
            {
                LevelData level = levels[levelIndex];
                CreateLevel(level);
            }
        }

        private void CreateLevel(LevelData level)
        {
            foreach (var fieldData in level.FieldModels)
            {
                // Create FieldViewer and FieldController
                var fieldViewer = Instantiate(level.FieldPrefab, levelParent);
                FieldController fieldController = new FieldController(fieldData.FieldModel, fieldViewer);

                FieldHolder fieldHolder = new FieldHolder(fieldController, fieldViewer);
                fieldsManager.FieldsHolder.Add(fieldHolder);
            }
        }

        private void ResetLevel()
        {

        }

        private void NextLevel()
        {
            levelIndex++;
            SaveLevelIndex();
        }

        private int LoadLevelIndex()
        {
            return PlayerPrefs.GetInt("LevelIndex", 0);
        }

        private void SaveLevelIndex()
        {
            PlayerPrefs.SetInt("LevelIndex", levelIndex);
        }
    }
}