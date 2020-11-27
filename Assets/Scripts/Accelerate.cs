using UnityEngine;
using UnityEngine.EventSystems;

public class Accelerate : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Car car;
    public static bool interactable = true;

    void Awake()
    {
        car = FindObjectOfType(typeof(Car)) as Car;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (interactable == true)
        {
            car.Accelerate();
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (interactable == true)
        {
            car.DontAccelerate();
        }
    }
}
