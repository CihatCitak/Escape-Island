using UnityEngine;
using ObjectPooling;

namespace PawnSystem
{
    public class Pawn : MonoBehaviour, IPawn, IPoolableObject<Pawn>
    {
        public ColorType ColorType { get; set; }
        public IObjectPool<Pawn> PoolParent { get; set; }

        public IPawn SetColor(ColorType colorType)
        {
            ColorType = colorType;
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
