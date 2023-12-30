using PawnSystem;
using UnityEngine;
using ColorSystem;
using System;
using System.Collections.Generic;

namespace FieldSystem
{
    public class FieldController
    {
        public FieldViewer FieldViewer { get => fieldViewer; set => fieldViewer = value; }
        private FieldViewer fieldViewer;

        private FieldModel fieldModel;

        public FieldController(FieldModel fieldModel, FieldViewer fieldViewer)
        {
            this.fieldViewer = fieldViewer;
            this.fieldModel = fieldModel.Clone();

            this.fieldViewer.transform.position = this.fieldModel.PositionInLevel;
            this.fieldViewer.transform.eulerAngles = new Vector3(0, this.fieldModel.YRotationOffset, 0);

            foreach (var fieldColumn in this.fieldModel.FieldColumns)
            {
                if (fieldColumn.ColorType == ColorType.Empty)
                    continue;

                foreach (var position in fieldColumn.Positions)
                {
                    IPawn pawn = PawnPool.Instance.Dequeue();
                    pawn
                        .SetTransformParent(fieldViewer.GetPawnPositionParent())
                        .SetColor(fieldColumn.ColorType)
                        .SetLocalPosition(position);

                    fieldColumn.Pawns.Add(pawn);
                }
            }
        }

        public int GetNextMovePawnCount()
        {
            int count = 0;
            ColorType colorType = ColorType.Empty;

            for (int i = fieldModel.FieldColumns.Count - 1; i >= 0; i--)
            {
                var fieldColumn = fieldModel.FieldColumns[i];

                if (fieldColumn.ColorType == ColorType.Empty)
                    continue;

                if (colorType == ColorType.Empty)
                { 
                    colorType = fieldColumn.ColorType;
                    count += fieldColumn.Positions.Count;
                    continue;
                }

                if(fieldColumn.ColorType == colorType)
                    count += fieldColumn.Positions.Count;
            }

            return count;
        }

        public int GetEmptyPawnPositionCount()
        {
            int count = 0;

            for (int i = fieldModel.FieldColumns.Count - 1; i >= 0; i--)
            {
                var fieldColumn = fieldModel.FieldColumns[i];

                if (fieldColumn.ColorType == ColorType.Empty)
                    count += fieldColumn.Positions.Count;
                else
                    break;
            }

            return count;
        }

        public ColorType GetNextColumnColorType()
        {
            for (int i = fieldModel.FieldColumns.Count - 1; i >= 0; i--)
            {
                var fieldColumn = fieldModel.FieldColumns[i];

                if (fieldColumn.ColorType != ColorType.Empty)
                    return fieldColumn.ColorType;
            }

            return ColorType.Empty;
        }

        public void AddPawns(List<List<IPawn>> pawnMatrix)
        {
            ColorType colorType = GetNextColumnColorType();
            int matrixIndex = 0;

            for (int i = 0; i < fieldModel.FieldColumns.Count; i++)
            {
                var fieldColumn = fieldModel.FieldColumns[i];
                
                if(fieldColumn.ColorType == ColorType.Empty)
                {
                    fieldColumn.ColorType = colorType;
                    fieldColumn.Pawns.AddRange(pawnMatrix[matrixIndex]);

                    for (int j = 0; j < pawnMatrix[matrixIndex].Count; j++)
                    {
                        var pawn = pawnMatrix[matrixIndex][j];
                        pawn.SetTransformParent(fieldViewer.GetPawnPositionParent());
                        pawn.SetLocalPosition(fieldColumn.Positions[j]);
                    }

                    matrixIndex++;
                }
            }
        }

        public List<List<IPawn>> RemovePawns()
        {
            ColorType colorType = GetNextColumnColorType();
            List<List<IPawn>> removePawnMatrix = new List<List<IPawn>>();

            for (int i = fieldModel.FieldColumns.Count - 1; i >= 0; i--)
            {
                var fieldColumn = fieldModel.FieldColumns[i];

                if (fieldColumn.ColorType == colorType)
                {
                    fieldColumn.ColorType = ColorType.Empty;
                    removePawnMatrix.Add(new List<IPawn>(fieldColumn.Pawns));
                    fieldColumn.Pawns.Clear();
                }
            }

            return removePawnMatrix;
        }
    }
}
