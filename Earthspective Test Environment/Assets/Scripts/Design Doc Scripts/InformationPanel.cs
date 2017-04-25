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
            anim.SetTrigger("Close");
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
        Debug.Log("Transition");

        title.text = eventPin.GetTitle();
        content.text = eventPin.GetDescription();
        icon.sprite = eventPin.GetIcon();
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

        textDate.text = "" + Math.Abs(date.GetYear()) + boundary;

        link.GetComponentInChildren<Text>().text = eventPin.link;
    }

}
