 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{

    public GameObject FireCube1;
    public GameObject FireCube2;
    public GameObject FireCube3;


    public bool cube1Placed=false;
    public bool cube2Placed=false;
    public bool cube3Placed=false;
    public bool canRepeat = true;
    private Animator anim;

    private bool platUp = false;
    private bool platDown = true;

    private float currentPos;
    private AudioSource audio;
    private AudioClip audioclip;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setCube(string number){
        if(number.Equals("1")){
            cube1Placed = true;
            FireCube1.SetActive(true);
        }
        else if (number.Equals("2"))
        {
            cube2Placed = true;
            FireCube2.SetActive(true);
        }
        else if (number.Equals("3"))
        {
            cube3Placed = true;
            FireCube3.SetActive(true);
        }
    }

    public void removeCube(string number)
    {
        if (number.Equals("1"))
        {
            cube1Placed = false;
            FireCube1.SetActive(false);

        }
        else if (number.Equals("2"))
        {
            cube2Placed = false;
            FireCube2.SetActive(false);

        }
        else if (number.Equals("3"))
        {
            cube3Placed = false;
            FireCube3.SetActive(false);

        }
    }

    public void movePlatform(){
        if (cube1Placed && cube2Placed && cube3Placed)
        {
            if (platDown && !platUp &&this.transform.position.y<=5242.4){
                platDown = false;
                platUp = true;
                anim.Play("PlatformUp");
                audio.Play();
            }
            else if (!platDown && platUp && this.transform.position.y >= 5248.800) {
                platDown = true;
                platUp = false;
                anim.Play("PlatformDown");
                audio.Play();

            }
            //canRepeat = false;
        }
    }
}
