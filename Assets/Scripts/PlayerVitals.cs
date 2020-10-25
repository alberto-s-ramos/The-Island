using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerVitals : MonoBehaviour {

    public Slider healthSlider;
    public int maxHealth;
    public int healthFallRate;


    public Slider thirstSlider;
    public int maxThirst;
    public int thirstFallRate;

    public Slider hungerSlider;
    public int maxHunger;
    public int hungerFallRate;

    public bool isDead = false;

    public GameObject reset;

    void Start()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;

        thirstSlider.maxValue = maxThirst;
        thirstSlider.value = maxThirst;

        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = maxHunger;
    }

    // Update is called once per frame
    void Update () {
        if(isDead==false){
            //HEALTH CONTROLLER
            if(hungerSlider.value<=0 && thirstSlider.value<=0)
            {
                healthSlider.value -= Time.deltaTime / healthFallRate * 2;
            }
            else if (hungerSlider.value <= 0 || thirstSlider.value <= 0)
            {
                healthSlider.value -= Time.deltaTime / healthFallRate ;
            }
            if(healthSlider.value<=0){
                CharacterDeath();
            }
            
            //THIRST CONTROLLER
            if (thirstSlider.value >= 0)
            {
                thirstSlider.value -= Time.deltaTime / thirstFallRate;
            }
            else if (thirstSlider.value <= 0)
            {
                thirstSlider.value = 0;
            }
            else if (thirstSlider.value >= maxThirst)
            {
                thirstSlider.value = maxThirst;
            }
            
            
            //HUNGER CONTROLLER
            if (hungerSlider.value>=0){
                hungerSlider.value -= Time.deltaTime / hungerFallRate;
            }
            else if(hungerSlider.value<=0){
                hungerSlider.value = 0;
            }
            else if(hungerSlider.value>= maxHunger){
                hungerSlider.value = maxHunger;
            }

        }

   


    }

    public void CharacterDeath(){
        this.GetComponent<Animator>().Play("Dying");
        reset.SetActive(true);
        isDead = true;

    }

    public void eat(int value){

        if(hungerSlider.value<maxHunger){
            hungerSlider.value += value;
        }
        if(healthSlider.value<maxHealth){
            healthSlider.value += value / 2;
        }
    }

    public void removeHP(float value)
    {
        if (healthSlider.value > 0)
        {
            healthSlider.value += value / 2;
        }
    }
    public void drink(int value){
        if(thirstSlider.value<maxThirst){
            thirstSlider.value += value;
        }
    }
    public void eatPoison(int valueHealth, int valueEat)
    {

        if (hungerSlider.value < maxHunger)
        {
            hungerSlider.value += valueEat;
        }
        if(healthSlider.value>0){
            healthSlider.value += valueHealth / 2;
        }

    }

    public bool isPlayerDead(){
        return isDead;
    }
    

}
