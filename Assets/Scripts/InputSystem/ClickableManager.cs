using FieldSystem;
using UnityEngine;

namespace InputSystem
{
    public class ClickableManager : MonoBehaviour
    {
        [SerializeField] private FieldsManager fieldManager;

        private IClickable firstCicked = null;
        private IClickable secondClicked = null;

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

            secondClicked = clickable;
            secondClicked.OnCLick();
            
            fieldManager?.TryTransfer(firstCicked, secondClicked);

            firstCicked.ResetClick();
            secondClicked.ResetClick();
            firstCicked = null;
            secondClicked = null;
        }
    }
}
