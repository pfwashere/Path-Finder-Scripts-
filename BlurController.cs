using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurController : MonoBehaviour
{
    public Material blurMaterial;
    public float blurIntensity = 0.01f; // Adjust as needed

    void Start()
    {
        if (blurMaterial != null)
        {
            blurMaterial.SetFloat("_BlurSize", blurIntensity);
        }
    }

    public void SetBlur(bool enabled)
    {
        blurMaterial.SetFloat("_BlurSize", enabled ? blurIntensity : 0f);
    }
}

