using UnityEngine;

public class ChopTrees : MonoBehaviour {

	private GameObject character;
	private int healthPoints;
	private bool canImageShowUp = true;
	public Texture2D cursor;
	public Item item;
    public GameObject minimap;
	private float tree_life;
	private float log_hit;
	private float log_hit_temp;
    //private AudioSource audio;

	public void Start(){
		character = GameObject.Find("Character");
		log_hit = Random.Range (2, 5);
		log_hit_temp = log_hit;
		tree_life = 3 * log_hit;
       //audio = GetComponent<AudioSource>();
	}

	public void OnMouseEnter()
	{
		float dist = Vector3.Distance(new Vector3(character.transform.position.x, this.transform.position.y, character.transform.position.z), this.transform.position);
		if (dist < 5 )
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
            if (Input.GetMouseButtonDown(0) && !character.GetComponent<ShowWeapon>().current_weapon.Equals("Hand"))
            {
                character.GetComponent<Animator>().Play("Great Sword Attack");
               // audio.Play();
                log_hit_temp--;
                tree_life--;
            }
            if (log_hit_temp == 0)
            {
                Instantiate(item.game_object, new Vector3(Random.Range(character.transform.position.x - 2, character.transform.position.x + 2),
                    character.transform.position.y + 1, Random.Range(character.transform.position.z - 2, character.transform.position.z + 2)),
                    character.transform.rotation);
                log_hit_temp = log_hit;
            }
            if (tree_life == 0)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                Destroy(transform.parent.gameObject);
                Destroy(this.gameObject);
            }
		}

			//			canImageShowUp
			//			character.GetComponent<PlayerVitals>().eat(healthPoints);
			//			Wait((float)0.3);
			//			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
			//			Destroy(this.gameObject);
	}
}
