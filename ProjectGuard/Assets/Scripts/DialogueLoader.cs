using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
		private JObject json;

		void Awake ()
		{
				TextAsset ta = (TextAsset)Resources.Load ("Dialogs/dialogue", typeof(TextAsset));
				string all = ta.text;
				json = (JObject)JToken.Parse (all);
		}

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
				//Debug.Log(GetDialogueNode("BrotherAhead").First().Text);

		}
	
		// Update is called once per frame
		void Update ()
		{
	    
		}

		public DialogueNode[] GetDialogueNode (string dialogueId)
		{
				if (json != null) {
						JArray relevantDialogue = (JArray)json ["dialogs"] [dialogueId];
						JArray speakers = (JArray)json ["speakers"];

						List<DialogueNode> dialogue = new List<DialogueNode> ();

						foreach (JToken line in relevantDialogue) {
								JToken speaker = speakers.First (e => e ["name"].Value<string> () == line ["speaker"].Value<string> ());

								DialogueNode node = new DialogueNode (line ["text"].Value<string> (), speaker ["appearname"].Value<string> (), line ["voicefile"].Value<string> (), speaker ["picture"].Value<string> ());

								dialogue.Add (node);
						}

						return dialogue.ToArray ();
				}

				throw new NullReferenceException ("json not loaded in GetDialogueNode");
		}

		public bool DialogueNodeExists (string dialogueId)
		{
				if (json != null) {
						return json ["dialogs"] [dialogueId].Any ();
				}


				throw new NullReferenceException ("json not loaded in DialogueNodeExists");
		}
}