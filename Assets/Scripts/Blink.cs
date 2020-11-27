using UnityEngine;

public class Blink : MonoBehaviour
{
    public Renderer blinkRightLights;
    public Renderer blinkLeftLights;
    public Material blinkMaterial;
    private Material normalMaterial;
    private bool isBlinkingLeft = false;
    private bool isBlinkingRight = false;
    private bool isEmergencyLightning = false;
    public Animation blinkLeftAnim;
    public Animation blinkRightAnim;
    public GameObject blinkLeftNon;     //buttons
    public GameObject blinkLeft;
    public GameObject blinkRightNon;
    public GameObject blinkRight;

    private void Start()
    {
        normalMaterial = blinkRightLights.material;
        blinkLeft.SetActive(false);
        blinkRight.SetActive(false);
        blinkLeftNon.SetActive(true);
        blinkRightNon.SetActive(true);
        blinkLeftAnim.enabled = false;
        blinkRightAnim.enabled = false;
        //blinkRightLights.material = normalMaterial;
        //blinkLeftLights.material = normalMaterial;
    }
    private void Update()
    {
        if(isBlinkingRight || isEmergencyLightning)
        {
            float floor = 0f;
            float ceiling = 1f;
            float emission = floor + Mathf.PingPong(Time.time * 2, ceiling - floor);
            blinkRightLights.material.SetColor("_EmissionColor", new Color(1f, 1f, 1f) * emission);
        }
        if(isBlinkingLeft || isEmergencyLightning)
        {
            float floor = 0f;
            float ceiling = 1f;
            float emission = floor + Mathf.PingPong(Time.time * 2, ceiling - floor);
            blinkLeftLights.material.SetColor("_EmissionColor", new Color(1f, 1f, 1f) * emission);
        }
    }
    public void BlinkRightButton()
    {
        blinkLeftAnim.enabled = false;
        blinkLeft.SetActive(false);
        blinkLeftNon.SetActive(true);
        blinkLeftLights.material = normalMaterial;
        if (isBlinkingRight)
        {
            blinkRight.SetActive(false);
            blinkRightNon.SetActive(true);
            blinkRightAnim.enabled = false;
            blinkRightLights.material = normalMaterial;
            isBlinkingRight = false;
        }
        else
        {
            blinkRightNon.SetActive(false);
            blinkRight.SetActive(true);
            blinkRightAnim.enabled = true;
            blinkRightLights.material = blinkMaterial;
            isBlinkingRight = true;
        }
        isBlinkingLeft = false;
        isEmergencyLightning = false;
    }
    public void BlinkLeftButton()
    {
        blinkRightAnim.enabled = false;
        blinkRight.SetActive(false);
        blinkRightNon.SetActive(true);
        blinkRightLights.material = normalMaterial;
        if (isBlinkingLeft)
        {
            blinkLeft.SetActive(false);
            blinkLeftNon.SetActive(true);
            blinkLeftAnim.enabled = false;
            blinkLeftLights.material = normalMaterial;
            isBlinkingLeft = false;
        }
        else
        {
            blinkLeftNon.SetActive(false);
            blinkLeft.SetActive(true);
            blinkLeftAnim.enabled = true;
            blinkLeftLights.material = blinkMaterial;
            isBlinkingLeft = true;
        }
        isBlinkingRight = false;
        isEmergencyLightning = false;
    }
    public void EmergencyLightsButton()
    {
        blinkRightAnim.enabled = false;
        blinkLeftAnim.enabled = false;
        if (isEmergencyLightning)
        {
            blinkLeft.SetActive(false);
            blinkRight.SetActive(false);
            blinkLeftNon.SetActive(true);
            blinkRightNon.SetActive(true);
            blinkLeftAnim.enabled = false;
            blinkRightAnim.enabled = false;
            blinkRightLights.material = normalMaterial;
            blinkLeftLights.material = normalMaterial;
            isEmergencyLightning = false;
        }
        else
        {
            blinkLeft.SetActive(false);
            blinkRight.SetActive(false);
            blinkLeftNon.SetActive(true);
            blinkRightNon.SetActive(true);
            blinkLeftAnim.enabled = true;
            blinkRightAnim.enabled = true;
            blinkRightLights.material = normalMaterial;
            blinkLeftLights.material = normalMaterial;
            blinkRightLights.material = blinkMaterial;
            blinkLeftLights.material = blinkMaterial;
            isEmergencyLightning = true;
        }
        isBlinkingLeft = false;
        isBlinkingRight = false;
    }
}