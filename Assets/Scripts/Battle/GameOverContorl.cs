using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverContorl : MonoBehaviour
{
    public GameObject gameOver;
    public Text message;


    public void Open(string content)
    {
        gameOver.SetActive(true);
        message.text = content;
    }
    public void Over()
    {
        SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
    }
}
