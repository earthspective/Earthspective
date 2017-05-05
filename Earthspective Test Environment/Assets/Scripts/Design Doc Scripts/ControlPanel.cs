//******* Tanner Marshall
//******* Capstone Spring 2017


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class ControlPanel : MonoBehaviour
{
    //List of all eventPins.
    public List<EventPin> eventPins;

    //List of all tags to check. 
    public Dictionary<string, bool> tags = new Dictionary<string, bool>();

    //Referneces to the other manager scripts. 
    public InformationPanel infoPanel;
    public TimeLine timeLine;

    public List<Toggle> tagToggles;

    public GameObject packTogglePrefab;
    public GameObject packToggleParent;

    //Planet variables for placing the pins. 
    public GameObject planet;
    public GameObject axis;
    public GameObject stylus;

    //List of prefab models to use for pins. 
    public List<GameObject> eventPinPrefabs;

    //Input fields for creating pins. 
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

    //Input and output fields 
    [SerializeField]
    InputField input;
    [SerializeField]
    InputField output;
    PinGenerator pincollection;


    // Use this for initialization
    void Start()
    {
        tags.Add("Invention", true);
        tags.Add("Military", true);
        tags.Add("Political", true);
        tags.Add("Discovery", true);
        tags.Add("Custom Pins", true);
        tags.Add("Default Pins", true);
        MonthDropDownChange();
        year.text = "0";

        SubmitPackName("pins");
        
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
        if(input.text != "")
        {
            SubmitPackName(input.text);
            var pack = Instantiate(packTogglePrefab, packToggleParent.transform);
            pack.SetActive(true);
            tagToggles.Add(pack.GetComponent<Toggle>());
            pack.name = pack.name+inc;
            tags.Add(pack.name, true);
            inc++;

        }
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
        bool createSuccessfully = true;

        GameObject pin;
       
        if(customPinTag[0].isOn == true)
        {
            pin = Instantiate(eventPinPrefabs[0], planet.transform);
        }else if (customPinTag[1].isOn == true)
        {
            pin = Instantiate(eventPinPrefabs[1], planet.transform);
        }else if (customPinTag[2].isOn == true)
        {
            pin = Instantiate(eventPinPrefabs[2], planet.transform);
        }else if (customPinTag[3].isOn == true)
        {
            pin = Instantiate(eventPinPrefabs[3], planet.transform);
        }else {
            pin = Instantiate(eventPinPrefabs[4], planet.transform);
        }
        
        Date date = pin.GetComponent<Date>();
        pin.GetComponent<EventPin>().SetDate(date);

        if (era.value == 0)
        {
            createSuccessfully = date.SetDate(day.value + 1, month.value + 1, Convert.ToInt32(year.text));
        }
        if(era.value == 1)
        {
            createSuccessfully = date.SetDate(day.value + 1, month.value + 1, 0 - Convert.ToInt32(year.text));
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
        if (lat.text != "" && lon.text != "")
        {
            pin.GetComponent<EventPin>().SetCoordinates(float.Parse(lat.text), float.Parse(lon.text));
            PlacePin(pin.GetComponent<EventPin>());
        }else
        {
            createSuccessfully = false;
            //start manual placment
        }

        pin.GetComponent<EventPin>().timeLine = timeLine;

        pin.GetComponent<EventPin>().controlPanel = this;

        pin.GetComponent<EventPin>().infoPanel = infoPanel;


        if(title.text == "") { createSuccessfully = false; }

        
        if(createSuccessfully == true)
        {
            title.text = "";
            lat.text = "";
            lon.text = "";
            year.text = "1";
            month.value = 0;
            day.value = 0;
            foreach(Toggle tog in customPinTag)
            {
                tog.isOn = false;
            }
            eventPins.Add(pin.GetComponent<EventPin>());
            FilterPins();
        }
        else
        {
            Destroy(pin);
        }



    }

    //Set the number of days in the day dropdown menu depending on the month. 
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

        day.value = 0;
        day.RefreshShownValue();

    }

    //Place the pin.
    void PlacePin(EventPin pin)
    {
        axis.transform.localRotation = Quaternion.Euler(0, -pin.GetCoordinates().x, pin.GetCoordinates().y);

        RaycastHit hit;

        if (Physics.Raycast(stylus.transform.position, stylus.transform.forward, out hit))
        {
            pin.gameObject.transform.position = hit.point;
            pin.gameObject.transform.rotation = Quaternion.LookRotation(hit.normal);
            pin.gameObject.transform.parent = planet.transform;
        }
    }
    
    //Loop through the downloaded pins and create eventPin objects from their data. 
    void LoadEventPack() {

        char[] delimiterChars = { ' ', ',', '.', ':','-', ':', '\t' };

        for (int i = 0; i < pincollection.Pins.Length; i++)
        {
            GameObject pin = this.gameObject;

            string xmltags = pincollection.Pins[i].tags;
            string[]indTags = xmltags.Split(delimiterChars);
            
            foreach (string tag in indTags)
            {
                
                //Find tags to instantiate appropriate pin model.
                if (tag == "Military")
                {
                    pin = Instantiate(eventPinPrefabs[0], planet.transform);
                    pin.GetComponent<EventPin>().tags.Add("Military");
                }
                else if (tag == "Invention")
                {
                    pin = Instantiate(eventPinPrefabs[1], planet.transform);
                    pin.GetComponent<EventPin>().tags.Add("Invention");
                }
                else if (tag == "Discovery")
                {
                    pin = Instantiate(eventPinPrefabs[2], planet.transform);
                    pin.GetComponent<EventPin>().tags.Add("Discovery");
                }
                else if (tag == "Political")
                {
                    pin = Instantiate(eventPinPrefabs[3], planet.transform);
                    pin.GetComponent<EventPin>().tags.Add("Political");
                }else
                {
                    pin = Instantiate(eventPinPrefabs[4], planet.transform);
                }
            }
            

            //Setup the Date
            pin.GetComponent<Date>();

            Date date = pin.GetComponent<Date>();
            pin.GetComponent<EventPin>().SetDate(date);

            string xmlDate = pincollection.Pins[i].year;
            string[] indDate = xmlDate.Split(delimiterChars);
            

            if(Convert.ToInt32(indDate[0]) > 0)
            {
                date.SetDate(Convert.ToInt32(indDate[2]) + 1, Convert.ToInt32(indDate[1])+ 1, Convert.ToInt32(indDate[0]));
            }
            else
            {
                date.SetDate(Convert.ToInt32(indDate[2]) + 1, Convert.ToInt32(indDate[1]) + 1, 0 - Convert.ToInt32(indDate[0]));
            }


            //Add the Text components. 
            pin.GetComponent<EventPin>().SetTitle(pincollection.Pins[i].title);
            pin.GetComponent<EventPin>().SetDescription(pincollection.Pins[i].desc);

            //Ad the sourceTag
            pin.GetComponent<EventPin>().sourceTags.Add("Default Pins");

            //Set the coordinates for the pin. 
            pin.GetComponent<EventPin>().SetCoordinates(float.Parse(pincollection.Pins[i].pos_y), float.Parse(pincollection.Pins[i].pos_x));

            //Place the pin on the globe. 
            PlacePin(pin.GetComponent<EventPin>());

            //Store references to the manager scripts in the pin. 
            pin.GetComponent<EventPin>().controlPanel = this;
            pin.GetComponent<EventPin>().timeLine = timeLine;
            pin.GetComponent<EventPin>().infoPanel = infoPanel;

            //Assign the Pin ID
            pin.GetComponent<EventPin>().id = pincollection.Pins[i].id;

            //Add this pin to the list. 
            eventPins.Add(pin.GetComponent<EventPin>());

        }
    }












    //Kyle Hooks
   
    private void SubmitPackName(string arg0)
    {
        Debug.Log(arg0);
        pincollection = PinGenerator.Load(arg0);

        LoadEventPack();
    }

    private void SendPackage(string path)
    {
        var xmlString = System.IO.File.ReadAllText(Path.Combine(Application.dataPath, "./XML/") + path + ".xml");
        var url = "http://capstone.adamcrider.com/" + path;
        var form = new WWWForm();
        form.AddField("testData", xmlString);
        WWW www = new WWW(url, form);

        StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        if (www.error == null)
        {
            Debug.Log("WWW sent!: " + www.text);
        }
        else Debug.Log("Error: " + www.error);
    }

    [XmlArray("Pins"), XmlArrayItem("Pin")]
    public Pin[] Pins;

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(PinGenerator));
        using (var stream = new FileStream(Path.Combine(Application.dataPath, "./XML/") + path + ".xml", FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static PinGenerator Load(string path)
    {
        WWW www = new WWW("http://capstone.adamcrider.com/" + path);
        while (!www.isDone)
        {
            Debug.Log("downloaded " + (www.progress.ToString()));
        }
        if (www.error != null)
        {
            Debug.Log("Failed to download, using cached pins");
        }
        else
        {
            Debug.Log("Downloaded pins, overwriting local version.");
            File.WriteAllBytes(Path.Combine(Application.dataPath, "./XML/") + path + ".xml", www.bytes);
        }

        var serializer = new XmlSerializer(typeof(PinGenerator));
        using (var stream = new FileStream(Path.Combine(Application.dataPath, "./XML/") + path + ".xml", FileMode.Open))
        {
            return serializer.Deserialize(stream) as PinGenerator;
        }
    }
}


[XmlRoot("PinCollection")]
public class PinGenerator
{

    [XmlArray("Pins"), XmlArrayItem("Pin")]
    public Pin[] Pins;

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(PinGenerator));
        using (var stream = new FileStream(Path.Combine(Application.dataPath, "./XML/") + path + ".xml", FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static PinGenerator Load(string path)
    {
        WWW www = new WWW("http://capstone.adamcrider.com/" + path);
        while (!www.isDone)
        {
            //Debug.Log("downloaded " + (www.progress.ToString()));
        }
        if (www.error != null)
        {
            Debug.Log("Failed to download, using cached pins");
        }
        else
        {
            Debug.Log("Downloaded pins, overwriting local version.");
            File.WriteAllBytes(Path.Combine(Application.dataPath, "./XML/") + path + ".xml", www.bytes);
        }

        var serializer = new XmlSerializer(typeof(PinGenerator));
        using (var stream = new FileStream(Path.Combine(Application.dataPath, "./XML/") + path + ".xml", FileMode.Open))
        {
            return serializer.Deserialize(stream) as PinGenerator;
        }
    }
}
