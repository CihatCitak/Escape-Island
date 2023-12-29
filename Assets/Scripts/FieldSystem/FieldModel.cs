using System;
using UnityEngine;
using System.Collections.Generic;

namespace FieldSystem
{
    [Serializable]
    public class FieldModel
    {
        public Vector3 PositionInLevel = Vector3.zero;
        public List<FieldColumn> FieldColumns = new List<FieldColumn>();

        // if field is in right of the level then turn left
        // if field is in left of the level then turn right
        public float YRotationOffset => (PositionInLevel.x >= 0) ? -90f : 90f;

        public FieldModel Clone()
        {
            FieldModel clonedField = new FieldModel();
            clonedField.PositionInLevel = PositionInLevel;

            foreach (FieldColumn column in FieldColumns)
            {
                clonedField.FieldColumns.Add(column.Clone());
            }

            return clonedField;
        }
    }

    [Serializable]
    public class FieldColumn
    {
        public List<Vector3> Positions = new List<Vector3>();
        public ColorType ColorType = ColorType.Empty;
        public bool IsOpen = true;

        public FieldColumn Clone()
        {
            FieldColumn clonedColumn = new FieldColumn();
            clonedColumn.Positions.AddRange(Positions);
            clonedColumn.ColorType = ColorType;
            clonedColumn.IsOpen = IsOpen;

            return clonedColumn;
        }
    }
}
