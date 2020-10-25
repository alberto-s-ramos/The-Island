using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEA : MonoBehaviour
{
    public GameObject seaWarning;
    public GameObject character;
    // Start is called before the first frame update
    void Start()
    {


    }

    private void OnTriggerExit(Collider other)
    {
        seaWarning.SetActive(false);

    }

    private void OnTriggerStay(Collider other)
    {
        seaWarning.SetActive(true);
        character.GetComponent<PlayerVitals>().removeHP(-0.5f);
    }
}
