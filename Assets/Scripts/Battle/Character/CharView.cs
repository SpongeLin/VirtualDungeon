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

    bool targetingAnim = false;
    public void SetClickTarget(bool active)
    {
        canClick = active;
        if (active)
        {
            //animator.SetTrigger("selected");
        }
        else
        {
            if (targetingAnim)
            {
                animator.SetTrigger("notSelected");
                targetingAnim = false;
            }
        }
    }

    void MouseDown()
    {
        if (!canClick) return;
        Debug.Log("CLICK!!");
        //SetClickTarget(!canClick);

        FieldManager.instance.ClickCharTarget(character);
    }

    private void OnMouseDown()
    {
        MouseDown();
    }
    private void OnMouseEnter()
    {
        if (!canClick) return;

        //punch select highlight
        if (!targetingAnim)
        {
            animator.SetTrigger("selected");
            targetingAnim = true;
        }

        FieldManager.instance.currentMouseOverCharacter = character;
    }
    private void OnMouseExit()
    {
        if (!canClick) return;

        if (targetingAnim)
        {
            animator.SetTrigger("notSelected");
            targetingAnim = false;
        }

        FieldManager.instance.currentMouseOverCharacter = null;
    }
}
