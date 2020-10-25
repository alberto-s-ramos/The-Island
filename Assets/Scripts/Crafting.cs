using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
	public Transform itemsParent;

	Crafting_Inventory crafting_inventory;

	public CraftingSlot[] slots;

	Crafting_Result_Slot crafting_result_slot;
	public Item fireplace;

	// Use this for initialization
	public void Start () {
		crafting_inventory = Crafting_Inventory.instance;

		slots = itemsParent.GetComponentsInChildren<CraftingSlot> ();
		crafting_result_slot = itemsParent.GetComponentInChildren<Crafting_Result_Slot>();
	}

   

    public void CraftTool(){
		if (crafting_result_slot.item == null ) {
			int fireplace_counter = 0;

			for (int i = 0; i < slots.Length; i++) {
				if (slots [i].item.game_object.tag.Equals ("Tree"))
					fireplace_counter++;
			}
			if (fireplace_counter == 3) {
				crafting_result_slot.item = fireplace;
				Crafting_Inventory.instance.Remove (crafting_inventory.items [2]);
				Crafting_Inventory.instance.Remove (crafting_inventory.items [1]);
				Crafting_Inventory.instance.Remove (crafting_inventory.items [0]);
				slots [2].ClearSlot ();
				slots [1].ClearSlot ();
				slots [0].ClearSlot ();
			}
			fireplace_counter = 0;
		}
	}
}
