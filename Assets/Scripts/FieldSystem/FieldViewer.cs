using InputSystem;
using UnityEngine;
using DG.Tweening;

namespace FieldSystem
{
    public class FieldViewer : MonoBehaviour, IClickable
    {
        public bool IsClickable { get; set; }

        public void OnCLick()
        {
            // Yukarı kaldır
            transform.DOMoveY(1f, 1f).SetEase(Ease.InOutSine).Play();
        }

        public void OnClose()
        {
            // Bayrak çıkart
            IsClickable = false;
        }

        public void ResetClick()
        {
            // Aşağıya indir
            transform.DOMoveY(0f, 0.5f).SetEase(Ease.InOutSine).Play();
        }
    }
}
