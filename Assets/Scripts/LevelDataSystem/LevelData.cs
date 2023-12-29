using UnityEngine;
using FieldSystem;
using System.Collections.Generic;

namespace LevelDataSystem
{
    [CreateAssetMenu(menuName = "Level Data/Level", fileName = "Level")]
    public class LevelData : ScriptableObject
    {
        [Header("Prefabs")]
        public FieldViewer FieldPrefab;
        public GameObject PawnPrefab;
        public GameObject ClosePawnPrefab;
        [Header("Field Datas")]
        public List<FieldModelScriptable> FieldModels;
    }
}