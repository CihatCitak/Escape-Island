using UnityEngine;

namespace FieldSystem
{
    public class FieldController
    {
        public FieldViewer FieldViewer { get => fieldViewer; set => fieldViewer = value; }
        private FieldViewer fieldViewer;

        private FieldModel fieldModel;

        public FieldController(FieldModel fm, FieldViewer fv)
        {
            fieldViewer = fv;
            fieldModel = fm.Clone();

            fieldViewer.transform.position = fieldModel.PositionInLevel;
            fieldViewer.transform.eulerAngles = new Vector3(0, fieldModel.YRotationOffset, 0);
        }

        // todo: Transfer ile ilgili işlemler burada yapılacak
    }
}
