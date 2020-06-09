using UnityEngine.EventSystems;

public class PuloJoystick : Joystick
{
    public bool pulou;
    protected override void Start()
    {
        pulou = false;
        base.Start();
        background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(false);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        pulou = Vertical > 0;
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }
}