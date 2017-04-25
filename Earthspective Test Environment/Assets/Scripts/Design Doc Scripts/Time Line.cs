using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeLine : MonoBehaviour {

    private Date currentDate;
    public Slider timeLine;
    public InputField inField;

    //Update the current date from user input. 
    public void ChangeDate(int step)
    {
        currentDate.SetDate(1,1,currentDate.GetYear()+step);
    }

    //Returns the distance in years between checkDate and the currentDate. 
    public float CompareDate(Date checkDate)
    {
         return Mathf.Abs(checkDate.GetYear() - currentDate.GetYear());
    }

    //Updates the date when the timeline slider changes
    public void UpdatefromTimeline()
    {
        currentDate.SetDate(currentDate.GetDay(), currentDate.GetMonth(), (int)timeLine.value);
    }

    //Updates the timeline when the date was changed by the input field
    public void UpdateTimeLine()
    {
        timeLine.value = Convert.ToInt32(inField.text);
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

        inField.text = "" + Math.Abs(currentDate.GetYear()) + boundary;
    }

    //Returns the current date. 
    public Date GetCurrentDate()
    {
        return currentDate;
    }
    
}
