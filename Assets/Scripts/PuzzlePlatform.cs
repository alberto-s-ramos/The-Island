using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePlatform : MonoBehaviour
{
    public GameObject platform;

  
    public void OnCollisionExit(Collision collision)
    {
        if (this.gameObject.name.Equals("CUBE 1 PLATFORM"))
        {
            platform.GetComponent<Puzzle>().removeCube("1");
        }
        else if (this.gameObject.name.Equals("CUBE 2 PLATFORM"))
        {
            platform.GetComponent<Puzzle>().removeCube("2");
        }
        else if (this.gameObject.name.Equals("CUBE 3 PLATFORM"))
        {
            platform.GetComponent<Puzzle>().removeCube("3");
        }
    }
    public void OnCollisionStay(Collision collision)
    {
       // Debug.Log("IM PLATFORM: " + this.gameObject.name + "         COLL: " + collision.gameObject.name);

        if (this.gameObject.name.Equals("CUBE 1 PLATFORM"))
        {
            verifyCube("CUBE 1 PLATFORM", collision.gameObject.tag);
        }
        else if (this.gameObject.name.Equals("CUBE 2 PLATFORM"))
        {
            verifyCube("CUBE 2 PLATFORM", collision.gameObject.tag);
        }
        else if (this.gameObject.name.Equals("CUBE 3 PLATFORM"))
        {
            verifyCube("CUBE 3 PLATFORM", collision.gameObject.tag);
        }
    }
   



    public void verifyCube(string platName, string cubeTag){
        if(platName.Equals("CUBE 1 PLATFORM")&& cubeTag.Equals("CUBE1")){
            //ACIONA CHAMA 1
            platform.GetComponent<Puzzle>().setCube("1");
        }
        else if (platName.Equals("CUBE 2 PLATFORM") && cubeTag.Equals("CUBE2"))
        {
            //ACIONA CHAMA 1
            platform.GetComponent<Puzzle>().setCube("2");

        }
        else if (platName.Equals("CUBE 3 PLATFORM") && cubeTag.Equals("CUBE3"))
        {
            //ACIONA CHAMA 3
            platform.GetComponent<Puzzle>().setCube("3");

        }
    }
}
