using FieldSystem;
using UnityEngine;

namespace InputSystem
{
    public class ClickableManager : MonoBehaviour
    {
        [SerializeField] private FieldsManager fieldManager;

        private IClickable firstClicked = null;

        /// <summary>
        /// Sets the last clicked clickable and handles the click logic.
        /// </summary>
        /// <param name="clickable">The clickable object that was clicked.</param>
        public void SetLastClicked(IClickable clickable)
        {
            if (clickable == null)
                return;

            // if there is no clickable that is not clicked before
            if (firstClicked == null && clickable.IsFirstClickable)
            {
                firstClicked = clickable;
                firstClicked.OnCLick();
                return;
            }

            // if same clickable clicked twice
            if (firstClicked == clickable)
            {
                firstClicked.ResetClick();
                firstClicked = null;
                return;
            }

            fieldManager?.TryTransferSetup(firstClicked, clickable);

            firstClicked?.ResetClick();
            firstClicked = null;
        }
    }
}
