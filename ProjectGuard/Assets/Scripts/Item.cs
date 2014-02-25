using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public InventoryItem type;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public InventoryItem ItemRemove()
    {
        UnityEditor.
        Destroy(this, 0.5f);
        return type;
    }
}
