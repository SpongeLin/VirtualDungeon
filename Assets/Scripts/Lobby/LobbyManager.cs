using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance { get; private set; }

    public LobbyHubControl hub;
    public DealControl deal;
    public DeckDisplay deckDisplay;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        hub.LobbyStart();

        if (GameData.instance.battleResult)
            deal.StartPickUp();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene("Battle",LoadSceneMode.Single);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            deal.StartPickUp();
        }
    }
    public void DeckDisplayOpen()
    {
        deckDisplay.Open();
    }
}
