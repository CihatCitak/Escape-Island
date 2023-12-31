using InputSystem;
using UnityEngine;
using DG.Tweening;
using ObjectPooling;

namespace FieldSystem
{
    public class FieldViewer : MonoBehaviour, IClickable, IPoolableObject<FieldViewer>
    {
        [SerializeField] private Transform pawnPositionParent;
        private Tween moveTween;

        public bool IsFirstClickable { get => pawnPositionParent.childCount > 0; }
        public bool IsClosed { get; set; }
        public IObjectPool<FieldViewer> PoolParent { get; set; }

        public void OnCLick()
        {
            if (moveTween != null)
                moveTween.Kill();

            moveTween = transform.DOMoveY(1f, 1f).SetEase(Ease.InOutSine).Play();
        }

        public void ResetClick()
        {
            if (moveTween != null)
                moveTween.Kill();

            moveTween = transform.DOMoveY(0f, 0.5f).SetEase(Ease.InOutSine).Play();
        }

        public Transform GetPawnPositionParent()
        {
            return pawnPositionParent;
        }
    }
}
