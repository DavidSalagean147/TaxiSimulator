using UnityEngine;
using UnityEngine.EventSystems;

public class Brake : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Car car;

    void Awake()
    {
        car = FindObjectOfType(typeof(Car)) as Car;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Accelerate.interactable == true)
        {
            car.Brake();
            car.BrakeTexture();
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (Accelerate.interactable == true)
        {
            car.DontBrake();
            car.NormalTexture();
        }
    }
}

