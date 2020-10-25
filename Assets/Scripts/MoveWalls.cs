using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class MoveWalls : MonoBehaviour
{
    public GameObject Walls;
    public GameObject character;

    public Image fadeImage;
    public Animator anim;

    // Start is called before the first frame update
    void Update()
    {
     
    }

    // Update is called once per frame

    public void OnMouseOver()
    {
        float dist = Vector3.Distance(new Vector3(character.transform.position.x, this.transform.position.y, character.transform.position.z), this.transform.position);
        if (dist < 2)
        {
            if (Input.GetMouseButtonUp(1))
            {
                character.GetComponent<PlayerController>().fade();
                 Walls.GetComponent<Animator>().Play("WallDown");
                 //Walls.GetComponent<AudioSource>().Play();

            }
        }
    }






}
