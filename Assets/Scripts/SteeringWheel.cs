using UnityEngine;

public class SteeringWheel : MonoBehaviour
{
    void Update()
    {
        if (!CameraController.isOn3rdPersonCamera)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, transform.eulerAngles.y, UISteeringWheel.outPut * 360f), Time.deltaTime * 500);
        }
    }
}
