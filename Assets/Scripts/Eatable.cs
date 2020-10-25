using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Eatable : MonoBehaviour {

    private GameObject character;
    private int healthPoints;
    private bool canImageShowUp = true;
    public Texture2D cursor;
    private Animator anim;
    private Animation pick;

    // Use this for initialization
    void Start () {
        character = GameObject.Find("Character");
        healthPoints = Random.Range(3, 6);
        anim = character.GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update () {

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
    public void OnMouseUp()
    {
        float dist = Vector3.Distance(new Vector3(character.transform.position.x, this.transform.position.y, character.transform.position.z), this.transform.position);
        if (dist < 2)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            anim.Play("Character|PICK");
            character.GetComponent<PlayerVitals>().eat(healthPoints);
            Destroy(this.gameObject);
        }
    }


    public void Wait(float seconds)
    {
        StartCoroutine(_wait(seconds));
    }
    IEnumerator _wait(float time)
    {
        yield return new WaitForSecondsRealtime(time);
    }

}
