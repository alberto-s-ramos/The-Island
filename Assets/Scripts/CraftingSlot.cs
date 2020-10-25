using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour
{
	private GameObject game_Object;
	private GameObject character;
	public Image icon;
	public Button removeButton;
	public Button useButton;

	public Item item;

	public void Start(){
		character = GameObject.Find("Character");
	}

	public void AddItem(Item newItem){
		item = newItem;
		game_Object = item.game_object;
		icon.sprite = item.icon;
		icon.enabled = true;
		useButton.interactable = true;
		removeButton.interactable = true;
	}

	public void ClearSlot(){
		item = null;
		game_Object = null;
		icon.sprite = null;
		icon.enabled = false;

		useButton.interactable = false;
		removeButton.interactable = false;

	}

	public void OnRemoveButton(){
		Instantiate (item.game_object,new Vector3(Random.Range(character.transform.position.x-2,character.transform.position.x+2),
			character.transform.position.y+1,Random.Range(character.transform.position.z -2,character.transform.position.z + 2)),
			character.transform.rotation);
		Crafting_Inventory.instance.Remove (item);
	}

	public void UseItem(){
		if (item != null) {
			bool WasPickedUp = Inventory.instance.Add(item);
			if(WasPickedUp)
				Crafting_Inventory.instance.Remove (item);
		}
	}
}
