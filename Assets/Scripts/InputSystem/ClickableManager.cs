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
            // if there is no field that is not clicked before
            if (firstCicked == null)
            {
                firstCicked = clickable;
                firstCicked.OnCLick();
                return;
            }

            // if same field clicked twice
            if (firstCicked == clickable)
            {
                firstCicked.ResetClick();
                return;
            }

            secondClicked = clickable;
            secondClicked.OnCLick();

            fieldManager.TryTransfer(firstCicked, secondClicked);

            firstCicked.ResetClick();
            secondClicked.ResetClick();
        }
    }
}
