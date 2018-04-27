using UnityEngine;

[ExecuteInEditMode] //able to preview in the  editor
public class HeatShader : MonoBehaviour
{
    public Material EffectMaterial; //material to render the screen with

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, EffectMaterial);    //render the src to the destination with the material 
    }
}