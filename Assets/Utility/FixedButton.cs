using UnityEngine;
using UnityEngine.EventSystems;

public class FixedButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    public bool Pressed;
    public FixedTouchField fixedTouchField;
    public bool Check = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(!fixedTouchField.Pressed){
            fixedTouchField.Pressed = true;
            fixedTouchField.PointerId = eventData.pointerId;
            fixedTouchField.PointerOld = eventData.position;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
        if(fixedTouchField.Pressed){
            fixedTouchField.Pressed = false;
        }
    }
}