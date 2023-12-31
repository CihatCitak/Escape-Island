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

        /// <summary>
        /// Attempts to transfer pawns between the field controllers associated with the provided clickable objects.
        /// </summary>
        /// <param name="clickableFirst">The first clickable object involved in the transfer.</param>
        /// <param name="clickableSecond">The second clickable object involved in the transfer.</param>
        public void TryTransfer(IClickable clickableFirst, IClickable clickableSecond)
        {
            // if some clickable is null then return;
            if (clickableFirst == null || clickableSecond == null)
                return;

            // Convert IClickables to FieldViewer
            FieldViewer firstClickViewer = clickableFirst as FieldViewer;
            FieldViewer secondClickViewer = clickableSecond as FieldViewer;

            // Find FieldViewer to FieldController
            FieldController firstClickFieldController = fieldsHolder.Find(x => x.FieldViewer == firstClickViewer).FieldController;
            FieldController secondClickFieldController = fieldsHolder.Find(x => x.FieldViewer == secondClickViewer).FieldController;

            TryTransfer(firstClickFieldController, secondClickFieldController);
        }

        /// <summary>
        /// Tries to transfer pawns from one field controller to another based on certain conditions.
        /// </summary>
        /// <param name="from">The source field controller.</param>
        /// <param name="to">The target field controller.</param>
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

