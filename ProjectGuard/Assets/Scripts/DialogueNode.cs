using System.Collections.Generic;
using System.Xml;
using System.Collections;

public class DialogueNode
{
    public DialogueNode(string text, string speakerName, string audioName, string speakerImage)
    {
        SpeakerImage = speakerImage;
        AudioName = audioName;
        SpeakerName = speakerName;
        Text = text;
    }

    public string Text { get; private set; }
    public string SpeakerName { get; private set; }
    public string AudioName { get; private set; }
    public string SpeakerImage { get; private set; }
}
