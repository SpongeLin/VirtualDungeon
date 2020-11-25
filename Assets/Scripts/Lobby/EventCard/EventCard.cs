using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EventCard : MonoBehaviour
{
    public int eventCardNum;
    public Button button;
    public Image image;
    public GameObject closeTip;
    EventCardControl eventCardControl = null;

    public int eventType;
    public string eventContent;

    bool canClcik;

    private void Awake()
    {
        button.gameObject.SetActive(false);
        closeTip.gameObject.SetActive(false);
    }

    public void SetEventCard(Sprite sprtie,int type,string content)
    {
        image.sprite = sprtie;
        eventType = type;
        eventContent = content;
    }
    public void ShowEvent(bool specAnim = false)
    {
        button.gameObject.SetActive(true);
        if (specAnim)
        {
            transform.DOPunchScale(new Vector3(0.3f, 0.3f, 1), 0.5f,5,0.1f);
        }
    }
    public void SetClick(bool active)
    {
        button.interactable = active;
        canClcik = active;
    }
    public void ShowTip()
    {
        closeTip.SetActive(true);
    }
    public void Click()
    {
        if (!canClcik) return;

        eventCardControl.ClickEvent(this);
    }
    public void SetControl(EventCardControl control)
    {
        eventCardControl = control;
    }
}
