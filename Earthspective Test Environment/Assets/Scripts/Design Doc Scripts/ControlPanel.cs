using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject eventPin;


    private int inc = 0;

    // Use this for initialization
    void Start()
    {
        tags.Add("Invention", true);
        tags.Add("Military", true);
        tags.Add("Political", true);
        tags.Add("Discovery", true);
        tags.Add("Custom Pins", true);
    }

    //Filter every pin based on tags. 
    private void FilterPins()
    {
        foreach (EventPin pin in eventPins)
        {
            pin.SetFilter();
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

}
