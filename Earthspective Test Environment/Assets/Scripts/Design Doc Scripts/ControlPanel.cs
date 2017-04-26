using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ControlPanel : MonoBehaviour
{

    public List<EventPin> eventPins;
    public Dictionary<string, bool> tags = new Dictionary<string, bool>();

    public InformationPanel infoPanel;
    public TimeLine timeLine;

    public List<Toggle> tagToggles;


    public GameObject packTogglePrefab;
    public GameObject packToggleParent;


    public GameObject stylus;

    public GameObject planet;

    public GameObject axis;



    public GameObject eventPin;


    public InputField title;
    public InputField lat;
    public InputField lon;
    public InputField description;
    public InputField year;
    public Dropdown era;
    public Dropdown month;
    public Dropdown day;
    public List<Toggle> customPinTag;

    private int daysInMonth=1;



    private int inc = 0;

    // Use this for initialization
    void Start()
    {
        tags.Add("Invention", true);
        tags.Add("Military", true);
        tags.Add("Political", true);
        tags.Add("Discovery", true);
        tags.Add("Custom Pins", true);
        MonthDropDownChange();
        year.text = "0";
    }

    //Filter every pin based on tags. 
    public void FilterPins()
    {
        foreach (EventPin pin in eventPins)
        {
            pin.SetFilter();
            pin.CheckFilter();
        }
    }

    //Import a group of pins, creating a new source tag. 
    public void ImportPack() {

        var pack = Instantiate(packTogglePrefab, packToggleParent.transform);
        pack.SetActive(true);
        //Change the name to match the import name. 
        tagToggles.Add(pack.GetComponent<Toggle>());
        pack.name = pack.name+inc;
        tags.Add(pack.name, true);
        inc++;

        FilterPins();
    }

    //Remove a given pack from teh scene. 
    public void RemovePack(Toggle tog)
    {
        //Remove all pins with pack as parent.
        tagToggles.Remove(tog);
        tags.Remove(tog.name);
        Destroy(tog.gameObject);
    }

    //Create a custom event and add it to the scene. 
    public void CreateEvenet() { }

    //Update the value of a tag and filter pins.
    public void ToggleTag(Toggle tog)
    {
        tags[tog.name] = tog.isOn;
        FilterPins();
    }

    //Add toggles to the list of tags
    public void AddToggleTag(Toggle tog)
    {
        tags.Add(tog.name, tog.isOn);
    }

    //Creates a custom pin.
    public void CreateCustomPin()
    {
        var pin = Instantiate(eventPin);
        Date date = pin.gameObject.AddComponent<Date>();
        pin.GetComponent<EventPin>().SetDate(date);
        if (era.value == 0)
        {
            Debug.Log("0");
            date.SetDate(Convert.ToInt32(year.text), month.value + 1, day.value + 1);
        }
        if(era.value == 1)
        {
            Debug.Log("1");
            date.SetDate(0 - Convert.ToInt32(year.text), month.value + 1, day.value + 1);
        }
        pin.GetComponent<EventPin>().SetTitle(title.text);
        pin.GetComponent<EventPin>().SetDescription(description.text);
        foreach(Toggle tog in customPinTag)
        {
            if(tog.isOn == true)
            {
                pin.GetComponent<EventPin>().tags.Add(tog.name);
            }
        }
        pin.GetComponent<EventPin>().sourceTags.Add("Custom Pins");
        if (lat.text != "" && lon.text != "null")
        {
            pin.GetComponent<EventPin>().SetCoordinates(float.Parse(lat.text), float.Parse(lon.text));
            PlacePin(pin.GetComponent<EventPin>());
        }else
        {
            //start manual placment
        }

        pin.GetComponent<EventPin>().timeLine = timeLine;

        pin.GetComponent<EventPin>().controlPanel = this;

        pin.GetComponent<EventPin>().infoPanel = infoPanel;

        eventPins.Add(pin.GetComponent<EventPin>());
        FilterPins();
    }

    public void MonthDropDownChange()
    {
        switch (month.value)
        {
            case 0:
                daysInMonth = 31;
                break;
            case 1:
                Debug.Log(year.text);
                if (Convert.ToInt32(year.text) % 4 == 0 && Convert.ToInt32(year.text) % 100 == 0 && Convert.ToInt32(year.text) % 400 == 0)
                {
                    daysInMonth = 29;
                }
                else
                {
                    daysInMonth = 28;
                }

                break;
            case 2:
                daysInMonth = 31;
                break;
            case 3:
                daysInMonth = 30;
                break;
            case 4:
                daysInMonth = 31;
                break;
            case 5:
                daysInMonth = 30;
                break;
            case 6:
                daysInMonth = 31;
                break;
            case 7:
                daysInMonth = 31;
                break;
            case 8:
                daysInMonth = 30;
                break;
            case 9:
                daysInMonth = 31;
                break;
            case 10:
                daysInMonth = 30;
                break;
            case 11:
                daysInMonth = 31;
                break;
        }

        day.ClearOptions();
        for (int i = 1; i <= daysInMonth; i++)
        {
            day.options.Add(new Dropdown.OptionData() { text = i.ToString() });
        }
    }


    void PlacePin(EventPin pin)
    {
        axis.transform.rotation = Quaternion.Euler(axis.transform.rotation.eulerAngles.x, -pin.GetCoordinates().x, pin.GetCoordinates().y);

        RaycastHit hit;

        if (Physics.Raycast(stylus.transform.position, stylus.transform.forward, out hit))
        {
            pin.gameObject.transform.position = hit.point;
            pin.gameObject.transform.rotation = Quaternion.LookRotation(hit.normal);
            pin.gameObject.transform.parent = planet.transform;
        }


    }

}
