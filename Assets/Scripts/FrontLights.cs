using UnityEngine;
using UnityEngine.UI;

public class FrontLights : MonoBehaviour
{
    public Light frontLightL;
    public Light frontLightR;
    private Renderer frontLights;
    private Material normalMaterial;
    public Material lightMaterial;
    public Image nonLighted;
    public Image shortLighted;
    public Image longLighted;
    private bool isShortLight = false;
    private bool isLongLight = false;

    private void Awake()
    {
        frontLights = GetComponent<Renderer>();
    }
    private void Start()
    {
        normalMaterial = frontLights.material;
        shortLighted.enabled = false;
        longLighted.enabled = false;
        nonLighted.enabled = true;
        frontLightL.range = 0f;
        frontLightL.intensity = 0f;
        frontLightR.range = 0f;
        frontLightR.intensity = 0f;
    }
    public void Light()
    {
        if (isShortLight)
        {
            isLongLight = true;
            isShortLight = false;
            nonLighted.enabled = false;
            shortLighted.enabled = false;
            longLighted.enabled = true;
            frontLights.material = lightMaterial;
            frontLightL.range = 40f;
            frontLightL.intensity = 5f;
            frontLightR.range = 40f;
            frontLightR.intensity = 5f;
        }
        else
        if (isLongLight)
        {
            isLongLight = false;
            isShortLight = false;
            shortLighted.enabled = false;
            longLighted.enabled = false;
            nonLighted.enabled = true;
            frontLights.material = normalMaterial;
            frontLightL.range = 0f;
            frontLightL.intensity = 0f;
            frontLightR.range = 0f;
            frontLightR.intensity = 0f;
        }
        else
        {
            isShortLight = true;
            isLongLight = false;
            nonLighted.enabled = false;
            longLighted.enabled = false;
            shortLighted.enabled = true;
            frontLights.material = lightMaterial;
            frontLightL.range = 20f;
            frontLightL.intensity = 3f;
            frontLightR.range = 20f;
            frontLightR.intensity = 3f;
        }
    }
}
