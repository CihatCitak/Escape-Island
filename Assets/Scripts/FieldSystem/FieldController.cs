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

        /// <summary>
        /// Initializes a new instance of the FieldController class with a given field model and field viewer.
        /// Creates pawns based on the field model's column and position data, skipping empty columns.
        /// </summary>
        /// <param name="fieldModel">The field model to be used for initializing the controller.</param>
        /// <param name="fieldViewer">The field viewer associated with the controller.</param>
        public FieldController(FieldModel fieldModel, FieldViewer fieldViewer)
        {
            // Create field from level data
            this.fieldViewer = fieldViewer;
            this.fieldModel = fieldModel.Clone();

            this.fieldViewer.transform.position = this.fieldModel.PositionInLevel;
            this.fieldViewer.transform.eulerAngles = new Vector3(0, this.fieldModel.YRotationOffset, 0);

            foreach (var fieldColumn in this.fieldModel.FieldColumns)
            {
                // if color type is empty we don't neet to create pawn
                if (fieldColumn.ColorType == ColorType.Empty)
                    continue;

                // create pawns for some color type
                foreach (var position in fieldColumn.Positions)
                {
                    // Using object pool for create pawn and do some special pawn settings
                    IPawn pawn = PawnPool.Instance.Dequeue();
                    pawn
                        .SetTransformParent(fieldViewer.GetPawnPositionParent())
                        .SetColor(fieldColumn.ColorType)
                        .SetLocalPosition(position);

                    fieldColumn.Pawns.Add(pawn);
                }
            }
        }

        /// <summary>
        /// Counts the number of pawn positions in the rightmost consecutive column with the same color type.
        /// </summary>
        /// <returns>The count of pawn positions in the rightmost consecutive column with the same color type, or 0 if no such column is found.</returns>
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

                if (fieldColumn.ColorType == colorType)
                    count += fieldColumn.Positions.Count;
                else
                    break;
            }

            return count;
        }

        // <summary>
        /// Counts the number of empty pawn positions in the rightmost consecutive column in the field model.
        /// </summary>
        /// <returns>The count of empty pawn positions in the rightmost consecutive column.</returns>
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

        /// <summary>
        /// Returns the color type of the rightmost non-empty column in the field model, or ColorType.Empty if all columns are empty.
        /// </summary>
        /// <returns>The color type of the next non-empty column or ColorType.Empty if all columns are empty.</returns>
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

        /// <summary>
        /// Adds pawns to the field columns based on the provided pawn matrix, starting from the leftmost column.
        /// </summary>
        /// <param name="pawnMatrix">The matrix of pawns to be added to the field columns.</param>
        public void AddPawns(List<List<IPawn>> pawnMatrix)
        {
            ColorType colorType = pawnMatrix[0][0].ColorType;
            int matrixIndex = 0;

            for (int i = 0; i < fieldModel.FieldColumns.Count; i++)
            {
                var fieldColumn = fieldModel.FieldColumns[i];
                
                if(fieldColumn.ColorType == ColorType.Empty)
                {
                    fieldColumn.ColorType = colorType;
                    fieldColumn.Pawns.Clear();
                    fieldColumn.Pawns.AddRange(new List<IPawn>(pawnMatrix[matrixIndex]));

                    for (int j = 0; j < pawnMatrix[matrixIndex].Count; j++)
                    {
                        var pawn = pawnMatrix[matrixIndex][j];
                        pawn.SetTransformParent(fieldViewer.GetPawnPositionParent());
                        pawn.SetLocalPosition(fieldColumn.Positions[j]);
                    }

                    matrixIndex++;
                    if(matrixIndex == pawnMatrix.Count)
                        break;
                }
            }

            pawnMatrix.Clear();
        }

        /// <summary>
        /// Removes pawns from the rightmost consecutive column with the same color type.
        /// Returns a matrix containing the removed pawns.
        /// </summary>
        /// <returns>A matrix containing the removed pawns or an empty matrix if no pawns were removed.</returns>
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
                else if(fieldColumn.ColorType != ColorType.Empty)
                    break;
            }

            return removePawnMatrix;
        }
    }
}
