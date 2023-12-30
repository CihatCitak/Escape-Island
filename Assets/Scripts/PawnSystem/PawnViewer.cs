using ColorSystem;
using UnityEngine;

namespace PawnSystem
{
    public class PawnViewer : MonoBehaviour
    {
        [SerializeField] private MaterialHolder materialHolder;
        [SerializeField] private MeshRenderer meshRenderer;

        public void SetColor(ColorType colorType)
        {
            var material = materialHolder.GetMaterial(colorType);

            if (material)
                meshRenderer.material = material;
        }
    }
}
