using UnityEngine;
using System.Xml;
using System.Collections;

public class LoadDialogueXML : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    XmlDocument doc = new XmlDocument();
        doc.Load("test.xml");
        //Debug.Log(doc.FirstChild["speechelements"].FirstChild["text"].InnerText);
        XmlElement speechelements = doc.FirstChild["speechelements"];
	    foreach (XmlElement speechelement in speechelements.ChildNodes)
	    {
	        Debug.Log(speechelement["text"].InnerText);
	    }
        
        

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
