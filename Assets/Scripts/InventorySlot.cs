using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

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
		if (!item.game_object.tag.Equals ("Weapon"))
			useButton.interactable = true;
		removeButton.interactable = true;
	}

	public void ClearSlot(){
		item = null;

		icon.sprite = null;
		icon.enabled = false;

		useButton.interactable = false;
		removeButton.interactable = false;
	}

	public void OnRemoveButton()
    {
           if (item.game_object.tag.Equals("CUBE1") || item.game_object.tag.Equals("CUBE2") || item.game_object.tag.Equals("CUBE3"))
        {
            Instantiate(item.game_object, character.transform.position + (character.transform.forward * 2), transform.rotation);
            item.game_object.GetComponent<AudioSource>().Play();

        }
        else if (item.game_object.tag.Equals("Weapon"))
        {
            if (item.game_object.name.Equals("Spear")){
                character.GetComponent<ShowWeapon>().showSpear = false;
                Instantiate(item.game_object, new Vector3(Random.Range(character.transform.position.x - 2, character.transform.position.x + 2),
                character.transform.position.y + 2, Random.Range(character.transform.position.z - 2, character.transform.position.z + 2)),
                Random.rotation);

            }
            else if (item.game_object.name.Equals("Sword")){
                character.GetComponent<ShowWeapon>().showSword = false;
                Instantiate(item.game_object, new Vector3(Random.Range(character.transform.position.x - 2, character.transform.position.x + 2),
                character.transform.position.y + 2, Random.Range(character.transform.position.z - 2, character.transform.position.z + 2)),
                Random.rotation);

            }
            Inventory_Weapons.instance.Remove(item);

        }
        else
            Instantiate(item.game_object, new Vector3(Random.Range(character.transform.position.x - 2, character.transform.position.x + 2),
                character.transform.position.y + 1, Random.Range(character.transform.position.z - 2, character.transform.position.z + 2)),
                character.transform.rotation);

        Inventory.instance.Remove(item);

    }

    public void UseItem(){
        if (item != null)
        {
            if(item.game_object.tag.Equals("Tree")){
                bool WasPickedUp = Crafting_Inventory.instance.Add(item);
                if (WasPickedUp)
                    Inventory.instance.Remove(item);

            }
            else if (item.game_object.tag.Equals("Food")){
                int healthPoints = Random.Range(3, 6);
                character.GetComponent<PlayerVitals>().eat(healthPoints);
                Inventory.instance.Remove(item);
            }
            else if (item.game_object.tag.Equals("WaterBottle"))
            {
                int healthPoints = Random.Range(3, 6);
                character.GetComponent<PlayerVitals>().drink(healthPoints);
                Inventory.instance.Remove(item);
            }
        }
    }

   
}
