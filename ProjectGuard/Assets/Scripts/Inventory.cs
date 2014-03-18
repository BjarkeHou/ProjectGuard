using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> content;
    public int gold;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PickedUpItem(GameObject item)
    {
        //InventoryItem i = item.GetComponent<Item>().ItemRemove();
        //content.Add(i);
    }
}

public enum InventoryItem {RedPotion, BluePotion, Gem}
