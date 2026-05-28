using UnityEngine;
using UnityEngine.EventSystems;

public class MobileBrake : MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler
{
    public RealisticBikeController bike;

    public void OnPointerDown(PointerEventData eventData)
    {
        bike.mobileBrake = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        bike.mobileBrake = false;
    }
}