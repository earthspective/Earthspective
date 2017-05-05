using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

public class Pin  {

	[XmlAttribute("title")]
	public string title;
	
	[XmlAttribute("desc")]
	public string desc;
	
	[XmlAttribute("x")]
	public string pos_x;
	
	[XmlAttribute("y")]
	public string pos_y;
	
	[XmlAttribute("year")]
	public string year;
	
}
