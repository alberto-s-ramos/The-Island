using UnityEngine;
using UnityEngine.UI;

public class Crafting_Result_Slot : MonoBehaviour
{
	private GameObject game_Object;
	private GameObject character;
	public Image icon;
	public Button useButton;

	public Item item;

	public void Start(){
		character = GameObject.Find("Character");
	}

	public void Update(){
		
		if (item != null) {
			game_Object = item.game_object;
			icon.sprite = item.icon;
			icon.enabled = true;
			useButton.interactable = true;
		}
	}
		

	public void ClearSlot(){
		item = null;

		icon.sprite = null;
		icon.enabled = false;

		useButton.interactable = false;
	}
		

	public void UseItem(){
		if (item != null) {
			if(item.game_object.name.Equals("Fireplace_02"))
                Instantiate (item.game_object,new Vector3(character.transform.position.x,character.transform.position.y+1,character.transform.position.z)+(transform.forward*2),character.transform.rotation);
			ClearSlot ();
		}
	}
}
