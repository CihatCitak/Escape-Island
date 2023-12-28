using UnityEngine;
using InputSystem;
using System.Collections.Generic;

namespace FieldSystem
{
    public class FieldsManager : MonoBehaviour
    {
        private List<FieldHolder> fieldsHolder = new List<FieldHolder>();

        public void TryTransfer(IClickable clickableFirst, IClickable clickableSecond)
        {
            // Convert IClickables to FieldViewer
            FieldViewer firstClickViewer = clickableFirst as FieldViewer;
            FieldViewer secondClickViewer = clickableSecond as FieldViewer;

            // Find FieldViewer to FieldController
            FieldController firstClickFieldController = fieldsHolder.Find(x => x.FieldView == firstClickViewer).FieldController;
            FieldController secondClickFieldController = fieldsHolder.Find(x => x.FieldView == secondClickViewer).FieldController;

            TryTransfer(firstClickFieldController, secondClickFieldController);
        }

        private void TryTransfer(FieldController from, FieldController to)
        {
            // if some field is null then return;
            if (from == null || to == null)
                return;

            /*int fromPawnCount = from.GetPawnCount();
            int toEmptyPositionCount = to.GetEmptyPositionCount();

            Color fromPawnColor = from.GetColor();
            Color toLastPawnColor = to.GetColor();

            bool isToAllPositionsEmpty = to.IsEmpty();


            // from'un bir sonraki satır pawn'larının rengi ile
            // to'un satır rengi uyuşuyor veya to'un bütün satırları boşsa
            // ve from'un bir sornaki göndereceği pawn sayısı kadar to da yer varsa
            bool canTransfer = fromPawnColor == toLastPawnColor && fromPawnCount <= toEmptyPositionCount;
            if (isToAllPositionsEmpty || canTransfer)
            {
                to.AddPawns(from.GetPawns());
                return;
            }*/
        }

        public sealed class FieldHolder
        {
            public FieldController FieldController;
            public FieldViewer FieldView;
        }
    }
}

