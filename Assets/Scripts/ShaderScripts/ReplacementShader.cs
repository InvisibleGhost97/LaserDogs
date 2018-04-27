using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] //able to preview in the  editor
public class ReplacementShader : MonoBehaviour {

    public Shader replacementShader;

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
