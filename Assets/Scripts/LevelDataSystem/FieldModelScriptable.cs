using FieldSystem;
using UnityEngine;

namespace LevelDataSystem
{
    [CreateAssetMenu(menuName = "Level Data/Field", fileName = "Field")]
    public class FieldModelScriptable : ScriptableObject
    {
        public FieldModel FieldModel;
    }
}
