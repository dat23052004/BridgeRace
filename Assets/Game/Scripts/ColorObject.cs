using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject : MonoBehaviour
{
    public ColorType colorType;
    [SerializeField] private MeshRenderer Renderer;
    public Material[] colorMats;
    
    public void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType;
        Renderer.material = GetColorMats(colorType);
    }

    public Material GetColorMats(ColorType colorType)
    {
        return colorMats[(int)colorType];       
    }


}
    