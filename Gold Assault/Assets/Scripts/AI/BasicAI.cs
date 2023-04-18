using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAI : MonoBehaviour, INoiseAlert, IDamagable
{
    [Header("Player and AI corilation")] // ! https://youtu.be/UjkSFoLxesw

    private NavMeshAgent agent;

    private Transform playerTarg;

    // private int currentState = 0;

    public float health = 100f;

    public AIType currentAIType;
    public State currentState;

    [Range(-1, 1)] private float mood = 0f; // this is a percentage; -100 to 100 percent

    [SerializeField] private float MaxLookRange = 10f;
    [SerializeField] private float MinLookRange = 3f;

    private bool lastCheckSeenPlayer;

    [SerializeField, Range(0, 1)] private float fieldOfVeiwBetweenOneAndZero = 0.5f;

    [Space, Header("AI roming")] // space here so I can look at inspector better and understant what is for what.

    [SerializeField] private float wanderingTime = 3f;
    [SerializeField] private float wanderRadius = 10f;


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
        surrender
    }

    public enum AIType
    {
        hostile,
        civi
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTarg = GameObject.Find("Player").transform;

        //! chamge to random
        currentAIType = AIType.hostile;

        currentState = State.alive;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(playerTarg);

        if (health <= 0)
        {

        }

        LookAtPlayerWithLineOfSight();

        timer += Time.deltaTime;

        if (timer >= wanderingTime && !Alerted)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
        else
        {
            if (agent.isStopped || Vector3.Distance(targetVector, transform.position) < minDistanceToInvestigate)
            {
                Alerted = false;
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

                agent.angularSpeed = 120;
            }
        }

        if (lastCheckSeenPlayer) transform.LookAt(playerTarg); // ! REMOVE SOON, this what?
    }

    public void NoiseMade(Vector3 positionOfNoise)
    {
        if (Vector3.Distance(transform.position, positionOfNoise) <= maxHearingRange)
        {
            Alerted = true;
            targetVector = positionOfNoise;
            agent.destination = positionOfNoise;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
