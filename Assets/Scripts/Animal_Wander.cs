using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal_Wander : MonoBehaviour
{

    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    bool InCoRoutine = false;
    public UnityEngine.AI.NavMeshPath path;
    //    public Vector3 target;
    //    public int point = 0;

    private GameObject character;
    public Texture2D cursor;
    private int Health;

    public Item item;

    //  private bool collideWithCharacter = false;

    private float attackDamage;

    private int patrol_speed;
    private int pursue_speed;
    //    public List<Transform> patrol_points = new List<Transform>();



    private Vector3 my_past_position;
    private int same_pos = 0;

    private FieldOfViewAttack fow;

    private GameObject[] points_array;
    private bool saw_player = false;
    private Vector3 player_past_position;

    private float wanderZone = 10f;
    private Vector3 origin;
    private Vector3 targetPatrolPos = Vector3.down;

    private bool chase = false;
    private bool wander = false;

    private bool firstAnimation = true;

    private bool attackAnimationFinished = true;
    private bool runAnimationFinished = true;
    private bool walkAnimationFinished = true;
    private bool deathAnimationFinished = false;

    private bool characterAttackAnimationFinished = true;

    private Animator anim;

    private int traffic = 0;

    private int number_of_meat;

    public void OnDrawGizmosSelected()
    {
        // Draw circle of radius wander zone
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(origin == Vector3.zero ? transform.position : origin, wanderZone);
    }


    // Use this for initialization
    void Start()
    {

        origin = transform.position;
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.speed = patrol_speed;
        path = new UnityEngine.AI.NavMeshPath();
        fow = gameObject.GetComponent<FieldOfViewAttack>();
        character = GameObject.Find("Character");

        targetPatrolPos = transform.position;

        anim = GetComponent<Animator>();

        if (this.name.Split(char.Parse(" "))[0].Equals("Gorilla"))
        {
            attackDamage = Random.Range(15, 25);
            Health = 150;
            patrol_speed = 2;
            pursue_speed = 5;
            number_of_meat = 2;
        }
        else if (this.name.Split(char.Parse(" "))[0].Equals("Wolf"))
        {
            attackDamage = Random.Range(5, 10);
            Health = 100;
            patrol_speed = 2;
            pursue_speed = 5;
            number_of_meat = 1;
        }
        else if (this.name.Split(char.Parse(" "))[0].Equals("Bear"))
        {
            attackDamage = Random.Range(10, 20);
            Health = 200;
            patrol_speed = 1;
            pursue_speed = 3;
            number_of_meat = 3;
        }

        saw_player = false;
        navMeshAgent.speed = patrol_speed;
        Wander();
    }

    void Update()
    {
        if (Health <= 0)
        {
            if (firstAnimation)
            {
                navMeshAgent.isStopped = true;
                anim.Play("Death");
                StartCoroutine(AnimDeathTime());
                firstAnimation = false;

            }
            if (deathAnimationFinished)
                Destroy(this.gameObject);
        }
    }

    void LateUpdate()
    {
        if (attackAnimationFinished && Health > 0)
        {
            if (fow.visibleTargets.Count == 1 && character.GetComponent<PlayerVitals>().isDead == false)
            {
                chase = true;
                wander = false;
                //          character.transform = fow.visibleTargets [0];
            }
            else
            {
                chase = false;
                wander = true;
            }

            if (my_past_position == transform.position)
            {
                traffic++;
                if (traffic == 5)
                {
                    Wander();
                    traffic = 0;
                }
            }

            //Quando vê o jogador
            if (chase)
            {
                if ((Vector3.Distance(this.transform.position, character.transform.position) <= 2f))
                {
                    //          if (collideWithCharacter && attackAnimationFinished ) {
                    navMeshAgent.isStopped = true;
                    anim.Play("Attack");
                    character.GetComponent<PlayerVitals>().healthSlider.value -= attackDamage;
                    StartCoroutine(AnimAttackTime());
                }
                else
                {
                    navMeshAgent.isStopped = false;
                    if (runAnimationFinished)
                    {
                        anim.Play("Run");
                        StartCoroutine(AnimRunTime());
                    }
                    Vector3 direction = character.transform.position - this.transform.position;

                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.2f);
                    if (direction.magnitude > 5)
                    {
                        this.transform.Translate(0, 0, 0.05f);
                        navMeshAgent.speed = pursue_speed;
                    }

                    //          StartCoroutine (AnimationTime(5f, "Run"));
                    navMeshAgent.SetDestination(character.transform.position);
                    saw_player = true;
                    player_past_position = character.transform.position;

                }
            }


            //        //Verifica se chegou ao ponto para onde se está a dirigir durante a sua patrulha
            else if (wander)
            {
                if (Vector3.Distance(this.transform.position, targetPatrolPos) < 3)
                {
                    //          if (this.name.Split (char.Parse (" ")) [0].Equals ("Gorilla") && firstAnimation) {
                    ////                this.GetComponent<Animator> ().Play ("ChestHit");
                    //              StartCoroutine (AnimationTime (3.2f));
                    //              firstAnimation = false;
                    //          }

                    saw_player = false;
                    navMeshAgent.speed = patrol_speed;
                    Wander();
                }
                if (walkAnimationFinished)
                {
                    anim.Play("Walk");
                    StartCoroutine(AnimWalkTime());
                }
            }
            //Patrulha
            //      else if (wander)
            //      {
            //          Debug.Log ("ola");
            //          saw_player = false;
            //          navMeshAgent.speed = patrol_speed;
            //          Wander();
            //
            //      }




            my_past_position = this.transform.position;
        }
    }

    //  IEnumerator AnimationTime(float seconds)
    //  {
    //      Debug.Log ("start chest hit  " + Time.time);
    //      animationFinished = false;
    ////        this.GetComponent<Animator> ().Play (animacao);
    //      yield return new WaitForSeconds (seconds);
    //      animationFinished = true;
    //      Debug.Log ("done chest hit  " + Time.time);
    //  }

    IEnumerator AnimAttackTime()
    {
        attackAnimationFinished = false;
        yield return new WaitForSeconds(1.5f);
        attackAnimationFinished = true;
    }

    void Wander()
    {

        Vector3 randomPoint = origin + Random.insideUnitSphere * wanderZone;
        targetPatrolPos = new Vector3(randomPoint.x, transform.position.y, randomPoint.z);
        navMeshAgent.SetDestination(targetPatrolPos);

    }

    IEnumerator AnimRunTime()
    {
        runAnimationFinished = false;
        yield return new WaitForSeconds(0.55f);
        runAnimationFinished = true;
    }

    IEnumerator AnimWalkTime()
    {
        walkAnimationFinished = false;
        yield return new WaitForSeconds(0.4f);
        walkAnimationFinished = true;
    }

    IEnumerator AnimDeathTime()
    {
        yield return new WaitForSeconds(2f);
        anim.speed = 0;
        for (int i = 0; i < number_of_meat; i++)
            Instantiate(item.game_object, new Vector3(Random.Range(transform.position.x - 1, transform.position.x + 1), transform.position.y + 1, Random.Range(transform.position.z - 1, transform.position.z + 1)), transform.rotation);
        yield return new WaitForSeconds(2f);
        deathAnimationFinished = true;
        firstAnimation = true;
    }

    public void OnMouseEnter()
    {

        float dist = Vector3.Distance(new Vector3(character.transform.position.x, character.transform.position.y, character.transform.position.z), this.transform.position);
        if (dist < 3)
        {
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }
    }

    public void OnMouseExit()
    {
        float dist = Vector3.Distance(new Vector3(character.transform.position.x, character.transform.position.y, character.transform.position.z), this.transform.position);
        if (dist < 3)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }



    //CLICK
    public void OnMouseOver()
    {

        float dist = Vector3.Distance(new Vector3(character.transform.position.x, character.transform.position.y, character.transform.position.z), this.transform.position);
        if (dist < 3)
        {
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
            if (Input.GetMouseButtonUp(0) && character.GetComponent<PlayerVitals>().isDead == false && characterAttackAnimationFinished)
            {
                if (character.GetComponent<ShowWeapon>().current_weapon.Equals("Hand"))
                    Health -= 8;
                else if (character.GetComponent<ShowWeapon>().current_weapon.Equals("Spear"))
                    Health -= Random.Range(20, 30);
                else if (character.GetComponent<ShowWeapon>().current_weapon.Equals("Sword"))
                    Health -= Random.Range(30, 40);
                character.GetComponent<Animator>().Play("Great Sword Attack");
                StartCoroutine(CharacterAttackAnimationTime());
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
        }
    }

    IEnumerator CharacterAttackAnimationTime()
    {
        characterAttackAnimationFinished = false;
        yield return new WaitForSeconds(0.5f);
        characterAttackAnimationFinished = true;
    }

    //  void OnCollisionEnter(Collision col){
    //      if (col.gameObject.name.Equals ("Character")) {
    //          collideWithCharacter = true;
    //          Debug.Log ("ataque");
    //      }
    //  }
    //
    //  void OnCollisionExit(Collision col){
    //      if (col.gameObject.name.Equals ("Character")) {
    //          collideWithCharacter = false;
    //          Debug.Log ("nao ataque");
    //      }
    //  }

}


