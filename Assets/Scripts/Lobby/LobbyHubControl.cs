using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyHubControl : MonoBehaviour
{
    public Text money;
    public LobbyCharInfoView front;
    public LobbyCharInfoView middle;
    public LobbyCharInfoView back;

    public void LobbyStart()
    {
        front.SetCharInfo(GameData.instance.front);
        middle.SetCharInfo(GameData.instance.middle);
        back.SetCharInfo(GameData.instance.back);

        money.text = GameData.instance.money.ToString();
    }
    public void LobbyUpdate()
    {
        front.UpdateCharInfo();
        middle.UpdateCharInfo();
        back.UpdateCharInfo();

        money.text = GameData.instance.money.ToString();
    }



}
