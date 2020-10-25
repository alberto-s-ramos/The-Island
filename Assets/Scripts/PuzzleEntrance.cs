using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleEntrance : MonoBehaviour
{

    public GameObject miniMap;
    private void OnTriggerEnter(Collider other)
    {
        if (miniMap.active) {
            miniMap.SetActive(false);
        }
        else if (!miniMap.active)
        {
            miniMap.SetActive(true);
        }
    }
}
