using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static bool isOn3rdPersonCamera = true;
    private GameObject frontViewFirstPersonCamera;
    private GameObject frontViewThirdPersonCamera;
    private GameObject Mirror;
    private GameObject hands;
    private GameObject character;

    private void Awake()
    {
        frontViewFirstPersonCamera = GameObject.FindGameObjectWithTag("FrontViewFirstPerson");
        frontViewThirdPersonCamera = GameObject.FindGameObjectWithTag("FrontViewThirdPerson");
        Mirror = GameObject.FindGameObjectWithTag("Mirror");
        hands = GameObject.FindGameObjectWithTag("hands");
        character = GameObject.FindGameObjectWithTag("character");
    }
    private void Start()
    {
        frontViewFirstPersonCamera.SetActive(false);
        Mirror.SetActive(false);
        hands.SetActive(false);
    }
    public void Camera()
    {
        if (isOn3rdPersonCamera == true)
        {
            frontViewThirdPersonCamera.SetActive(false);
            frontViewFirstPersonCamera.SetActive(true);
            Mirror.SetActive(true);
            hands.SetActive(true);
            character.SetActive(false);
            isOn3rdPersonCamera = false;
        }
        else
        {
            frontViewFirstPersonCamera.SetActive(false);
            frontViewThirdPersonCamera.SetActive(true);
            Mirror.SetActive(false);
            hands.SetActive(false);
            character.SetActive(true);
            isOn3rdPersonCamera = true;
        }
    }
}