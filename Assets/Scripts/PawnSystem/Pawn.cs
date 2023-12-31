using UnityEngine;
using ColorSystem;
using DG.Tweening;
using ObjectPooling;

namespace PawnSystem
{
    public class Pawn : MonoBehaviour, IPawn, IPoolableObject<Pawn>
    {
        public PawnViewer pawnViewer;
        public ColorType ColorType { get; set; }
        public IObjectPool<Pawn> PoolParent { get; set; }

        private const float MoveDelayOffset = 0.1f;
        private const float MoveStaticDelay = 0.25f;
        private const float MoveDuration = 1f;
        private Tween moveTwenn;

        private void OnDisable()
        {
            ColorType = ColorType.Empty;
            moveTwenn?.Kill();
        }

        public IPawn SetColor(ColorType colorType)
        {
            ColorType = colorType;
            pawnViewer.SetColor(colorType);
            return this;
        }

        public IPawn SetLocalPosition(Vector3 localPosition, int movingPawnIndex = 0, bool isFirstSet = false)
        {
            if(isFirstSet)
            {
                transform.localPosition = localPosition;
                return this;
            }

            moveTwenn?.Kill();
            moveTwenn = transform.DOLocalMove(localPosition, MoveDuration)
                .SetDelay((MoveDelayOffset * movingPawnIndex) + MoveStaticDelay);

            return this;
        }

        public IPawn SetTransformParent(Transform parent)
        {
            transform.parent = parent;
            return this;
        }
    }
}
