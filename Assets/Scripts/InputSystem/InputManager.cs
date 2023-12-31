using UnityEngine;

namespace InputSystem
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Camera gamePlayCamera;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private ClickableManager clickableManager;

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                var mousePosition = Input.mousePosition;
                var ray = gamePlayCamera.ScreenPointToRay(mousePosition);

                if(Physics.Raycast(ray, out RaycastHit raycastHit, layerMask))
                {
                    var rbBody = raycastHit.collider.attachedRigidbody;
                    
                    if(rbBody.transform.TryGetComponent(out IClickable clickable))
                    {
                        clickableManager.SetLastClicked(clickable);
                    }
                }
            }
        }
    }
}
