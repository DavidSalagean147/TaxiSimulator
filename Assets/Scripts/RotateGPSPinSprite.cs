using UnityEngine;

public class RotateGPSPinSprite : MonoBehaviour
{
    private GameObject GPSArrow;
    void Awake()
    {
        GPSArrow = GameObject.Find("GPSArrowSprite");
    }
    void Update()
    {
        transform.rotation = GPSArrow.transform.rotation;
    }
}
