using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class RetrievePackage : MonoBehaviour {

	[SerializeField]
	InputField input;
	[SerializeField]
	InputField output;
	PinGenerator pincollection;
	
	// Use this for initialization
	void Start () {
		var submitPack = new InputField.SubmitEvent();
		submitPack.AddListener(SubmitPackName);
		input.onEndEdit = submitPack;
		var sendPack = new InputField.SubmitEvent();
		sendPack.AddListener(SendPackage);
		output.onEndEdit = sendPack;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void SubmitPackName(string arg0)
	{
		Debug.Log(arg0);
		if (arg0 == "pins")
		{
			pincollection = PinGenerator.Load(arg0);
		}
		for(int i=0; i < pincollection.Pins.Length; i++)
		{
			Debug.Log(pincollection.Pins[i].desc);
		}
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
}
