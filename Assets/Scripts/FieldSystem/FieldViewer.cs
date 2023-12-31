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
        private const float MoveDuration = 0.5f;
        private const float MoveYOffset = 0.5f;

        public bool IsFirstClickable { get => pawnPositionParent.childCount > 0; }
        public IObjectPool<FieldViewer> PoolParent { get; set; }

        public void OnCLick()
        {
            if (moveTween != null)
                moveTween.Kill();

            moveTween = transform.DOMoveY(MoveYOffset, MoveDuration).SetEase(Ease.InOutSine).Play();
        }

        public void ResetClick()
        {
            if (moveTween != null)
                moveTween.Kill();

            moveTween = transform.DOMoveY(0f, MoveDuration / 2).SetEase(Ease.InOutSine).Play();
        }

        public Transform GetPawnPositionParent()
        {
            return pawnPositionParent;
        }
    }
}
