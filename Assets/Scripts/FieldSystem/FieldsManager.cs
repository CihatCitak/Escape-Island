using ColorSystem;
using InputSystem;
using UnityEngine;
using LevelDataSystem;
using System.Collections.Generic;
using UISystem;

namespace FieldSystem
{
    public class FieldsManager : MonoBehaviour
    {
        public List<FieldHolder> FieldsHolder { get => fieldHolders; }
        private List<FieldHolder> fieldHolders = new List<FieldHolder>();

        private void OnEnable() => LevelManager.Instance.AllPoolObjectReturnsPool += () => fieldHolders.Clear();
        private void OnDisable() => LevelManager.Instance.AllPoolObjectReturnsPool -= () => fieldHolders.Clear();

        /// <summary>
        /// Attempts to transfer pawns between the field controllers associated with the provided clickable objects.
        /// </summary>
        /// <param name="clickableFirst">The first clickable object involved in the transfer.</param>
        /// <param name="clickableSecond">The second clickable object involved in the transfer.</param>
        public void TryTransferSetup(IClickable clickableFirst, IClickable clickableSecond)
        {
            // if some clickable is null then return;
            if (clickableFirst == null || clickableSecond == null)
                return;

            // Convert IClickables to FieldViewer
            FieldViewer firstClickViewer = clickableFirst as FieldViewer;
            FieldViewer secondClickViewer = clickableSecond as FieldViewer;

            // Find FieldViewer to FieldController
            FieldController firstClickFieldController = fieldHolders.Find(x => x.FieldViewer == firstClickViewer).FieldController;
            FieldController secondClickFieldController = fieldHolders.Find(x => x.FieldViewer == secondClickViewer).FieldController;

            bool isTransferSucces = TryTransfer(firstClickFieldController, secondClickFieldController);

            if (isTransferSucces && CheckAlldFieldDone())
            {
                MainUI.Instance.LevelWin();
            }
        }

        /// <summary>
        /// Tries to transfer pawns from one field controller to another based on certain conditions.
        /// </summary>
        /// <param name="from">The source field controller.</param>
        /// <param name="to">The target field controller.</param>
        private bool TryTransfer(FieldController from, FieldController to)
        {
            // if some field is null then return;
            if (from == null || to == null)
                return false;

            // If ToField is empty don't waste time do transfer
            ColorType toLastPawnColor = to.GetNextColumnColorType();
            bool isToAllPositionsEmpty = toLastPawnColor == ColorType.Empty;
            if(isToAllPositionsEmpty)
            {
                to.AddPawns(from.RemovePawns());
                return true;
            }

            // If field has different color types transfer is impossible
            ColorType fromPawnColor = from.GetNextColumnColorType();
            if (fromPawnColor != toLastPawnColor)
                return false;

            // If ToField empty position count is enough for FromFieldPawnCount
            int fromPawnCount = from.GetNextMovePawnCount();
            int toEmptyPositionCount = to.GetEmptyPawnPositionCount();
            if (fromPawnCount <= toEmptyPositionCount)
            {
                to.AddPawns(from.RemovePawns());
                return true;
            }

            return false;
        }

        private bool CheckAlldFieldDone()
        {
            foreach (var fieldHolder in fieldHolders)
            {
                if(!fieldHolder.FieldController.IsFieldDone())
                    return false;
            }

            return true;
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

