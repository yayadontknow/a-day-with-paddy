using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerpos : MonoBehaviour
{
    [Tooltip("Name of the global shader vector property.")]
    public string globalShaderPropertyName = "_PlayerPos";

    void Update()
    {
        if (!string.IsNullOrEmpty(globalShaderPropertyName))
        {
            Shader.SetGlobalVector(globalShaderPropertyName, transform.position);
        }
    }
}
