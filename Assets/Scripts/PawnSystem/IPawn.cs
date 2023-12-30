using UnityEngine;

namespace PawnSystem
{
    public interface IPawn
    {
        public ColorType ColorType { get; set; }
        public void SetColor(ColorType colorType);
        public void MoveLocalPosition(Vector3 position);
    }
}
