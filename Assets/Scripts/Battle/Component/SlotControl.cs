using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlotControl : MonoBehaviour
{
    public Transform frontHero;
    public Transform middleHero;
    public Transform backHero;

    public Transform frontEnemy;
    public Transform middleEnemy;
    public Transform backEnemy;

    public void SetSlotPos(CharView view,TeamPos pos,bool isEnemy,bool gameStart=false)
    {
        if (view == null) return;

        Transform slotPos = GetPos(pos,isEnemy);
        float piddle = isEnemy ? 12 : -12;
        if (gameStart)
            view.transform.position = new Vector3(slotPos.position.x + piddle, slotPos.position.y, slotPos.position.z);
        view.transform.DOMove(slotPos.position, 0.8f);

    }
    Transform GetPos(TeamPos pos,bool isEnemy)
    {
        if (pos == TeamPos.Front)
        {
            if (!isEnemy)
            {
                return frontHero;
            }
            else
            {
                return frontEnemy;
            }
        }
        if (pos == TeamPos.Middle)
        {
            if (!isEnemy)
            {
                return middleHero;
            }
            else
            {
                return middleEnemy;
            }
        }
        if (pos == TeamPos.Back)
        {
            if (!isEnemy)
            {
                return backHero;
            }
            else
            {
                return backEnemy;
            }
        }
        return null;
    }
}
