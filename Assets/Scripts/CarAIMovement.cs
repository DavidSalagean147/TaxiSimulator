using System.Collections.Generic;
using UnityEngine;

public class CarAIMovement : MonoBehaviour
{
    [HideInInspector]
    public List<Transform> waypoints = new List<Transform>();

    public GameObject toTakeWPs;

    [HideInInspector]
    public int currentWaypoint;

    private int motorTorque;
    private int brakeTorque = 300;
    private float maxSteerAngle = 40f;
    private float maxSpeed;
    private float maxSpeedCopy;

    private RaycastHit hit;

    private Rigidbody Player;
    private Rigidbody rb;

    private WheelCollider[] colliders;
    public GameObject[] wheels;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        foreach (Transform child in toTakeWPs.transform)
        {
            waypoints.Add(child.GetComponent<Transform>());
        }
        colliders = GetComponentsInChildren<WheelCollider>();
        motorTorque = Random.Range(40, 50);
        maxSpeed = Random.Range(4, 6);
        maxSpeedCopy = maxSpeed;
    }

    private void Start()
    {
        Vector3 direction = waypoints[currentWaypoint + 1].position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }

    void FixedUpdate()
    {
        //if (Vector3.Distance(Player.transform.position, transform.position) < 150)
        {
            if (Vector3.Distance(waypoints[currentWaypoint].transform.position, transform.position) <= 1f)
            {
                currentWaypoint++;
                if (waypoints.Count <= currentWaypoint)
                {
                    currentWaypoint = 1;
                }
            }

            Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit);
            if (hit.distance > 3f)
            {
                Vector3 relativeVector = transform.InverseTransformPoint(waypoints[currentWaypoint].position);
                float steerAngle = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;

                colliders[0].steerAngle = steerAngle;
                colliders[1].steerAngle = steerAngle;

                if (rb.velocity.magnitude < maxSpeed && hit.distance > 4f)
                {
                    colliders[2].motorTorque = motorTorque;
                    colliders[3].motorTorque = motorTorque;
                    colliders[2].brakeTorque = 0;
                    colliders[3].brakeTorque = 0;
                }
                else
                {
                    colliders[2].brakeTorque = 0;
                    colliders[3].brakeTorque = 0;
                    colliders[2].motorTorque = 0;
                    colliders[3].motorTorque = 0;
                }
            }
            else
            {
                colliders[2].motorTorque = 0;
                colliders[3].motorTorque = 0;
                colliders[2].brakeTorque = brakeTorque;
                colliders[3].brakeTorque = brakeTorque;
            }
        
            if (transform.position.y < -200f || transform.position.y > 200f)
            {
                if (Vector3.Distance(new Vector3(transform.position.x, Player.transform.position.y, transform.position.z), Player.transform.position) > 20f)
                {
                    transform.position = new Vector3(transform.position.x, waypoints[currentWaypoint].position.y + 0.1f, transform.position.z);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }
        /*else
        {
            colliders[2].motorTorque = 0;
            colliders[3].motorTorque = 0;
            colliders[2].brakeTorque = brakeTorque;
            colliders[3].brakeTorque = brakeTorque;
        }*/
        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 15)
        {
            maxSpeed = (float)(maxSpeedCopy / 1.5);
        }
        else
        {
            maxSpeed = maxSpeedCopy;
        }
    }

    private void Update()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) < 50)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].GetWorldPose(out Vector3 pos, out Quaternion rot);
                wheels[i].transform.position = pos;
                wheels[i].transform.rotation = rot;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            /*if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 5)
            {
                currentWaypoint++;
                if (waypoints.Count <= currentWaypoint)
                {
                    currentWaypoint = 1;
                }
            }*/
            colliders[2].motorTorque = 0;
            colliders[3].motorTorque = 0;
            colliders[2].brakeTorque = brakeTorque;
            colliders[3].brakeTorque = brakeTorque;
        }
    }
}