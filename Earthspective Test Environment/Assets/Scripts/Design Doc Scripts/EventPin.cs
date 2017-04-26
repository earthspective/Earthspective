using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventPin : MonoBehaviour
{
    #region variables
    //*********Class Variables
    //Latitude,Longitude in 00.0000 format. 
    private Vector2 coordinates;

    //Date of the event. 
    public Date date;

    //The main preview image associated with this pin. 
    private Sprite icon;

    //The Title of the Event, ie The Battle of Thermopylae
    private string title;

    //Holder for the paragraph of data. Will be pulled from the list of paragraphs.
    private string description;

    //The url for the source of the info. 
    public string link;
    
    //List of all tags associated with the event. 
    public List<string> sourceTags;

    //List of all tags associated with the event. 
    public List<string> tags;

    //Reference to the script that controls the infopanel. 
    public InformationPanel infoPanel;

    //Reference to the script that controls the timeline. 
    public TimeLine timeLine;

    //Reference to the script that controls the controlPanel;
    public ControlPanel controlPanel;

    //Variable controlling whether the pin should check aditional filter properties. 
    private bool filtered = false;



    //*********Unity Variables
    //The Object that is the 3D label. 
    public GameObject titleObject;

    //The 3D text that appears above the pin. 
    private TextMesh label;

    //3D Object for pin
    public GameObject pinObject;

    //The maximum distance the current date can be from this date and be visible.
    private float maxDistance = 50f;

    //The renderers for the pin's objects, used to control transparency. 
    MeshRenderer pinRender;
    MeshRenderer titleRender;
    #endregion

    
    //Initializes the label and it's contents; 
    void Awake()
    {
        label = titleObject.GetComponent<TextMesh>();
        label.text = title;

        pinRender = pinObject.GetComponent<MeshRenderer>();
        titleRender = titleObject.GetComponent<MeshRenderer>();
    }

    //Checks for mouse clicks to 
    private void OnMouseDown()
    {
        Debug.Log("PinDown");
        infoPanel.DisplayPin(this);
    }
    
    //Handle fading the objects in and out or checking tag filters. 
    public void CheckFilter()
    {

        //If the pin isn't filtered set it's visability based on it's distance from the current date. 
        if (filtered == false)
        { 
            //Find the distance from this date to the current date. 
            float distance = timeLine.CompareDate(date);

            pinRender.material.color = new Color(pinRender.material.color.r, pinRender.material.color.g, pinRender.material.color.b, (maxDistance - distance) / maxDistance);
            titleRender.material.color = new Color(titleRender.material.color.r, titleRender.material.color.g, titleRender.material.color.b, (maxDistance - distance) / maxDistance);
        }
    }

    //Checks it's tag against the control panel to see if it should be filtered by tag. 
    public void SetFilter()
    {
        Debug.Log("SetFilter");

            //If the pin has no tags, it should never be filtered by tags.
        if (tags.Count > 0)
        {
            //If it does have tags, check each tag against it's status in
            foreach (string tag in tags)
            {
                //If at least one tag is true,stop cheking, else set it false and check the next tag. 
                if (controlPanel.tags[tag] == true)
                {
                    filtered = false;
                    break;
                }
                else
                {
                    filtered = true;
                }
            }

            //If filtered finishes as true, set it to transparent. 
            if (filtered == true)
            {
                pinRender.material.color = Color.clear;
                titleRender.material.color = Color.clear;
            }
        }

        foreach (string tag in sourceTags)
        {
            if (controlPanel.tags[tag] == false)
            {
                filtered = true;
                break;
            }
        }
    }

    //Check to see if the tag is present.
    public bool ContainsTag(string tag)
    {
        foreach(string pinTag in tags)
        {
            if(pinTag == tag)
            {
                return true;
            }
        }
        return false;
    }



    //Setters and getters
    public void SetCoordinates(float lat, float lon)
    {
        coordinates.x = lat;
        coordinates.y = lon;
    }

    public Vector2 GetCoordinates() {return coordinates;}

    public void SetDate(Date newDate) { date = newDate; }

    public Date GetDate(){return date;}

    public void SetIcon(Sprite sprite) { icon = sprite;}

    public Sprite GetIcon() {return icon;}

    public void SetTitle(string newTitle) { title = newTitle;}

    public string GetTitle() {return title;}

    public void SetDescription(string desc) { description = desc; }

    public string GetDescription() {return description;}

}
