using FieldSystem;
using UnityEngine;

namespace InputSystem
{
    public class ClickableManager : MonoBehaviour
    {
        [SerializeField] private FieldsManager fieldManager;

        private IClickable firstCicked = null;

        public void SetLastClicked(IClickable clickable)
        {
            if (clickable == null)
                return;

            // if there is no clickable that is not clicked before
            if (firstCicked == null)
            {
                firstCicked = clickable;
                firstCicked.OnCLick();
                return;
            }

            // if same clickable clicked twice
            if (firstCicked == clickable)
            {
                firstCicked.ResetClick();
                firstCicked = null;
                return;
            }

            fieldManager?.TryTransfer(firstCicked, clickable);

            firstCicked.ResetClick();
            firstCicked = null;
        }
    }
}
