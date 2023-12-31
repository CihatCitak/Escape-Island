using FieldSystem;
using UnityEngine;

namespace InputSystem
{
    public class ClickableManager : MonoBehaviour
    {
        [SerializeField] private FieldsManager fieldManager;

        private IClickable firstClicked = null;

        public void SetLastClicked(IClickable clickable)
        {
            if (clickable == null)
                return;

            // if there is no clickable that is not clicked before
            if (firstClicked == null)
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

            fieldManager?.TryTransfer(firstClicked, clickable);

            firstClicked.ResetClick();
            firstClicked = null;
        }
    }
}
