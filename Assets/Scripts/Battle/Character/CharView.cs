using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharView : MonoBehaviour
{
    public CharViewUI charViewUI;
    public CharData character;
    public bool canClick = false;

    public CharAvatar charAvatar;//??
    public Animator animator;


    public void SetClickTarget(bool active)
    {
        canClick = active;
        if (active)
        {
            animator.SetTrigger("selected");
        }
        else
        {
            animator.SetTrigger("notSelected");
        }
    }

    void MouseDown()
    {
        if (!canClick) return;
        Debug.Log("CLICK!!");
        //SetClickTarget(!canClick);

        FieldManager.instance.ClickCharTarget(character);
    }
    void MouseEnter()
    {
        
    }

    private void OnMouseDown()
    {
        MouseDown();
    }
    private void OnMouseEnter()
    {
        MouseEnter();
    }
}
