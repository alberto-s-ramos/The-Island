using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningMessage : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject minimapWarning;
    public GameObject spearWarning;

    void Start()
    {
        
    }

    public void displayWarning(){
        StartCoroutine(MapTimer());
    }

    public void displaySpearWarning()
    {
        StartCoroutine(SpearTimer());
    }

    IEnumerator MapTimer()
    {
        minimapWarning.SetActive(true);
        yield return new WaitForSeconds(3);
        minimapWarning.SetActive(false);

    }

    IEnumerator SpearTimer()
    {
        spearWarning.SetActive(true);
        yield return new WaitForSeconds(3);
        spearWarning.SetActive(false);

    }
}
