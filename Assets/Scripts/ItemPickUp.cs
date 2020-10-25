using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUp : MonoBehaviour {

	private GameObject character;
	private int healthPoints;
	private bool canImageShowUp = true;
	public Texture2D cursor;
	public Item item;
    public GameObject minimap;
    private GameObject platform;
    private AudioSource audio;

    public GameObject gameManager;

    public void Start(){
		character = GameObject.Find("Character");
        platform = GameObject.Find("PLATFORM");

        healthPoints = Random.Range(10, 15);
        if (this.tag.Equals ("Water")){
            Physics.IgnoreCollision (GetComponent<Collider>(), character.GetComponent<Collider>());
            audio = GetComponent<AudioSource>();
        }
     
	}
 
    public void OnMouseEnter()
	{

        float dist = Vector3.Distance(new Vector3(character.transform.position.x, this.transform.position.y, character.transform.position.z), this.transform.position);
		if (dist < 5 && canImageShowUp)
		{
			Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
		}
	}

	public void OnMouseExit()
	{
		float dist = Vector3.Distance(new Vector3(character.transform.position.x, this.transform.position.y, character.transform.position.z), this.transform.position);
		if (dist < 5)
		{
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		}
	}



	//CLICK
	public void OnMouseOver()
	{

		float dist = Vector3.Distance(new Vector3(character.transform.position.x, this.transform.position.y, character.transform.position.z), this.transform.position);
		if (dist < 5)
		{
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
            if (Input.GetMouseButtonUp(0)){
                if (!this.tag.Equals("Food") && !this.tag.Equals("Water") && !this.tag.Equals("Map") && !this.tag.Equals("WaterBottle"))
                {
                    return;
                }
                else if (this.tag.Equals("Food"))
                {

                    character.GetComponent<Animator>().Play("Picking Up");
                    character.GetComponent<PlayerVitals>().eat(healthPoints);
                    Destroy(this.gameObject);
                }

                else if (this.tag.Equals("Water"))
                {
                    character.GetComponent<PlayerVitals>().drink(Random.Range(5, 7));
                    audio.Play();
                }
                else if (this.tag.Equals("WaterBottle"))
                {
                    character.GetComponent<PlayerVitals>().drink(Random.Range(5, 7));
                    Destroy(this.gameObject);
                }
                else if (this.tag.Equals("Map"))
                {
                    character.GetComponent<Animator>().Play("Button Pushing");

                    minimap.SetActive(true);
                    Destroy(this.gameObject);
                    gameManager.GetComponent<WarningMessage>().displayWarning();
                }
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

			}
			if(Input.GetMouseButtonUp(1)){
                if(item.game_object.tag.Equals("Food") || item.game_object.tag.Equals("Water") || item.game_object.tag.Equals("Tree") || item.game_object.tag.Equals("WaterBottle") ||
                   item.game_object.tag.Equals("CUBE1")|| item.game_object.tag.Equals("CUBE2")|| item.game_object.tag.Equals("CUBE3"))
                {
                    bool WasPickedUp = Inventory.instance.Add (item);
                    if (WasPickedUp && !this.tag.Equals("Water")) {
                        character.GetComponent<Animator>().Play("Picking Up");
                        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                        Destroy(this.gameObject);
                    }
                    if(WasPickedUp &&  item.game_object.tag.Equals("CUBE1")){
                        character.GetComponent<Animator>().Play("Picking Up");
                        platform.GetComponent<Puzzle>().removeCube("1");
                    }
                    else if (WasPickedUp && item.game_object.tag.Equals("CUBE2"))
                    {
                        character.GetComponent<Animator>().Play("Picking Up");
                        platform.GetComponent<Puzzle>().removeCube("2");
                    }
                    else if (WasPickedUp && item.game_object.tag.Equals("CUBE3"))
                    {
                        character.GetComponent<Animator>().Play("Picking Up");
                        platform.GetComponent<Puzzle>().removeCube("3");
                    }

                }

			}

//			canImageShowUp
//			character.GetComponent<PlayerVitals>().eat(healthPoints);
//			Wait((float)0.3);
//			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
//			Destroy(this.gameObject);
		}
	}

  

}
