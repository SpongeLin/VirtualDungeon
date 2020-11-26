using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEventContorl : MonoBehaviour
{
    public GameObject eventWindow;
    public Text eventName;
    public Text eventDescription;
    public Image eventImage;

    public Button button1;
    public Text buttonText1;
    public Button button2;
    public Text buttonText2;

    GameEventData eventData = null;

    public void StartEvent(string gameEventName)
    {
        GameEventData data = EventCreator.CreateGameEvent(gameEventName);
        if (data == null)
        {
            Debug.LogError("Game Event is wrong.Found not this gameEvent ：" + gameEventName);
            return;
        }
        StartEventData(data);
    }
    void StartEventData(GameEventData data)
    {
        LobbyManager.instance.SetBlackForEventCard(true);

        eventData = data;

        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);

        eventWindow.SetActive(true);
        eventName.text = data.eventName;
        eventDescription.text = data.eventDescription;
        eventImage.sprite = Resources.Load<Sprite>("EventImage/" + data.eventImage);

        if (data.selection1 != null)
        {
            button1.gameObject.SetActive(true);
            buttonText1.text = data.selection1.selectionContent;
        }
        if (data.selection2 != null)
        {
            button2.gameObject.SetActive(true);
            buttonText2.text = data.selection2.selectionContent;
        }
    }
    public void Click(int buttonNum)
    {
        if (buttonNum == 1)
        {
            eventData.selection1.SelectionExcite();
        }
        if (buttonNum == 2)
        {
            eventData.selection2.SelectionExcite();
        }
    }
    public void Next()
    {
        if (eventData.nextEvent == null)
        {
            CloseEventWindow();
            return;
        }
        StartEventData(eventData.nextEvent);
    }

    public void CloseEventWindow()
    {
        LobbyManager.instance.EventCardCallBack();
        LobbyManager.instance.SetBlackForEventCard(false);
        eventWindow.SetActive(false);
    }


}
