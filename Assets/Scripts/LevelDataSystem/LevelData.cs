using UnityEngine;
using FieldSystem;
using System.Collections.Generic;

namespace LevelDataSystem
{
    [CreateAssetMenu(menuName = "Level Data/Level", fileName = "Level")]
    public class LevelData : ScriptableObject
    {
        public int LevelIndex = 0;
        public GameObject PawnPrefab;
        public GameObject ClosePawnPrefab;
        public List<FieldModelScriptable> FieldDatas;
    }
}