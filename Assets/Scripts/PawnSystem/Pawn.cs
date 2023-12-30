using UnityEngine;

namespace PawnSystem
{
    public class Pawn : MonoBehaviour, IPawn
    {
        public ColorType ColorType { get; set; }

        public void MoveLocalPosition(Vector3 localPosition)
        {
            transform.localPosition = localPosition;
        }

        public void SetColor(ColorType colorType)
        {
            ColorType = colorType;
        }
    }
}
