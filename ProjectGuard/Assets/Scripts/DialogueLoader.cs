using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
    private JObject json;
    // Use this for initialization
    void Start ()
    {
        /*
		XmlDocument doc = new XmlDocument();
		doc.Load("test.xml");
		//Debug.Log(doc.FirstChild["speechelements"].FirstChild["text"].InnerText);
		XmlElement speechelements = doc.FirstChild["speechelements"];
        foreach (XmlElement speechelement in speechelements.ChildNodes)
        {
            Debug.Log(speechelement["text"].InnerText);
        }
        */
        StreamReader sr = File.OpenText("test.json");
        //string all = sr.ReadToEnd();
        json = (JObject)JToken.ReadFrom(new JsonTextReader(sr));

        GetDialogueNode(0);

    }
	
    // Update is called once per frame
    void Update () {
	    
    }

    public DialogueNode GetDialogueNode(int id)
    {
        JArray nodes = (JArray) json["speechelements"];
        JArray speakers = (JArray) json["speakers"];

        JToken correctNode = nodes.First(e => e["id"].Value<int>() == id);

        JToken speaker = speakers.First(e => e["name"].Value<string>() == correctNode["speaker"].Value<string>());

        DialogueNode node = new DialogueNode(correctNode["text"].Value<string>(), speaker["appearname"].Value<string>(), correctNode["voicefile"].Value<string>(), speaker["picture"].Value<string>());

        //Debug.Log(node.SpeakerImage);
        //Debug.Log(node.SpeakerName);
        //Debug.Log(node.Text);
        //Debug.Log(node.AudioName);

        return node;
    }
}