﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Date : MonoBehaviour {

    public int day;
    public int month;
    public int year;
    public bool era;

    //Update the class variables
    public bool SetDate(int setDay, int setMonth, int setYear)
    {
        if (setDay > 0)
        {
            //Check the month to see how many days should be allowed. 
            #region switchMonth
            switch (setMonth)
            {
                case 1:
                    if (setDay <= 31)
                    {
                        day = setDay;
                    }
                    else { return false; }
                    break;
                case 2:
                    if (isLeapYear(setYear) == false)
                    {
                        if (setDay <= 28)
                        {
                            day = setDay;
                        }
                        else { return false; }
                         break;
                    }
                    else
                    {
                        if (setDay <= 29)
                        {
                            day = setDay;
                        }
                        else { return false; }
                    }
                    break;
                case 3:
                    if (setDay <= 31)
                    {
                        day = setDay;
                    }
                    else { return false; }
                    break;
                case 4:
                    if (setDay <= 30)
                    {
                        day = setDay;
                    }
                    else { return false; }
                    break;
                case 5:
                    if (setDay <= 31)
                    {
                        day = setDay;
                    }
                    else { return false; }
                    break;
                case 6:
                    if (setDay <= 30)
                    {
                        day = setDay;
                    }
                    else { return false; }
                    break;
                case 7:
                    if (setDay <= 31)
                    {
                        day = setDay;
                    }
                    else { return false; }
                    break;
                case 8:
                    if (setDay <= 31)
                    {
                        day = setDay;
                    }
                    else { return false; }
                    break;
                case 9:
                    if (setDay <= 30)
                    {
                        day = setDay;
                    }
                    else { return false; }
                    break;
                case 10:
                    if (setDay <= 31)
                    {
                        day = setDay;
                    }
                    else { return false; }
                    break;
                case 11:
                    if (setDay <= 30)
                    {
                        day = setDay;
                    }
                    else { return false; }
                    break;
                case 12:
                    if (setDay <= 31)
                    {
                        day = setDay;
                    }
                    else { return false; }
                    break;
                default:
                    return false;
            }

            month = setMonth;

            #endregion

            //Determine if the year is within bounds and what era it is in. 
            if(setYear >= -10000 && setYear < 2100)
            {
                if (setYear == 0 && era == true)
                {
                    setYear = -1;
                }

                if (setYear == 0 && era == false)
                {
                    setYear = 1;
                }
                year = setYear;
            }
            else { return false; }

            if (year > 0)
            {
                era = true;
            }

            if(year < 0)
            {
                era = false;
            }
            return true;
        }
        else { return false; }
    }

    //Checks to see if the year is a leap year to control the number of days in february.
    public bool isLeapYear(int checkYear)
    {
        if (checkYear % 4 == 0 && checkYear % 100 == 0 && checkYear % 400 == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Returns a string representation of the date. 
    public string GetDate()
    {
        string date = "";

        #region switchMonth
        switch (month)
        {
            case 1:
                date += "January";
                break;
            case 2:
                date += "February";
                break;
            case 3:
                date += "March";
                break;
            case 4:
                date += "April";
                break;
            case 5:
                date += "May";
                break;
            case 6:
                date += "June";
                break;
            case 7:
                date += "July";
                break;
            case 8:
                date += "August";
                break;
            case 9:
                date += "September";
                break;
            case 10:
                date += "October";
                break;
            case 11:
                date += "November";
                break;
            case 12:
                date += "December";
                break;
        }
        #endregion

        date += " " + day;
        date += ", " + Math.Abs(year);

        if(era == false) {date += " BC";}
        else {date += " CE";}
        return date;
    }

    //Return each class variable. 
    public int GetDay() { return day; }

    public int GetMonth() { return month; }

    public int GetYear() { return year;}

    public bool GetEra() { return era; }

}
