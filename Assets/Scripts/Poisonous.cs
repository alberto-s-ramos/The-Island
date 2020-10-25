using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Poisonous : MonoBehaviour
{

    private GameObject character;
    private int healthPoints;
    private int eatPoints;
    public Texture2D cursor;
    private bool canImageShowUp = true;

    // Use this for initialization
    void Start()
    {
        character = GameObject.Find("Character");
        healthPoints = Random.Range(-10, -4);
        eatPoints = Random.Range(-6, -3);
    }


    // Update is called once per frame
    void Update()
    {

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
            canImageShowUp = false;
            character.GetComponent<PlayerVitals>().eatPoison(healthPoints, eatPoints);
            Wait((float)0.3);
            Destroy(this.gameObject);
        }
    }





    public void Wait(float seconds)
    {
        StartCoroutine(_wait(seconds));
    }
    IEnumerator _wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

}
