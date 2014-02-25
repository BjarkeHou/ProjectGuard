using UnityEngine;
using System.Collections;

public class PlayerPickup : MonoBehaviour {

    public Inventory player;

	// Use this for initialization
	void Start ()
	{
	}

    void OnTriggerEnter(Collider other)
    {

        //check if it is an item
        if (other.gameObject.tag != "PickupItem")
        {
            return;
        }

        player.PickedUpItem(other.gameObject);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
