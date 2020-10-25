using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class FinalScript : MonoBehaviour
{

    public Image fadeImage;
    public Animator fadeAnim;
    public void Start()
    {

    }
    public void BackToMainMenu()
    {

        StartCoroutine(Fading());
    }

    IEnumerator Fading()
    {
        Debug.Log("FADING");
        fadeAnim.SetBool("Fade", true);
        yield return new WaitUntil(() => fadeImage.color.a == 1);
        Debug.Log("Changing scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -2);
    }
}
