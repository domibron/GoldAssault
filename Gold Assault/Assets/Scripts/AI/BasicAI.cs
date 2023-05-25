using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAI : MonoBehaviour, INoiseAlert, IStunable
{
    [Header("Player and AI corilation")] // ! https://youtu.be/UjkSFoLxesw

    private NavMeshAgent agent;

    [SerializeField] private float angularSpeed = 120;

    private Transform playerTarg;

    // private int currentState = 0;

    public float health = 100f;
    private float maxHealth = 100f;

    public AIType currentAIType;
    public State currentState;
    private State previousState;

    [Range(-1, 1)] private float mood = 0f; // this is a percentage; -100 to 100 percent

    [SerializeField] private float MaxLookRange = 10f;
    [SerializeField] private float MinLookRange = 3f;

    private bool lastCheckSeenPlayer;

    [SerializeField, Range(0, 1)] private float fieldOfVeiwBetweenOneAndZero = 0.5f;

    [Space, Header("AI roming")] // space here so I can look at inspector better and understant what is for what.

    [SerializeField] private float wanderingTime = 6f;
    [SerializeField] private float wanderRadius = 6f;


    private Transform targetLocation;
    private float timer = 0;

    [Space, Header("AI Heard player")]

    [SerializeField] private float maxHearingRange = 20f;
    [SerializeField] private float minDistanceToInvestigate = 5f;

    private bool Alerted = false;

    private Vector3 targetVector;

    public enum State
    {
        dead,
        alive,
        alerted,
        patrolling,
        scared,
        paniced,
        raged,
        surrender,
        engaging,
        stuned,
        NOAI
    }

    public enum AIType
    {
        hostile,
        civi
    }

    [Space, Header("AI engagement")]

    private float timeTilNextShot = 1f;
    private float waitingTime = 0f;
    private float stunTime = 0f;
    private float currentTime = 0f;

    private AudioSource audioSource;
    public AudioClip audioClip;

    public GameObject bulletLine;

    private AdvanceHealthSystemAI healthSystem;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTarg = GameObject.Find("Player").transform;
        audioSource = GetComponent<AudioSource>();

        //! chamge to random
        currentAIType = AIType.hostile;

        healthSystem = GetComponent<AdvanceHealthSystemAI>();

        if (currentState != State.NOAI)
        {
            currentState = State.alive;
        }
        else
        {
            healthSystem.Immortal = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(playerTarg);
        if (currentState == State.NOAI)
        {
            return;
        }

        if (health <= 0)
        {
            currentState = State.dead;

            // for now I will remove the game object. ragdolls
            Destroy(gameObject);

            return;
        }

        // TODO change to the tried and trusted one
        currentTime += Time.deltaTime;


        if (currentTime < stunTime)
        {
            print("I am stuned");
        }


        LookAtPlayerWithLineOfSight();

        if (lastCheckSeenPlayer) // some are temp.
        {
            transform.LookAt(playerTarg);
            shoot();
        }

        timer += Time.deltaTime;

        if ((timer >= wanderingTime || Vector3.Distance(transform.position, agent.pathEndPosition) < 2f) && currentState != State.alerted || !agent.hasPath)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            NavMeshPath path = new NavMeshPath();

            if (agent.CalculatePath(newPos, path) && path.status == NavMeshPathStatus.PathComplete)
            {
                agent.SetDestination(newPos);
                timer = 0;

            }
            else
            {
                // retry path
            }
        }
        else
        {
            if (agent.isStopped || Vector3.Distance(targetVector, transform.position) < minDistanceToInvestigate)
            {
                currentState = State.alive;
                // Alerted = false;
            }
        }
    }

    private Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }

    private void LookAtPlayerWithLineOfSight()
    {
        Vector3 dir = playerTarg.position - transform.position;



        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit))
        {
            Debug.DrawRay(transform.position, dir, Color.blue);


            if (hit.distance > MaxLookRange)
            {
                lastCheckSeenPlayer = false;
                return;
            }

            if (Vector3.Dot(transform.forward, dir) < fieldOfVeiwBetweenOneAndZero && hit.distance > MinLookRange)
            {
                lastCheckSeenPlayer = false;
                return;
            }


            if (hit.collider.transform.tag == "Player")
            {
                lastCheckSeenPlayer = true;
                // transform.LookAt(playerTarg);

                // disable rotatemove.
                agent.angularSpeed = 0;

                // if civi nothing else shoot at player.
            }
            else
            {
                lastCheckSeenPlayer = false;

                agent.angularSpeed = angularSpeed;
            }
        }

        // if (lastCheckSeenPlayer) transform.LookAt(playerTarg); // ! remove this and ctreate proper logic for the AI, this is not good, and should not be here
    }

    private void shoot()
    {
        // some wiereererd stuf. https://forum.unity.com/threads/raycast-layermask-parameter.944194/#post-6161542.
        int layer = 9;
        layer = 1 << layer; // makes the layer 9 to be hit.
                            // layer = (1 << layer) | (1 << 1);
        layer = ~layer; // inverts so that the body can be hit.

        // shoot at the player.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, float.MaxValue, layer))
        {
            if (currentTime >= waitingTime)
            {
                GameObject _go = Instantiate(bulletLine, Vector3.zero, Quaternion.identity);
                _go.GetComponent<BulletSmoke>().CreateLine(agent.transform.position, hit.point, 1f);

                waitingTime = currentTime + timeTilNextShot;
                // print(hit.transform.name);
                hit.collider.GetComponent<IDamagable>()?.TakeDamage(5f);
                audioSource.clip = audioClip;
                audioSource.Play();
            }
        }
    }

    public void NoiseMade(Vector3 positionOfNoise)
    {
        if (currentState == State.NOAI) return;

        if (Vector3.Distance(transform.position, positionOfNoise) <= maxHearingRange)
        {
            // Alerted = true;
            currentState = State.alerted;
            targetVector = positionOfNoise;
            agent.destination = positionOfNoise;
        }
    }

    void OnDestroy()
    {

        PlayerRefernceItems.current.AINoiseAlertSubs.Remove(gameObject);

    }

    public void GetStunned(float stunTime)
    {
        previousState = currentState;
        currentState = State.stuned;

        stunTime = currentTime + stunTime;
    }
}
