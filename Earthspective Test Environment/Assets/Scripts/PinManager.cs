using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;


public class PinManager : MonoBehaviour
{

    //public float latitude;
    // public float longitude;

    public GameObject stylus;

    public GameObject pinPrefab;

    public GameObject planet;

    public List<EventPin> eventPins = new List<EventPin>();

    private List<EventPin> addedPins = new List<EventPin>();

    public InfoPanelManager infoPanelManger;

    public TimelineCounter timeline;
	
	PinGenerator pincollection;
	
	public GameObject[] generated_pins;

    // Use this for initialization
    void Start()
    {
        /*foreach (EventPin pin in eventPins)
        {
            PlacePin(pin);
        }*/
		pincollection = PinGenerator.Load(Path.Combine(Application.dataPath, "./XML/pins.xml"));
		for(int i=0; i < pincollection.Pins.Length; i++)
		{
			var generated_pin = pinPrefab;
			var pin = pincollection.Pins[i];
			//var tempPin = (GameObject)Instantiate(generated_pin, new Vector3 (float.Parse(pin.pos_x), float.Parse(pin.pos_y), float.Parse(pin.pos_z)), Quaternion.identity);
			//tempPin.transform.parent = gameObject.transform;
			eventPins[0].SetCoordinates(float.Parse(pin.pos_x), float.Parse(pin.pos_y));
			eventPins[0].GetComponent<EventPin>().title = pin.title;
			eventPins[0].GetComponent<EventPin>().description = pin.desc;
			eventPins[0].GetComponent<EventPin>().date = int.Parse(pin.year);
			PlacePin(eventPins[0]);
		}
    }

    // Update is called once per frame
    void Update()
    {
        foreach (EventPin pin in addedPins)
        {
            //int gap = Mathf.Abs(pin.date - timeline.date);
            //Debug.Log(gap);
            //if(gap > 10)
            //{
            //    Debug.Log("SetFalse");
            //    pin.gameObject.SetActive(false);
            //}
            //else
            //{
            //    Debug.Log("SetTrue");
            //    pin.gameObject.SetActive(true);
            //}
            pin.CheckFilter(timeline.date);
        }
    }

    void PlacePin(EventPin pin)
    {
        this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, -pin.longitude, pin.latitude);

        RaycastHit hit;

        if (Physics.Raycast(stylus.transform.position, stylus.transform.forward, out hit))
        {
            var placedPin = Instantiate(pin, hit.point, Quaternion.LookRotation(hit.normal));

            placedPin.transform.parent = planet.transform;

            placedPin.infoPanelManger = this.infoPanelManger;

            addedPins.Add(placedPin);
        }
    }
}
