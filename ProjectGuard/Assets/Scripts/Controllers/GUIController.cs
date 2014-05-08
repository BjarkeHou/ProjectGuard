using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour
{

	public bool showDisarmTrapPrompt = false;
	
	public bool showDialogPrompt = false;
	public string dialogPromptText = "";
	
	public bool showDialog;
	public string dialogText = "";
	public string dialogButtonText = "";
	
	public DialogController dialogCtrl;
	
	public void DialogButtonClicked()
	{
		if (dialogCtrl == null)
			return;
			
		dialogCtrl.dialogButtonClicked();
	}
}
