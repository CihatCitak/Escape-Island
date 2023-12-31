using ColorSystem;
using System;
using InputSystem;
using UnityEngine;
using System.Collections.Generic;

namespace FieldSystem
{
    public class FieldsManager : MonoBehaviour
    {
        public List<FieldHolder> FieldsHolder { get => fieldsHolder; }
        private List<FieldHolder> fieldsHolder = new List<FieldHolder>();

        public void TryTransfer(IClickable clickableFirst, IClickable clickableSecond)
        {
            // Convert IClickables to FieldViewer
            FieldViewer firstClickViewer = clickableFirst as FieldViewer;
            FieldViewer secondClickViewer = clickableSecond as FieldViewer;

            // Find FieldViewer to FieldController
            FieldController firstClickFieldController = fieldsHolder.Find(x => x.FieldViewer == firstClickViewer).FieldController;
            FieldController secondClickFieldController = fieldsHolder.Find(x => x.FieldViewer == secondClickViewer).FieldController;

            TryTransfer(firstClickFieldController, secondClickFieldController);
        }

        private void TryTransfer(FieldController from, FieldController to)
        {
            // if some field is null then return;
            if (from == null || to == null)
                return;

            int fromPawnCount = from.GetNextMovePawnCount();
            int toEmptyPositionCount = to.GetEmptyPawnPositionCount();

            ColorType fromPawnColor = from.GetNextColumnColorType();
            ColorType toLastPawnColor = to.GetNextColumnColorType();

            bool isToAllPositionsEmpty = toLastPawnColor == ColorType.Empty;

            bool canTransfer = fromPawnColor != ColorType.Empty && 
                fromPawnColor == toLastPawnColor && fromPawnCount <= toEmptyPositionCount;

            if (isToAllPositionsEmpty || canTransfer)
            {
                to.AddPawns(from.RemovePawns());
                return;
            }
        }

        public sealed class FieldHolder
        {
            public FieldController FieldController;
            public FieldViewer FieldViewer;

            public FieldHolder(FieldController fieldController, FieldViewer fieldViewer)
            {
                FieldController = fieldController;
                FieldViewer = fieldViewer;
            }
        }
    }
}

