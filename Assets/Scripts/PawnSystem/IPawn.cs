using UnityEngine;
using ColorSystem;

namespace PawnSystem
{
    public interface IPawn
    {
        public ColorType ColorType { get; set; }
        public IPawn SetColor(ColorType colorType);
        public IPawn SetLocalPosition(Vector3 localPosition, int movingPawnIndex = 0, bool isFirstSet = false);
        public IPawn SetTransformParent(Transform parent);
    }
}
