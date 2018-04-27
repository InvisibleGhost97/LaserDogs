using UnityEngine;

[ExecuteInEditMode] //able to preview in the  editor
public class BlurShader : MonoBehaviour
{
    public Material BlurMaterial; //material to render the screen with
    //controls the blur iterations performed
    [Range(0, 10)]
    public int Iterations;

    [Range(0, 4)]
    public int DownRes;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {

        int width = src.width >> DownRes;
        int height = src.height >> DownRes;

        //generate a temporary render texture that is the same width and height as the source
        RenderTexture rt = RenderTexture.GetTemporary(src.width, src.height);
        //preform a Blit from the src to the rt
        Graphics.Blit(src, rt);    //render the src to the destination with the material 

        //wiith the screen content stored in rt we do this loop
        for (int i = 0; i < Iterations; i++)
        {
            //creating another temporary render texture
            RenderTexture rt2 = RenderTexture.GetTemporary(width, height);
            //blurred image should now be in rt2 so below rt is released
            Graphics.Blit(rt, rt2, BlurMaterial);
            RenderTexture.ReleaseTemporary(rt);
            rt = rt2;   //the blurred image is referenced by rt

        }

        Graphics.Blit(rt, dst);
        RenderTexture.ReleaseTemporary(rt); //release the temporary texture


    }
}