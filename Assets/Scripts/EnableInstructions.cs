using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableInstructions : MonoBehaviour
{
    public GameObject img;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("h")){
            if(img.active==true){
                img.SetActive(false);
            }
            else if (img.active == false)
            {
                img.SetActive(true);
            }
        }
    }
}
