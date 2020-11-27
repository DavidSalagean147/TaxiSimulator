using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI;

public class AISpawner : MonoBehaviour
{
    public enum AIType { Man, Car, Client };
    public AIType aiType;

    public List<Transform> waypoints = new List<Transform>();

    public GameObject[] AI;
    private int numberOfAI;
    public int ratio;

    private Rigidbody Player;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        numberOfAI = transform.childCount - 1;
        foreach (Transform child in transform)
        {
            waypoints.Add(child.GetComponent<Transform>());
        }
    }

    private void Start()
    {
        if (ratio > 1)
        {
            ratio = Random.Range(ratio - 1, ratio + 2);
        }
        StartCoroutine(SpawnAI());
    }

    IEnumerator SpawnAI()
    {
        int count = 2;
        if (aiType != AIType.Client)
        {
            while (count < numberOfAI)
            {
                if (count % ratio == 0 && waypoints[count].tag != "notSpawnable")
                {
                    int randomAI = Random.Range(0, AI.Length);
                    GameObject obj = Instantiate(AI[randomAI]);
                    //obj.GetComponent<NavMeshAgent>().enabled = false;
                    //int randomWaypoint = Random.Range(0, transform.childCount);
                    //Transform child = transform.GetChild(randomWaypoint);
                    if (aiType == AIType.Man)
                    {
                        obj.GetComponent<ManAISimple>().currentWaypoint = count;
                        obj.transform.position = obj.GetComponent<ManAISimple>().waypoints[obj.GetComponent<ManAISimple>().currentWaypoint].transform.position;
                        obj.name = "AIMan";
                    }
                    else if (aiType == AIType.Car)
                    {
                        obj.GetComponent<CarAIMovement>().currentWaypoint = count;
                        obj.transform.position = obj.GetComponent<CarAIMovement>().waypoints[obj.GetComponent<CarAIMovement>().currentWaypoint].transform.position;
                        obj.name = "AICar";
                    }

                    obj.transform.SetParent(null);
                    /*obj.transform.position = child.position;
                    obj.GetComponent<NavMeshAgent>().enabled = true;*/

                    yield return new WaitForEndOfFrame();

                    count++;
                }
                else
                {
                    count++;
                }
            }

            for (int i = 0; i < AI.Length; i++)
            {
                Destroy(AI[i]);
            }
        }
        else
        {
            for (int i = 0; i < waypoints.Count; i++)
            {
                waypoints[i].gameObject.SetActive(false);
            }
            while (count < numberOfAI)
            {
                if (count % ratio == 0 && Vector3.Distance(waypoints[count].position, Player.position) > 30f)
                {
                    int randomAI = Random.Range(0, AI.Length);
                    GameObject obj = Instantiate(AI[randomAI]);

                    obj.transform.position = waypoints[count].position;
                    obj.name = "AIClient";
                    obj.tag = "client";
                    obj.transform.SetParent(null);

                    yield return new WaitForEndOfFrame();

                    count++;
                }
                else
                {
                    count++;
                }
            }
        }
    }

    public void Spawn()
    {
        StartCoroutine(SpawnAI());
    }
}
