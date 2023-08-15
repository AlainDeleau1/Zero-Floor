using UnityEngine;
using UnityEngine.EventSystems;

public class ButtomPlaySound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler // 2 Interfaces del sistema de eventos de Unity
{
    public string onEntrySoundName = "OnEntry";
    public string onClickedSoundName = "OnClicked";

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundMan.Instance.PlaySound(onEntrySoundName);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundMan.Instance.PlaySound(onClickedSoundName);
    }
}
