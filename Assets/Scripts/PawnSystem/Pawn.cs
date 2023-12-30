using UnityEngine;
using ColorSystem;
using ObjectPooling;

namespace PawnSystem
{
    public class Pawn : MonoBehaviour, IPawn, IPoolableObject<Pawn>
    {
        public PawnViewer pawnViewer;
        public ColorType ColorType { get; set; }
        public IObjectPool<Pawn> PoolParent { get; set; }

        public IPawn SetColor(ColorType colorType)
        {
            ColorType = colorType;
            pawnViewer.SetColor(colorType);
            return this;
        }

        public IPawn SetLocalPosition(Vector3 localPosition)
        {
            transform.localPosition = localPosition;
            return this;
        }

        public IPawn SetTransformParent(Transform parent)
        {
            transform.parent = parent;
            return this;
        }
    }
}
