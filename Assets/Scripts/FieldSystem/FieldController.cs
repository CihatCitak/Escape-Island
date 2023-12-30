using PawnSystem;
using UnityEngine;
using ColorSystem;

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
                }
            }
        }

        // todo: Transfer ile ilgili işlemler burada yapılacak
    }
}
