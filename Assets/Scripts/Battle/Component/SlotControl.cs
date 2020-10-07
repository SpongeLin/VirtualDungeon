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

    public void SetSlotPos(CharView view,TeamPos pos,bool isEnemy)
    {
        if(pos == TeamPos.Front)
        {
            if (!isEnemy)
            {
                view.transform.DOMove(frontHero.position, 0.6f);
            }
            else
            {
                view.transform.DOMove(frontEnemy.position, 0.6f);
            }
        }
        if(pos == TeamPos.Middle)
        {
            if (!isEnemy)
            {
                view.transform.DOMove(middleHero.position, 0.6f);
            }
            else
            {
                view.transform.DOMove(middleEnemy.position, 0.6f);
            }
        }
        if (pos == TeamPos.Back)
        {
            if (!isEnemy)
            {
                view.transform.DOMove(backHero.position, 0.6f);
            }
            else
            {
                view.transform.DOMove(backEnemy.position, 0.6f);
            }
        }

    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
