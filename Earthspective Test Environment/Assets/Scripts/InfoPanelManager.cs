using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelManager : MonoBehaviour {

    private EventPin selectedPin;

    public Text title;
    public Text content;
    public Image icon;
    public Text date;
    public Button link;
    public Animator anim;
    private bool open = false;
    private float timer = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}


    public void setSelectedPin(EventPin pin)
    {
        selectedPin = pin;
        Transition(); 
    }

    private void Transition()
    {
        Debug.Log("Transition");
        title.text = selectedPin.title;
        content.text = selectedPin.description;
        icon.sprite = selectedPin.icon;

        string boundary = "";
        if (selectedPin.date >= 0)
        {
            boundary = " AD";
        }
        else
        {
            boundary = " BC";
        }
        
        date.text = "" + Math.Abs(selectedPin.date) + boundary;

        link.GetComponentInChildren<Text>().text = selectedPin.link;
    }

    public void OpenWebPage()
    {
        if(selectedPin != null)
        {
            Application.OpenURL(selectedPin.link);
        }
    }

    public void togglePanel(EventPin pin)
    {

        if( open == false)
        {
            setSelectedPin(pin);
            anim.SetTrigger("Open");
            open = true;
            return;
        }

        if(open == true && (pin == null || pin == selectedPin))
        {
            anim.SetTrigger("Close");
            open = false;
            timer = 0f;
            return;
        }

        if(open == true && pin != null && pin != selectedPin)
        {
            StartCoroutine(ExecuteAfterTime(0.6f,pin));
            anim.SetTrigger("Cycle");
            open = true;
            timer = 0f;
            return;
        }
    }

    IEnumerator ExecuteAfterTime(float time,EventPin pin)
    {
        yield return new WaitForSeconds(time);

        setSelectedPin(pin);
    }

}
