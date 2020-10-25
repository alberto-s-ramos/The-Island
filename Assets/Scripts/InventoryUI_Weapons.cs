using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI_Weapons : MonoBehaviour {

	public Transform itemsParent;

	public GameObject inventoryUI_Weapons;

	Inventory_Weapons inventory_Weapons;

	public InventorySlot[] slots;

	// Use this for initialization
	void Start () {
		inventory_Weapons = Inventory_Weapons.instance;
		inventory_Weapons.onItemChangedCallBack += UpdateUI;

		slots = itemsParent.GetComponentsInChildren<InventorySlot> ();
	}


	void UpdateUI () {
		for (int i = 0; i < slots.Length; i++) {
			if (i < inventory_Weapons.items.Count)
				slots [i].AddItem (inventory_Weapons.items [i]);
			else
				slots [i].ClearSlot ();
		}
			

	}
}
