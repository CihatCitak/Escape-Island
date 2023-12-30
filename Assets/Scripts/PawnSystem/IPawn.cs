using UnityEngine;
using ColorSystem;

namespace PawnSystem
{
    public interface IPawn
    {
        public ColorType ColorType { get; set; }
        public IPawn SetColor(ColorType colorType);
        public IPawn SetLocalPosition(Vector3 position);
        public IPawn SetTransformParent(Transform parent);
    }
}
