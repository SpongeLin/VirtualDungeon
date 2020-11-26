using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyHubControl : MonoBehaviour
{
    public Text money;
    public Text fogCounter;
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

    }
    public void MoneyUpdate()
    {
        money.text = GameData.instance.money.ToString();
    }

    public void FogCounterRun()
    {
        int fog = GameData.instance.fogBeforeNum;
        if (fog == 0)
        {
            fogCounter.text = "白霧正在侵蝕";
        }
        else
        {
            fogCounter.text = "白霧將在" + fog + "回合後到來";
        }
    }

}
