using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeLine : MonoBehaviour {

    public Slider timeLine;
    public InputField inField;

    private Date currentDate;
    private int yearValue;


    //Update the current date from user input. 
    public void ChangeDate(int step)
    {
        currentDate.SetDate(1,1,currentDate.GetYear()+step);
    }

    public bool CompareDate(Date checkDate, int maxDistance)
    {

         float distance = Mathf.Abs(checkDate.GetYear() - currentDate.GetYear());

        if ((maxDistance - distance) / maxDistance == 1)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
