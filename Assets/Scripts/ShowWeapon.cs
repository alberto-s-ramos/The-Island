using UnityEngine;

public class ShowWeapon : MonoBehaviour {

	[SerializeField]
	public GameObject Spear;
    public GameObject Sword;

    public bool showSpear;
    public bool showSword;

    public GameObject canvas;

	public string current_weapon = "Hand";

	// Use this for initialization
	void Start () {
		showSpear = false;
        showSword = false;

    }

    // Update is called once per frame
    void Update () {
	
        if (showSpear == true && showSword==false) {
			Spear.SetActive (true);
            Sword.SetActive(false);
            current_weapon = "Spear";
		}
        if (showSword == false && showSpear==false) {
            Sword.SetActive(false);
            Spear.SetActive(false);

            current_weapon = "Hand";
        }
        else if (showSword == true && showSpear==false) {
            Sword.SetActive(true);
            Spear.SetActive(false);
            current_weapon = "Sword";
        }



		if (Input.GetKeyDown (KeyCode.Alpha1)) {
            if (canvas.GetComponent<InventoryUI_Weapons>().slots[0].item.name.Equals("Spear"))
            {
                if(showSpear==true){
                    showSpear = false;
                    showSword = false;
                }
                else if(showSpear==false){
                    showSpear = true;
                    showSword = false;

                }

            }
        }
		else if (Input.GetKeyDown (KeyCode.Alpha2)) {
            if(canvas.GetComponent<InventoryUI_Weapons>().slots[1].item.name.Equals("Spear"))
            {
                if (showSpear == true)
                {
                    showSpear = false;
                    showSword = false;
                }
                else if (showSpear == false)
                {
                    showSpear = true;
                    showSword = false;

                }
            }

        }
		else if (Input.GetKeyDown (KeyCode.Alpha3)) {
            if (canvas.GetComponent<InventoryUI_Weapons>().slots[2].item.name.Equals("Spear"))
            {
                if (showSpear == true)
                {
                    showSpear = false;
                    showSword = false;
                }
                else if (showSpear == false)
                {
                    showSpear = true;
                    showSword = false;

                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (canvas.GetComponent<InventoryUI_Weapons>().slots[0].item.name.Equals("Sword"))
            {
                if (showSword == true)
                {
                    showSword = false;
                    showSpear = false;

                }
                else if (showSword == false)
                {
                    showSword = true;
                    showSpear = false;

                }

            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (canvas.GetComponent<InventoryUI_Weapons>().slots[1].item.name.Equals("Sword") )
            {
                if (showSword == true)
                {
                    showSword = false;
                    showSpear = false;

                }
                else if (showSword == false)
                {
                    showSword = true;
                    showSpear = false;

                }
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (canvas.GetComponent<InventoryUI_Weapons>().slots[2].item.name.Equals("Sword") )
            {
                if (showSword == true)
                {
                    showSword = false;
                    showSpear = false;

                }
                else if (showSword == false)
                {
                    showSword = true;
                    showSpear = false;

                }
            }
        }
        else if (Input.GetKeyDown (KeyCode.Q)) {
			showSpear = false;
            showSword = false;
		}
	}


}
