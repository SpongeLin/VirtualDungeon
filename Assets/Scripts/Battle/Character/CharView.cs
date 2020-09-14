using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharView : MonoBehaviour
{
    public CharViewUI charViewUI;
    public CharData character;
    bool canClick = false;

    public CharAvatar charAvatar;//??
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


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
        Debug.Log("CLICK!!");
        SetClickTarget(!canClick);
    }
    void MouseEnter()
    {
        FieldManager.instance.battleHubControl.ShowCharInfo(character);
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
