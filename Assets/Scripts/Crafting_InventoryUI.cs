using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting_InventoryUI : MonoBehaviour
{
	public Transform itemsParent;

	public GameObject crafting_inventoryUI;

	Crafting_Inventory crafting_inventory;

	public CraftingSlot[] slots;
	public Crafting_Result_Slot crafting_result_slot;

	// Use this for initialization
	void Start () {
		crafting_inventory = Crafting_Inventory.instance;
		crafting_inventory.onItemChangedCallBack += UpdateUI;

		slots = itemsParent.GetComponentsInChildren<CraftingSlot> ();
		crafting_result_slot = itemsParent.GetComponentInChildren<Crafting_Result_Slot> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab)) {
			crafting_inventoryUI.SetActive (!crafting_inventoryUI.activeSelf);
		}
	}

	void UpdateUI () {
		for (int i = 0; i < slots.Length; i++) {
			if (i < crafting_inventory.items.Count)
				slots [i].AddItem (crafting_inventory.items [i]);
			else
				slots [i].ClearSlot ();
		}


	}
}
