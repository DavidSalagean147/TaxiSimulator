using UnityEngine;

public class Semaphore : MonoBehaviour
{
    private Renderer semaphoreRenderer;

    public Material semaphoreMaterial;

    public Texture2D redSemaphoreTexture;
    public Texture2D yellowSemaphoreTexture;
    public Texture2D greenSemaphoreTexture;

    private new Collider collider;
    private void Awake()
    {
        semaphoreRenderer = GetComponent<Renderer>();
        collider = GetComponent<Collider>();
    }
    private void Start()
    {
        semaphoreRenderer.material = semaphoreMaterial;
        collider.enabled = false;
    }
    public void Red()
    {
        semaphoreMaterial.mainTexture = redSemaphoreTexture;
        collider.enabled = true;
    }
    public void Yellow()
    {
        semaphoreMaterial.mainTexture = yellowSemaphoreTexture;
        collider.enabled = true;
    }
    public void Green()
    {
        semaphoreMaterial.mainTexture = greenSemaphoreTexture;
        collider.enabled = false;
    }
}
