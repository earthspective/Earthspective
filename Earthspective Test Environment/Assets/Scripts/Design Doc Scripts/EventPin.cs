using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventPin : MonoBehaviour
{

    //*********Variables to be loaded from XML*********

    //Preferably in a decimal format ie 92.2345 -23.3428 but the 34°44′10″N 92°19′52″W format can work too.
    //We just need to pick one or the other. 
    public float latitude;
    public float longitude;

    //The Title of the Event, ie The Battle of Thermopylae
    public string title;

    //The images associated with the Event. images[0] will be assigned to the icon. 
    public List<Sprite> images;

    //Contains the strings to fill in the content section. Not sure the best way to implement for more complex formats.
    public List<Text> paragraphs;

    //The main preview image associated with this pin. 
    public Sprite icon;

    //Holder for the paragraph of data. Will be pulled from the list of paragraphs.
    public string description;

    //The url for the source of the info. 
    public string link;

    //List of all tags associated with the event. 
    public List<string> tags;




    //Unity Variables
    public TextMesh label;

    public InfoPanelManager infoPanelManger;

    public GameObject pinObject;

    public GameObject titleObject;

    public ParticleSystem particles;

    public bool particlePlayed;


    public int date;

    public float maxDistance = 50f;

    // Use this for initialization
    void Awake()
    {
        label.text = title;
    }

    private void OnMouseDown()
    {
        Debug.Log("PinDown");
        infoPanelManger.togglePanel(this);
    }


    //Design Doc functions to be implemented. 

    //Handle fading the objects in and out or turn off the pin? 
    public void CheckFilter(float currentDate)
    {
        float distance = Mathf.Abs(date - currentDate);

        MeshRenderer pinRender = pinObject.GetComponent<MeshRenderer>();
        MeshRenderer titleRender = titleObject.GetComponent<MeshRenderer>();

        pinRender.material.color = new Color(pinRender.material.color.r, pinRender.material.color.g, pinRender.material.color.b, (maxDistance -distance)/maxDistance);
        titleRender.material.color = new Color(titleRender.material.color.r, titleRender.material.color.g, titleRender.material.color.b, (maxDistance - distance)/maxDistance);

        //Play particles when the event occurs. 
        if (particles != null)
        {
            if ((maxDistance - distance) / maxDistance == 1)
            {
                if (particlePlayed == false)
                {
                    particlePlayed = true;
                    particles.Play();
                }
            }
            else
            {
                particlePlayed = false;
            }
        }
    }

    public void SetDate(int newDate)
    {
        date = newDate;
    }

    public float GetDate()
    {
        return date;
    }

    public string GetDescription()
    {
        return description;
    }

    public string GetTitle()
    {
        return title;
    }

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

    public void SetCoordinates(float lat, float lon)
    {
        latitude = lat;
        longitude = lon;
    }








}
