using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public static FieldManager instance { get; private set; }

    public bool playerTurn;




    private void Awake()
    {
        instance = this;


    }



}
