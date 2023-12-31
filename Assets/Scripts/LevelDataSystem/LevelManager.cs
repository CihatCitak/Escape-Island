using System;
using UnityEngine;
using FieldSystem;
using System.Collections.Generic;
using UISystem;

namespace LevelDataSystem
{
    [DefaultExecutionOrder(-1)]
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private FieldsManager fieldsManager;
        [SerializeField] private Transform levelParent;
        [SerializeField] private List<LevelData> levels;

        public Action AllPoolObjectReturnsPool;

        public static LevelManager Instance { get => instance; set => instance = value; }
        private static LevelManager instance;

        private int levelIndex = 0;

        private void Awake()
        {
            if (instance)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            CreateLevel();
        }

        private void CreateLevel()
        {
            levelIndex = LoadLevelIndex();

            if (levelIndex >= 0 && levelIndex < levels.Count)
            {
                LevelData level = levels[levelIndex];
                CreateLevel(level);
            }

            MainUI.Instance.SetLevelCount(levelIndex + 1);
        }

        /// <summary>
        /// Creates fields and associated controllers based on the provided level data.
        /// </summary>
        /// <param name="level">The level data containing information about the fields.</param>
        private void CreateLevel(LevelData level)
        {
            foreach (var fieldData in level.FieldModels)
            {
                var fieldViewer = FieldPool.Instance.Dequeue();
                fieldViewer.transform.parent = levelParent;
                FieldController fieldController = new FieldController(fieldData.FieldModel, fieldViewer);

                FieldHolder fieldHolder = new FieldHolder(fieldController, fieldViewer);
                fieldsManager.FieldsHolder.Add(fieldHolder);
            }
        }

        public void ResetLevel()
        {
            AllPoolObjectReturnsPool?.Invoke();
            CreateLevel();
        }

        public void LevelWin()
        {
            AllPoolObjectReturnsPool?.Invoke();

            NextLevel();
            CreateLevel();
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
