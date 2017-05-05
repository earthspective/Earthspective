using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeLine : MonoBehaviour
{

    public Date currentDate;
    public Slider timeLine;
    public InputField inField;
    public ControlPanel cPanel;
    public Text era;


    public void Awake()
    {
        currentDate.SetDate(1, 1, 1);
        UpdatefromTimeline();
        UpdateInField();
    }


    //Update the current date from user input. 
    public void ChangeDate(int step)
    {
        currentDate.SetDate(1, 1, currentDate.GetYear() + step);
        UpdateInField();
        cPanel.FilterPins();
    }

    //Returns the distance in years between checkDate and the currentDate. 
    public float CompareDate(Date checkDate)
    {
        return Mathf.Abs(checkDate.GetYear() - currentDate.GetYear());
        cPanel.FilterPins();
    }

    //Updates the date when the timeline slider changes
    public void UpdatefromTimeline()
    {
        currentDate.SetDate(currentDate.GetDay(), currentDate.GetMonth(), (int)timeLine.value);
        UpdateInField();
        cPanel.FilterPins();
        cPanel.year.text = timeLine.value.ToString();
        if(timeLine.value < 0)
        {
            cPanel.era.value = 1;
        }else
        {
            cPanel.era.value = 0;
        }
        cPanel.era.RefreshShownValue();
    }

    //Updates the timeline when the date was changed by the input field
    public void UpdateTimeLine()
    {
        timeLine.value = Convert.ToInt32(inField.text);
        cPanel.FilterPins();
    }

    //Updates the value of the InputField. 
    public void UpdateInField()
    {
        string boundary = "";
        if (currentDate.GetEra() == true)
        {
            boundary = " AD";
        }
        else
        {
            boundary = " BC";
        }
        era.text = boundary;
        inField.text = "" + Math.Abs(currentDate.GetYear());
    }

    //Returns the current date. 
    public Date GetCurrentDate()
    {
        return currentDate;
    }

}
