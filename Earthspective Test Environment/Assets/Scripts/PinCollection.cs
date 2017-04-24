using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("PinCollection")]
public class PinCollection {

	[XmlArray("pins")]
	[XmlArrayItem("pin")]
	public List<Pin> pins = new List<Pin>();
	
}
