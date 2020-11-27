using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ManAISimple : MonoBehaviour
{
    [HideInInspector]
    public List<Transform> waypoints = new List<Transform>();

    public GameObject toTakeWPs;

    [HideInInspector]
    public int currentWaypoint;

    private int direction;
    //public float moveSpeed = 0.4f;
    //public float rotSpeed = 3f;

    //private RaycastHit hit;

    private Animator anim;
    private Rigidbody rb;
    private bool isDead = false;

    private Rigidbody Player;
    private NavMeshAgent nav;

    //private GameObject[] avoidingPoints;
    //private int indexWhereToGoWhenAvoid;
    //private bool isAvoiding = false;
    //private bool calculatedTheNearestAvoidingPoint = false;
    //float minDist = 999f;
    //private int indexOfTheNearestAvoidingPointFromTheNextWaypoint;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        //avoidingPoints = GameObject.FindGameObjectsWithTag("avoidingPoint");
        foreach (Transform child in toTakeWPs.transform)
        {
            waypoints.Add(child.GetComponent<Transform>());
        }
        nav = GetComponent<NavMeshAgent>();
        direction = Random.Range(0, 2);
    }
    private void Start()
    {
        nav.speed = Random.Range(0.4f, 0.5f);
        nav.enabled = false;

        if (direction == 0)
        {
            if (currentWaypoint >= waypoints.Count)
            {
                Vector3 direction = waypoints[1].position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = rotation;
            }
            else
            {
                Vector3 direction = waypoints[currentWaypoint + 1].position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = rotation;
            }
        }
        else
        {
            if (currentWaypoint <= 1)
            {
                Vector3 direction = waypoints[waypoints.Count - 1].position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = rotation;
            }
            else
            {
                Vector3 direction = waypoints[currentWaypoint - 1].position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = rotation;
            }
        }
        nav.enabled = true;
    }
    void FixedUpdate()
    {
        if (isDead == false)
        { 
            nav.enabled = true;
            anim.enabled = true;
            rb.isKinematic = false;
            if (isDead == false && transform.position.y < 200 && transform.position.y > -200)
            {
                if (Vector3.Distance(waypoints[currentWaypoint].position, transform.position) <= 0.3f)
                {
                    if (direction == 0)
                    {
                        currentWaypoint++;
                        if (currentWaypoint >= waypoints.Count)
                        {
                            currentWaypoint = 0;
                        }
                    }
                    else
                    {
                        currentWaypoint--;
                        if (currentWaypoint <= -1)
                        {
                            currentWaypoint = waypoints.Count - 1;
                        }
                    }
                }
                if (Vector3.Distance(Player.transform.position, waypoints[currentWaypoint].position) < 1 && Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 2)
                {
                    if (direction == 0)
                    {
                        currentWaypoint++;
                        if (currentWaypoint >= waypoints.Count)
                        {
                            currentWaypoint = 1;
                        }
                    }
                    else
                    {
                        currentWaypoint--;
                        if (currentWaypoint <= 0)
                        {
                            currentWaypoint = waypoints.Count - 1;
                        }
                    }
                }
                if (nav.enabled == true)
                {
                    nav.SetDestination(waypoints[currentWaypoint].position);
                }
                /*Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit);
                if (hit.distance > 0.5f && isAvoiding == false)
                {
                    //umbla normal
                    Vector3 direction = waypoints[currentWaypoint].position - transform.position;
                    Quaternion rotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotSpeed);
                    transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, Time.deltaTime * moveSpeed);
                    calculatedTheNearestAvoidingPoint = false;
                    //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                }
                else
                {
                    //ocoleste
                    if (calculatedTheNearestAvoidingPoint == false)
                    {
                        minDist = 999f;
                        for (int i = 0; i < avoidingPoints.Length; i++)
                        {
                            if (Vector3.Distance(transform.position, avoidingPoints[i].transform.position) < minDist)
                            {
                                minDist = Vector3.Distance(transform.position, avoidingPoints[i].transform.position);
                                indexWhereToGoWhenAvoid = i;
                            }
                        }
                        calculatedTheNearestAvoidingPoint = true;
                        isAvoiding = true;
                    }
                    else
                    {
                        for (int i = 0; i < avoidingPoints.Length; i++)
                        {
                            avoidingPoints[i].transform.position = new Vector3(avoidingPoints[i].transform.position.x, 0.15f, avoidingPoints[i].transform.position.z);
                        }

                        minDist = 999f;
                        for (int i = 0; i < avoidingPoints.Length; i++)
                        {
                            if (Vector3.Distance(waypoints[currentWaypoint].position, avoidingPoints[i].transform.position) < minDist)
                            {
                                minDist = Vector3.Distance(waypoints[currentWaypoint].position, avoidingPoints[i].transform.position);
                                indexOfTheNearestAvoidingPointFromTheNextWaypoint = i;
                            }
                        }

                        if (isAvoiding == true)
                        {
                            if (Vector3.Distance(avoidingPoints[indexWhereToGoWhenAvoid].transform.position, transform.position) <= 0.05f)
                            {
                                indexWhereToGoWhenAvoid++;
                                if (avoidingPoints.Length <= indexWhereToGoWhenAvoid)
                                {
                                    indexWhereToGoWhenAvoid = 0;
                                }
                            }

                            Vector3 dir = avoidingPoints[indexWhereToGoWhenAvoid].transform.position - transform.position;
                            Quaternion rot = Quaternion.LookRotation(dir);
                            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * rotSpeed);
                            transform.position = Vector3.MoveTowards(transform.position, avoidingPoints[indexWhereToGoWhenAvoid].transform.position, Time.deltaTime * moveSpeed);

                            if (Vector3.Distance(avoidingPoints[indexOfTheNearestAvoidingPointFromTheNextWaypoint].transform.position, transform.position) <= 0.05f || Vector3.Distance(transform.position, Player.transform.position) > 3)
                            { 
                                if (Vector3.Distance(Player.transform.position, waypoints[currentWaypoint].position) > 2f)
                                {
                                    isAvoiding = false;
                                }
                                else
                                {
                                    currentWaypoint++;
                                    isAvoiding = false;
                                }
                            }
                        }
                    }
                }
            */
            }
        }
        else
        {
            if (Vector3.Distance(waypoints[currentWaypoint].position, Player.transform.position) > 20f && Vector3.Distance(transform.position, Player.transform.position) > 20f)
            {
                transform.position = new Vector3(transform.position.x, waypoints[currentWaypoint].position.y + 0.1f, transform.position.z);
                //transform.position = waypoints[1].position;
                //currentWaypoint = 1;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                //rb.isKinematic = true;
                isDead = false;
                anim.enabled = true;
                //isAvoiding = false;
                nav.enabled = true;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            //rb.isKinematic = false;
            isDead = true;
            anim.enabled = false;
            //isAvoiding = false;
            nav.enabled = false;
        }
    }
}
