using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISteeringWheel : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private bool steeringWheelBeingHeld = false;
    private RectTransform steeringWheel;
    private float steeringWheelAngle = 0f;
    private float lastSteeringWheelAngle = 0f;
    private Vector2 center;
    private float maxSteerAngle = 360f;
    public static float outPut;
    private Car car;

    private void Awake()
    {
        steeringWheel = GetComponent<RectTransform>();
        car = FindObjectOfType(typeof(Car)) as Car;
    }
    void Update()
    {
        if (steeringWheelAngle != 0f)
        {
            if (steeringWheelBeingHeld == false)
            {
                float deltaAngle = 500f * Time.deltaTime;
                if (Mathf.Abs(deltaAngle) > Mathf.Abs(steeringWheelAngle))
                {
                    steeringWheelAngle = 0f;
                }
                else if (steeringWheelAngle > 0f)
                {
                    steeringWheelAngle -= deltaAngle;
                }
                else
                {
                    steeringWheelAngle += deltaAngle;
                }
                steeringWheel.localEulerAngles = new Vector3(0f, 0f, -steeringWheelAngle);
                outPut = steeringWheelAngle / maxSteerAngle;
            }
            else
            {
                steeringWheel.localEulerAngles = new Vector3(0f, 0f, -steeringWheelAngle);
                outPut = steeringWheelAngle / maxSteerAngle;
            }
            car.Steer();
        }
    }
    public void OnPointerDown(PointerEventData data)
    {
        steeringWheelBeingHeld = true;
        center = RectTransformUtility.WorldToScreenPoint(data.pressEventCamera, steeringWheel.position);
        lastSteeringWheelAngle = Vector2.Angle(Vector2.up, data.position - center);
    }
    public void OnDrag(PointerEventData data)
    {
        float newAngle = Vector2.Angle(Vector2.up, data.position - center);
        if((data.position - center).sqrMagnitude >= 400f)
        {
            if(data.position.x > center.x)
            {
                steeringWheelAngle += newAngle - lastSteeringWheelAngle;
            }
            else
            {
                steeringWheelAngle -= newAngle - lastSteeringWheelAngle;
            }
        }
        steeringWheelAngle = Mathf.Clamp(steeringWheelAngle, - maxSteerAngle, maxSteerAngle);
        lastSteeringWheelAngle = newAngle;
    }
    public void OnPointerUp(PointerEventData data)
    {
        OnDrag(data);
        steeringWheelBeingHeld = false;
    }
}
