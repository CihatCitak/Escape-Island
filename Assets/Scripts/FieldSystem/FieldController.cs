namespace FieldSystem
{
    public class FieldController
    {
        public FieldViewer FieldViewer { get => fieldViewer; set => fieldViewer = value; }
        private FieldViewer fieldViewer;

        private FieldModel fieldModel;

        public FieldController(FieldModel fieldData)
        {
            fieldModel = fieldData.Clone();
        }

        // todo: Transfer ile ilgili işlemler burada yapılacak
    }
}
