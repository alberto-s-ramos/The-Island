using UnityEngine;

public class WeaponPickUp : MonoBehaviour {

	private GameObject character;
	private bool canImageShowUp = true;
	public Texture2D cursor;
	public Item item;

    public GameObject gameManager;

	public void Start(){
		character = GameObject.Find("Character");
	}

	public void OnMouseEnter()
	{
		float dist = Vector3.Distance(new Vector3(character.transform.position.x, this.transform.position.y, character.transform.position.z), this.transform.position);
		if (dist < 2 && canImageShowUp)
		{
			Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
		}
	}

	public void OnMouseExit()
	{
		float dist = Vector3.Distance(new Vector3(character.transform.position.x, this.transform.position.y, character.transform.position.z), this.transform.position);
		if (dist < 2)
		{
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		}
	}



	//CLICK
	public void OnMouseOver()
	{
		float dist = Vector3.Distance(new Vector3(character.transform.position.x, this.transform.position.y, character.transform.position.z), this.transform.position);
		if (dist < 2)
		{
			Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
			if (Input.GetMouseButtonUp (1)) {
				bool WasPickedUp = Inventory_Weapons.instance.Add (item);
				if (WasPickedUp) {
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    Destroy(this.gameObject);
                     if (item.name.Equals("Spear"))
                    {
                        gameManager.GetComponent<WarningMessage>().displaySpearWarning();
                    }
                }
			}
		}
	}
}
