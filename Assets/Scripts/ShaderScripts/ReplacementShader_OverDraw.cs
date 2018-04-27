using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] //able to preview in the  editor
public class ReplacementShader_OverDraw : MonoBehaviour
{

    public Shader replacementShader;
    public Color OverDrawColor;

    void OnValidate()
    {
        Shader.SetGlobalColor("_OverDrawColor", OverDrawColor); //when you need a variable in the shader but don't need/want it to be a material property
    }

    void OnEnable()
    {
        if (replacementShader != null)
        {
            GetComponent<Camera>().SetReplacementShader(replacementShader, "");

        }
    }


    //the shader stays as is until it is reset
    void Disable()
    {
        GetComponent<Camera>().ResetReplacementShader();
    }
}
