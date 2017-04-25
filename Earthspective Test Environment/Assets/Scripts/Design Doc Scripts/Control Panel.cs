using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour {

    List<EventPin> eventPins;
    public Dictionary<string, bool> tags;

    public InformationPanel infoPanel;

	// Use this for initialization
	void Start () {
        tags.Add("Invention", true);
        tags.Add("Military", true);
        tags.Add("Political", true);
        tags.Add("Discovery", true);
	}


    private void FilterPins() {
        foreach(EventPin pin in eventPins)
        {
            pin.SetFilter();
        }
    }

    private void ImportPack() { }

    private void CreateEvenet() { }

}
