using UnityEngine;
using UnityEngine.EventSystems;

public class HelpForCinemachine : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{ 
    public void OnPointerDown(PointerEventData eventData)
    {
        CinemachineCoreGetInputTouchAxis.isTouching = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CinemachineCoreGetInputTouchAxis.isTouching = false;
    }
}
