using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Advanced_Agent_Movement : MonoBehaviour
{

    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    bool InCoRoutine = false;
    private UnityEngine.AI.NavMeshPath path;
    private Vector3 target;
    private int point = 0;

    private GameObject character;
    public Texture2D cursor;
    public Item item;

    private int patrol_speed;
    private int pursue_speed;
    private List<Transform> patrol_points = new List<Transform>();

    private bool chase = false;
    private bool wander = false;

    private bool trafic = false;
    private Vector3 my_past_position;
    private int same_pos = 0;

    private FieldOfViewAttack fow;

    private GameObject[] points_array;
    private bool saw_player = false;

    private Animator anim;

    private bool firstAnimation = true;

    private bool attackAnimationFinished = true;
    private bool runAnimationFinished = true;
    private bool walkAnimationFinished = true;
    private bool deathAnimationFinished = false;
    private bool characterAttackAnimationFinished = true;

    private int Health;
    private int attackDamage;

    // Use this for initialization
    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.speed = patrol_speed;
        path = new UnityEngine.AI.NavMeshPath();
        fow = gameObject.GetComponent<FieldOfViewAttack>();

        character = GameObject.Find("Character");
        patrol_speed = 3;
        pursue_speed = 4;
        anim = GetComponent<Animator>();

        attackDamage = Random.Range(20, 30);
        Health = 100;

        resetPathPoints();
        Shuffle();
        Patrol();

    }

    void Update()
    {
        if (Health <= 0)
        {
            if (firstAnimation)
            {
                navMeshAgent.isStopped = true;
                anim.Play("Death 3");
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


            if (trafic && my_past_position == this.transform.position)
            {
                if (same_pos == 5)
                {
                    saw_player = false;
                    navMeshAgent.speed = patrol_speed;
                    point++;

                    if (point == patrol_points.Count)
                    {
                        GameObject[] points_array_temp = GameObject.FindGameObjectsWithTag("Point");
                        resetPathPoints();
                        Shuffle();
                        point = 0;
                        Patrol();
                    }

                    same_pos = 0;
                }
                else
                    same_pos++;
            }

            //        //Verifica se chegou ao ponto para onde se está a dirigir durante a sua patrulha
            //          if ((Mathf.Abs(this.transform.position.x - patrol_points[point].position.x) < 1) && Mathf.Abs(this.transform.position.z - patrol_points[point].position.z) < 1)
            //          {
            //              navMeshAgent.speed = patrol_speed;
            //              point++;
            //              if (point == patrol_points.Count)
            //                  {
            //                  GameObject[] points_array_temp = GameObject.FindGameObjectsWithTag("Point");
            //                  resetPathPoints();
            //                  Shuffle();
            //                  point = 0;
            //              }
            //          }


            //Quando vê o jogador
            else if (chase)
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
                    Vector3 direction = character.transform.position - this.transform.position;

                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.5f);
                    if (direction.magnitude > 5)
                    {
                        this.transform.Translate(0, 0, 0.05f);
                        navMeshAgent.speed = pursue_speed;
                    }
                    navMeshAgent.SetDestination(character.transform.position);
                    saw_player = true;
                    //          }
                }
            }
            //Patrulha
            else if (wander)
            {
                //Verifica se chegou ao ponto para onde se está a dirigir durante a sua patrulha
                if ((Mathf.Abs(this.transform.position.x - patrol_points[point].position.x) < 1) && Mathf.Abs(this.transform.position.z - patrol_points[point].position.z) < 1)
                {
                    navMeshAgent.speed = patrol_speed;
                    point++;
                    if (point == patrol_points.Count)
                    {
                        GameObject[] points_array_temp = GameObject.FindGameObjectsWithTag("Point");
                        resetPathPoints();
                        Shuffle();
                        point = 0;
                    }
                }
                saw_player = false;
                navMeshAgent.speed = patrol_speed;
                Patrol();

            }

            my_past_position = this.transform.position;
        }
    }


    IEnumerator AnimAttackTime()
    {
        attackAnimationFinished = false;
        yield return new WaitForSeconds(1.5f);
        attackAnimationFinished = true;
    }

    IEnumerator AnimDeathTime()
    {
        yield return new WaitForSeconds(4f);
        anim.speed = 0;
        Instantiate(item.game_object, new Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z + 1), transform.rotation);
        yield return new WaitForSeconds(2f);
        deathAnimationFinished = true;
        firstAnimation = true;
    }


    void Patrol()
    {
        RaycastHit hit;
        Vector3 direction = patrol_points[point].position - this.transform.position;
        if (Physics.Raycast(this.transform.position, direction, out hit))
        {
            navMeshAgent.SetDestination(patrol_points[point].position);
        }
    }

    void Shuffle()
    {
        for (int i = 0; i < patrol_points.Count; i++)
        {
            Transform temp = patrol_points[i];
            int randomIndex = Random.Range(i, patrol_points.Count);
            patrol_points[i] = patrol_points[randomIndex];
            patrol_points[randomIndex] = temp;
        }
    }
    void resetPathPoints()
    {
        patrol_points.Clear();
        points_array = GameObject.FindGameObjectsWithTag("Point");

        for (int i = 0; i < points_array.Length; i++)
            patrol_points.Add(points_array[i].transform);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Spider1") || collision.gameObject.name.Equals("Spider2"))
        {
            trafic = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name.Equals("Spider1") || collision.gameObject.name.Equals("Spider2"))
        {
            trafic = false;
        }
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


}
