using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IntroViewControl : MonoBehaviour
{
    public GameObject introView;
    public Text IntroName;
    public Text IntroText;
    bool viewActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (viewActive)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            introView.transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }

        //introView.transform.GetComponent<RectTransform>().pivot = new Vector2(1, 0);
    }
    public void OpenIntroView(IntroViewContent content,bool right)
    {
        IntroName.text = content.GetName();
        if (right)
            introView.transform.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
        else
            introView.transform.GetComponent<RectTransform>().pivot = new Vector2(1, 0);
        introView.SetActive(true);
        viewActive = true;
    }
    public void CloseIntroView()
    {
        introView.SetActive(false);
        viewActive = false;
    }
}
