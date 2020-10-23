using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatusCreator
{
    public static CharStatus CreateCharStatus(string charStatusName, int statusNum)
    {
        switch (charStatusName)
        {
            case "DamageToZero":
                return new nCharStatus.DamageToZero(statusNum);
        }

        Debug.LogWarning("This charStatus is not exist.");
        return null;
    }
    //===============================================================
    public static CardStatus CreateCardStatus(string cardStatusName, int statusNum)
    {
        switch (cardStatusName)
        {
            case "OriExhasut":
                return new nCardStatus.OriExhasut();
        }

        Debug.LogWarning("This cardStatus is not exist.");
        return null;
    }
    //===============================================================
    public static FieldStatus CreateFieldStatus(string fieldStatusName, int statusNum)
    {
        switch (fieldStatusName)
        {

        }

        Debug.LogWarning("This fieldStatus is not exist.");
        return null;
    }
}