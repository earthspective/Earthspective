using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InformationPanel : MonoBehaviour
{

    public EventPin eventPin;

    public Text title;
    public Text content;
    public Image icon;
    public Text textDate;
    public Button link;
    public Animator anim;

    private Date date;
    private List<string> tags;
    private bool open = false;
    private float timer = 0f;

    private string imgPath;

    public GameObject panelContent;


    public void OpenLink()
    {
        if (eventPin != null)
        {
            Application.OpenURL(eventPin.link);
        }
    }

    public void DisplayPin(EventPin pin)
    {

        if (open == false)
        {
            anim.SetTrigger("Open");
            open = true;
            return;
        }

        if (open == true && (pin == null || pin == eventPin))
        {
            anim.ResetTrigger("Open");
            open = false;
            timer = 0f;
            return;
        }

        if (open == true && pin != null && pin != eventPin)
        {
            StartCoroutine(ExecuteAfterTime(0.6f, pin));
            anim.SetTrigger("Cycle");
            open = true;
            timer = 0f;
            return;
        }


        content.GetComponent<ContentSizeFitter>().enabled = false;
        content.GetComponent<ContentSizeFitter>().enabled = true;

        panelContent.GetComponent<ContentSizeFitter>().enabled = false;
        panelContent.GetComponent<ContentSizeFitter>().enabled = true;

    }




    IEnumerator ExecuteAfterTime(float time, EventPin pin)
    {
        yield return new WaitForSeconds(time);

        setSelectedPin(pin);
    }

    private void setSelectedPin(EventPin pin)
    {
        eventPin = pin;
        Transition();
    }

    private void Transition()
    {
        anim.ResetTrigger("Cycle");
        title.text = eventPin.GetTitle();
        content.text = eventPin.GetDescription();
        loadImage();
        //icon.sprite = eventPin.GetIcon();
        imgPath = eventPin.GetIcon();
        date = eventPin.GetDate();

        string boundary = "";
        if (date.GetEra() == true)
        {
            boundary = " AD";
        }
        else
        {
            boundary = " BC";
        }
        Debug.Log(date.GetDay());

        textDate.text = date.GetDate();

        link.GetComponentInChildren<Text>().text = eventPin.link;

    }

    IEnumerator loadImage()
    {
        //var url = "http://capstone.adamcrider.com/" + imgPath;
        var url = "http://solarviews.com/raw/earth/bluemarblewest.jpg";
        WWW www = new WWW(url);
        yield return www;
        icon.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
    }

}
