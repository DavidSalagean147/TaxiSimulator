using Cinemachine;
using UnityEngine;

public class CinemachineCoreGetInputTouchAxis : MonoBehaviour
{
    private float TouchSensitivity_x = -100f;
    private float TouchSensitivity_y = 100f;

    public static bool isTouching = false;

    void Start()
    {
        CinemachineCore.GetInputAxis = HandleAxisInputDelegate;
    }

    float HandleAxisInputDelegate(string axisName)
    {
        if (isTouching)
        {
            switch (axisName)
            {

                case "Mouse X":

                    if (Input.touchCount > 0)
                    {
                        return Input.touches[0].deltaPosition.x / TouchSensitivity_x;
                    }
                    else
                    {
                        return Input.GetAxis(axisName);
                    }

                case "Mouse Y":
                    if (Input.touchCount > 0)
                    {
                        return Input.touches[0].deltaPosition.y / TouchSensitivity_y;
                    }
                    else
                    {
                        return Input.GetAxis(axisName);
                    }
            }
        }

        return 0f;
    }
}