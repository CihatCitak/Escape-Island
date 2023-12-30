using System;
using UnityEngine;
using System.Collections.Generic;

namespace ColorSystem
{
    [CreateAssetMenu(menuName = "Color/Material Holder", fileName = "Material Holder")]
    public class MaterialHolder : ScriptableObject
    {
        public List<ColorMaterial> colorMaterials;

        public Material GetMaterial(ColorType colorType)
        {
            return colorMaterials.Find(x => x.ColorType == colorType)?.Material;
        }
    }

    [Serializable]
    public class ColorMaterial
    {
        public ColorType ColorType;
        public Material Material;
    }
}