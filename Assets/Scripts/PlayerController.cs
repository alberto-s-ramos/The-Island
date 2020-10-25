using System.Collections; using System.Collections.Generic; using UnityEngine; using UnityEngine.UI; using UnityEngine.SceneManagement;    public class PlayerController : MonoBehaviour {       public Transform playerCam, character, centerPoint;       private float mouseX, mouseY;     public float mouseSensivity = 10.0f;     public float mouseYPosition = 1.0f;      private float moveFB, moveLR;     public float moveSpeed ;      private float zoom;     public float zoomSpeed = 2f;      public float zoomMin = -2f;     public float zoomMax = -7f;      public float rotationSpeed = 5f;         private bool isShiftKeyDown;      private Animator anim;     private float verticalVelocity;     public float gravity ;     public float jumpForce ;    

    private CharacterController cc;
    private bool grounded=true;      private int updateNum = 0;
    private bool blockEverything = true;

    public Image fadeImage;     public Animator fadeAnim;


    void Start(){         zoom = -6;         anim = character.GetComponent<Animator>();
        cc = character.GetComponent<CharacterController>();         anim.Play("Standing Up");
     }

    void FixedUpdate(){         if(updateNum==0){             StartCoroutine(WaitForStartAnim());
        }
             //---------ZOOM---------             zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;              if (zoom > zoomMin)                 zoom = zoomMin;              if (zoom < zoomMax)                 zoom = zoomMax;              playerCam.transform.localPosition = new Vector3(0, 0, zoom);             //-----------             if (Input.GetMouseButton(1))             {                 mouseX += Input.GetAxis("Mouse X");                 mouseY -= Input.GetAxis("Mouse Y");             }             mouseY = Mathf.Clamp(mouseY, -10f, 60f);             playerCam.LookAt(centerPoint);             centerPoint.localRotation = Quaternion.Euler(mouseY, mouseX, 0);              centerPoint.position = new Vector3(character.position.x, character.position.y + mouseYPosition + 0.75f, character.position.z);              if (this.GetComponent<PlayerVitals>().isPlayerDead() == false)             {                 grounded = cc.isGrounded;                  if (grounded)                 {                     if (Input.GetKeyDown(KeyCode.Space))                     {                         // anim.Play("Character|JUMP");                         verticalVelocity = jumpForce;                     }                 }                 else                 {                     verticalVelocity -= 1f * gravity * Time.deltaTime;                 }
            if (!blockEverything)             {                  Vector3 moveVector = new Vector3(0, verticalVelocity, 0);                 cc.Move(moveVector * Time.deltaTime);                 verticalVelocity -= 0.3f * gravity * Time.deltaTime;                  moveFB = Input.GetAxis("Vertical") * moveSpeed;                 moveLR = Input.GetAxis("Horizontal") * moveSpeed;
                               MoveCharacter(moveLR, moveFB);                  if (Input.GetAxis("Horizontal") > 0 | Input.GetAxis("Horizontal") < 0 |                     Input.GetAxis("Vertical") > 0 | Input.GetAxis("Vertical") < 0)                 {                     Quaternion turnAngle = Quaternion.Euler(0, centerPoint.eulerAngles.y, 0);                     character.rotation = Quaternion.Slerp(character.rotation, turnAngle, Time.deltaTime * rotationSpeed);                 }             }         }
       
      
     }      private void MoveCharacter(float x, float y){               anim.SetFloat("Vel_X", x);         anim.SetFloat("Vel_Y", y);
             Vector3 movement = new Vector3(moveLR, 0, moveFB);         movement = character.rotation * movement;
        character.GetComponent<CharacterController>().Move(movement * Time.deltaTime);

    }

    IEnumerator WaitForStartAnim()     {         yield return new WaitForSeconds(9);         updateNum++;         blockEverything = false;     }      public void fade(){         StartCoroutine(Fading());     }      IEnumerator Fading()     {         Debug.Log("FADING");         fadeAnim.Play("FadeOut");         yield return new WaitUntil(()=>fadeImage.color.a==1);         Debug.Log("Changing scene");         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);     }  }