using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{

    public GameObject platform;
    public GameObject character;
    public Texture2D cursor;

    private bool canUse=false;


    private Animator anim;

    public GameObject theOtherLever;

    public void Start()
    {
        anim = GetComponent<Animator>();
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
            if (Input.GetMouseButtonUp(0))
            {
                character.GetComponent<Animator>().Play("Button Pushing");
                theOtherLever.SetActive(true);
                this.gameObject.SetActive(false);
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                platform.GetComponent<Puzzle>().movePlatform();

            }
        }
    }



}