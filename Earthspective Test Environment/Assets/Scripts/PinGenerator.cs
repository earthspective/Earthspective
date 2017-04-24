using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

[XmlRoot("PinCollection")]
public class PinGenerator
{

    [XmlArray("Pins"), XmlArrayItem("Pin")]
    public Pin[] Pins;

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(PinGenerator));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static PinGenerator Load(string path)
    {
        WWW www = new WWW("http://capstone.adamcrider.com/pins/");
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
            File.WriteAllBytes(path, www.bytes);
        }

        var serializer = new XmlSerializer(typeof(PinGenerator));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as PinGenerator;
        }
    }



}

