using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FogEventCard : MonoBehaviour
{
    public float offest;
    public void SetFogPos(Vector3 pos,bool anim =false)
    {
        Vector3 final = new Vector3(pos.x + offest, transform.position.y, transform.position.z);
        if (anim)
        {

            transform.DOMove(final, 0.8f);
        }
        else
        {
            transform.position = final;
        }
    }
}
