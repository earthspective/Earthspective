//******* Tanner Marshall
//******* Capstone Spring 2017

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InformationPanel : MonoBehaviour
{
    //The current Pin to Load.
    public EventPin eventPin;

    //The UI elements to be filled by the pin.
    public Text title;
    public Text content;
    public Image icon;
    public Text textDate;
    public Button link;

    //Animator to control the panel
    public Animator anim;
    //Helper variables for animator
    private bool open = false;
    private float timer = 0f;

    //Variables from eventPin.
    private Date date;
    private List<string> tags;
    private string imgPath;

    //The default sprite to display if an event has no image. 
    public Sprite defaultSprite;

    //Attempt to open the source if the eventPin has one. 
    public void OpenLink()
    {
        if (eventPin != null)
        {
            Application.OpenURL(eventPin.link);
        }
    }

    //Control the animator to hid the panel while we update the info. 
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
    }
    
    //Wait until the panel is hidden and then update the info.
    IEnumerator ExecuteAfterTime(float time, EventPin pin)
    {
        yield return new WaitForSeconds(time);

        setSelectedPin(pin);
    }

    //Assign the current Pin
    private void setSelectedPin(EventPin pin)
    {
        eventPin = pin;
        Transition();
    }

    //Transition to the new content. 
    private void Transition()
    {
        //Set the UI elements. 
        anim.ResetTrigger("Cycle");
        title.text = eventPin.GetTitle();
        content.text = eventPin.GetDescription();
        StartCoroutine(loadImage());
        imgPath = eventPin.GetIcon();
        date = eventPin.GetDate();

        //Check for the era.
        string boundary = "";
        if (date.GetEra() == true)
        {
            boundary = " AD";
        }
        else
        {
            boundary = " BC";
        }

        //Assign the date. 
        textDate.text = date.GetDate();

        link.GetComponentInChildren<Text>().text = eventPin.link;
    }

    //Load images from the hostsite.
    IEnumerator loadImage()
    {
        var url = "http://capstone.adamcrider.com/images/img_" + eventPin.id + ".jpg";
        WWW www = new WWW(url);
        yield return www;
        if(string.IsNullOrEmpty(www.error))
        {
            icon.sprite = Sprite.Create(www.texture, new Rect(0.0f, 0.0f, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }else
        {
            icon.sprite = defaultSprite;
        }
    }

}
