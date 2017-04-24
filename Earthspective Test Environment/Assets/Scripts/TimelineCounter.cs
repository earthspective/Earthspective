using System;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TimelineCounter : MonoBehaviour {

    public Slider timeLine;
    public InputField inField;

    public int date;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        date = (int)timeLine.value;
	}

    public void UpdateTimeLine()
    {
        timeLine.value = Convert.ToInt32(inField.text);
    }

    public void UpdateInField()
    {
        string boundary = "";
        if(timeLine.value >= 0)
        {
            boundary = " AD";
        }else
        {
            boundary = " BC";
        }

        inField.text = ""+ Math.Abs(timeLine.value)+boundary;
    }

    public void Increment(int step)
    {
        timeLine.value = timeLine.value + step;
    }

}
