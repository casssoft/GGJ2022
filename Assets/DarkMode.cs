using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMode : MonoBehaviour
{
    public Shader awesomeShader = null;
    private Material m_renderMaterial;

    void Start()
    {
        if (awesomeShader == null)
        {
            Debug.LogError("no awesome shader.");
            m_renderMaterial = null;
            return;
        }
        m_renderMaterial = new Material(awesomeShader);
    }
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (GlobalVariables.followPlayer)
        {
            Graphics.Blit(source, destination, m_renderMaterial);
        } else
        {
            // Dont use the dark mode shader
            Graphics.Blit(source, destination);
        }
    }
}
