using InputSystem;
using UnityEngine;

namespace FieldSystem
{
    public class FieldViewer : MonoBehaviour, IClickable
    {
        public bool IsClickable { get; set; }

        public void OnCLick()
        {
            // Yukarı kaldır
        }

        public void OnClose()
        {
            // Bayrak çıkart
            IsClickable = false;
        }

        public void ResetClick()
        {
            // Aşağıya indir
        }
    }
}
