using UnityEngine;
using FieldSystem;
using System.Collections.Generic;

namespace LevelDataSystem
{
    [CreateAssetMenu(menuName = "Level Data/Level", fileName = "Level")]
    public class LevelData : ScriptableObject
    {
        public List<FieldModelScriptable> FieldModels;
    }
}