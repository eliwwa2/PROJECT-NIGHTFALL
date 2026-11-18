
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileFlashlightButton : MonoBehaviour,
    IPointerDownHandler
{
    public Light flashlight;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (flashlight == null)
            return;

        flashlight.enabled = !flashlight.enabled;

        Debug.Log(
            flashlight.enabled
                ? "Taschenlampe eingeschaltet."
                : "Taschenlampe ausgeschaltet."
        );
    }
}
