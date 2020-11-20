using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealControl : MonoBehaviour
{
    public GameObject black;
    public PickUpDeal pickUpDeal;

    public void StartPickUp()
    {
        black.SetActive(true);
        pickUpDeal.gameObject.SetActive(true);
        pickUpDeal.StartPickUp();
    }
    public void Close()
    {
        black.SetActive(false);
        pickUpDeal.gameObject.SetActive(false);
    }

}
