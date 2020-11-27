using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
public class ManAIClient : MonoBehaviour
{
    public GameObject waypoint1;
    public GameObject arrivingArea1;
    public GameObject waypoint2;
    public GameObject arrivingArea2;
    private Collider[] colliders;

    private bool isInFirstWaypoint = true;
    public float moveSpeed = 0.4f;
    public float rotSpeed = 3f;

    public int rotationWP01;
    public int rotationWP02;

    private Animator anim;
    private Rigidbody rb;
    private bool isDead = false;

    private Rigidbody Player;
    private GameObject arrowForFirstPerson;
    private GameObject arrowForThirdPerson;
    public GameObject carArea;

    private Animation openFRDoorAnim;
    private Animation openBRDoorAnim;
    private Animation openBLDoorAnim;
    private bool doorPlayedAnim = false;
    private float FRDist;
    private float BRDist;
    private float BLDist;

    private bool stayInCar = false;

    private bool collidersAreEnabled = true;

    private bool canGoWithTheTaxi = true;
    private float timeToCanGoWithTheTaxi = 0f;

    private GameObject whereToEnterInCarFR;
    private GameObject whereToEnterInCarBR;
    private GameObject whereToEnterInCarBL;
    private bool FRNearest = false;
    private bool BRNearest = false;
    private bool BLNearest = false;
    private bool calculatedTheNearestDoor = false;

    public GameObject timePanel;
    public Text timeText;
    private float time;
    private float theTime = 20f;
    private bool isOutOfTime = false;

    public Button accelerationButton;
    public Button brakeButton;

    private new Renderer renderer;

    private Car car;

    private void Awake()
    {
        car = FindObjectOfType(typeof(Car)) as Car;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        openFRDoorAnim = GameObject.FindGameObjectWithTag("openFRDoorAnim").GetComponent<Animation>();
        openBRDoorAnim = GameObject.FindGameObjectWithTag("openBRDoorAnim").GetComponent<Animation>();
        openBLDoorAnim = GameObject.FindGameObjectWithTag("openBLDoorAnim").GetComponent<Animation>();
        rb = GetComponent<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        whereToEnterInCarFR = GameObject.FindGameObjectWithTag("whereToEnterInCarFR");
        whereToEnterInCarBR = GameObject.FindGameObjectWithTag("whereToEnterInCarBR");
        whereToEnterInCarBL = GameObject.FindGameObjectWithTag("whereToEnterInCarBL");
        arrowForFirstPerson = GameObject.FindGameObjectWithTag("arrow1st");
        arrowForThirdPerson = GameObject.FindGameObjectWithTag("arrow3rd");
        timeText.text = $"TIME: {(int)time / 60}:{time % 60:00}";
        renderer = GetComponentInChildren<Renderer>();
    }
    private void Start()
    {
        arrowForFirstPerson.SetActive(false);
        arrowForThirdPerson.SetActive(false);
        arrivingArea1.SetActive(false);
        arrivingArea2.SetActive(false);
        carArea.SetActive(false);
        timePanel.SetActive(false);
    }
    void FixedUpdate()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) < 60 && isDead == false)
        {
            if (Vector3.Distance(transform.position, Player.transform.position) > 5)
            {
                Vector3 lookPosition = Player.transform.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(lookPosition, Vector3.up);
                Quaternion lookR = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
                transform.rotation = lookR;
            }

            anim.enabled = true;
            //face cu mana
            if (Car.isBussy == false && canGoWithTheTaxi == true && isDead == false)
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isGettingInRightDoor", false);
                anim.SetBool("isGettingOutRightDoor", false);
                anim.SetBool("isGettingInLeftDoor", false);
                anim.SetBool("isGettingOutLeftDoor", false);
                anim.SetBool("isWaving", true);
                carArea.SetActive(true);
                arrivingArea1.SetActive(false);
                arrivingArea2.SetActive(false);
                renderer.enabled = true;
            }
            else
            {
                if (stayInCar == false && calculatedTheNearestDoor == false && isDead == false)
                {
                    carArea.SetActive(false);
                    arrivingArea1.SetActive(false);
                    arrivingArea2.SetActive(false);
                    renderer.enabled = false;
                    if (collidersAreEnabled)
                    {
                        for (int i = 0; i < colliders.Length; i++)
                        {
                            colliders[i].enabled = false;
                        }
                        collidersAreEnabled = false;
                    }
                }
            }
            //urca in masina
            if (Car.isBussy == false && isDead == false && canGoWithTheTaxi == true && Player.velocity.magnitude < 0.1f && (Vector3.Distance(whereToEnterInCarBL.transform.position, transform.position) < 1.5f || Vector3.Distance(whereToEnterInCarBR.transform.position, transform.position) < 1.5f || Vector3.Distance(whereToEnterInCarFR.transform.position, transform.position) < 1.5f))
            {
                if (calculatedTheNearestDoor == false)
                {
                    FRDist = Vector3.Distance(transform.position, whereToEnterInCarFR.transform.position);
                    BRDist = Vector3.Distance(transform.position, whereToEnterInCarBR.transform.position);
                    BLDist = Vector3.Distance(transform.position, whereToEnterInCarBL.transform.position);
                    calculatedTheNearestDoor = true;
                    if (FRDist < BRDist && FRDist < BLDist)
                    {
                        FRNearest = true;
                        BRNearest = false;
                        BLNearest = false;
                    }
                    else if (BRDist < FRDist && BRDist < BLDist)
                    {
                        FRNearest = false;
                        BRNearest = true;
                        BLNearest = false;
                    }
                    else
                    {
                        FRNearest = false;
                        BRNearest = false;
                        BLNearest = true;
                    }
                }
                if (FRNearest)
                {
                    car.Brake();
                    Accelerate.interactable = false;
                    car.NormalTexture();
                    transform.SetParent(whereToEnterInCarFR.transform);
                    anim.SetBool("isWaving", false);
                    anim.SetBool("isGettingInRightDoor", false);
                    anim.SetBool("isGettingOutRightDoor", false);
                    anim.SetBool("isGettingInLeftDoor", false);
                    anim.SetBool("isGettingOutLeftDoor", false);
                    anim.SetBool("isWalking", true);
                    carArea.SetActive(false);

                    Vector3 dir = whereToEnterInCarFR.transform.position - transform.position;
                    Quaternion rot = Quaternion.LookRotation(dir);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * rotSpeed);
                    transform.position = Vector3.MoveTowards(transform.position, whereToEnterInCarFR.transform.position, Time.deltaTime * moveSpeed);


                    if (Vector3.Distance(transform.position, whereToEnterInCarFR.transform.position) == 0f)
                    {
                        Car.isBussy = true;
                        transform.position = whereToEnterInCarFR.transform.position;
                        transform.rotation = whereToEnterInCarFR.transform.rotation;
                        anim.SetBool("isWaving", false);
                        anim.SetBool("isWalking", false);
                        anim.SetBool("isGettingInRightDoor", true);
                        anim.SetBool("isGettingOutRightDoor", false);
                        anim.SetBool("isGettingInLeftDoor", false);
                        anim.SetBool("isGettingOutLeftDoor", false);
                        openFRDoorAnim.Play();
                    }
                }
                else if (BRNearest)
                {
                    car.Brake();
                    Accelerate.interactable = false;
                    car.NormalTexture();
                    transform.SetParent(whereToEnterInCarBR.transform);
                    anim.SetBool("isWaving", false);
                    anim.SetBool("isGettingInRightDoor", false);
                    anim.SetBool("isGettingOutRightDoor", false);
                    anim.SetBool("isGettingInLeftDoor", false);
                    anim.SetBool("isGettingOutLeftDoor", false);
                    anim.SetBool("isWalking", true);
                    carArea.SetActive(false);

                    Vector3 dir = whereToEnterInCarBR.transform.position - transform.position;
                    Quaternion rot = Quaternion.LookRotation(dir);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * rotSpeed);
                    transform.position = Vector3.MoveTowards(transform.position, whereToEnterInCarBR.transform.position, Time.deltaTime * moveSpeed);


                    if (Vector3.Distance(transform.position, whereToEnterInCarBR.transform.position) == 0f)
                    {
                        Car.isBussy = true;
                        transform.position = whereToEnterInCarBR.transform.position;
                        transform.rotation = whereToEnterInCarBR.transform.rotation;
                        anim.SetBool("isWaving", false);
                        anim.SetBool("isWalking", false);
                        anim.SetBool("isGettingInRightDoor", true);
                        anim.SetBool("isGettingOutRightDoor", false);
                        anim.SetBool("isGettingInLeftDoor", false);
                        anim.SetBool("isGettingOutLeftDoor", false);
                        openBRDoorAnim.Play();
                    }
                }
                else if (BLNearest)
                {
                    car.Brake();
                    Accelerate.interactable = false;
                    car.NormalTexture();
                    transform.SetParent(whereToEnterInCarBL.transform);
                    anim.SetBool("isWaving", false);
                    anim.SetBool("isGettingInRightDoor", false);
                    anim.SetBool("isGettingOutRightDoor", false);
                    anim.SetBool("isGettingInLeftDoor", false);
                    anim.SetBool("isGettingOutLeftDoor", false);
                    anim.SetBool("isWalking", true);
                    carArea.SetActive(false);

                    Vector3 dir = whereToEnterInCarBL.transform.position - transform.position;
                    Quaternion rot = Quaternion.LookRotation(dir);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * rotSpeed);
                    transform.position = Vector3.MoveTowards(transform.position, whereToEnterInCarBL.transform.position, Time.deltaTime * moveSpeed);


                    if (Vector3.Distance(transform.position, whereToEnterInCarBL.transform.position) == 0f)
                    {
                        Car.isBussy = true;
                        transform.position = whereToEnterInCarBL.transform.position;
                        transform.rotation = whereToEnterInCarBL.transform.rotation;
                        anim.SetBool("isWaving", false);
                        anim.SetBool("isWalking", false);
                        anim.SetBool("isGettingInRightDoor", false);
                        anim.SetBool("isGettingOutRightDoor", false);
                        anim.SetBool("isGettingInLeftDoor", true);
                        anim.SetBool("isGettingOutLeftDoor", false);
                        openBLDoorAnim.Play();
                    }
                }
                if (collidersAreEnabled == true)
                {
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        colliders[i].enabled = false;
                    }
                    collidersAreEnabled = false;
                }
            }
            if (stayInCar == true)
            {
                if (FRNearest)
                {
                    transform.position = whereToEnterInCarFR.transform.position;
                    transform.rotation = whereToEnterInCarFR.transform.rotation;
                }
                else if (BRNearest)
                {
                    transform.position = whereToEnterInCarBR.transform.position;
                    transform.rotation = whereToEnterInCarBR.transform.rotation;
                }
                else if (BLNearest)
                {
                    transform.position = whereToEnterInCarBL.transform.position;
                    transform.rotation = whereToEnterInCarBL.transform.rotation;
                }
                if (time >= 0f)
                {
                    time -= Time.deltaTime;
                    timeText.text = $"TIME: {(int)time / 60}:{time % 60:00}";
                }
                if (isInFirstWaypoint)
                {
                    if (CameraController.isOn3rdPersonCamera == false)
                    {
                        arrowForThirdPerson.SetActive(false);
                        arrowForFirstPerson.SetActive(true);
                        Vector3 lookPos = arrivingArea2.transform.position - arrowForFirstPerson.transform.position;
                        Quaternion lookRot = Quaternion.LookRotation(lookPos, Vector3.up);
                        Quaternion rotation = Quaternion.Euler(10, lookRot.eulerAngles.y, 0);
                        arrowForFirstPerson.transform.rotation = rotation;
                    }
                    else
                    {
                        arrowForFirstPerson.SetActive(false);
                        arrowForThirdPerson.SetActive(true);
                        Vector3 lookPos = arrivingArea2.transform.position - arrowForThirdPerson.transform.position;
                        Quaternion lookRot = Quaternion.LookRotation(lookPos, Vector3.up);
                        Quaternion rotation = Quaternion.Euler(20, lookRot.eulerAngles.y, 0);
                        arrowForThirdPerson.transform.rotation = rotation;
                    }
                }
                else
                {
                    if (CameraController.isOn3rdPersonCamera == false)
                    {
                        arrowForThirdPerson.SetActive(false);
                        arrowForFirstPerson.SetActive(true);
                        Vector3 lookPos = arrivingArea1.transform.position - arrowForFirstPerson.transform.position;
                        Quaternion lookRot = Quaternion.LookRotation(lookPos, Vector3.up);
                        Quaternion rotation = Quaternion.Euler(10, lookRot.eulerAngles.y, 0);
                        arrowForFirstPerson.transform.rotation = rotation;
                    }
                    else
                    {
                        arrowForFirstPerson.SetActive(false);
                        arrowForThirdPerson.SetActive(true);
                        Vector3 lookPos = arrivingArea1.transform.position - arrowForThirdPerson.transform.position;
                        Quaternion lookRot = Quaternion.LookRotation(lookPos, Vector3.up);
                        Quaternion rotation = Quaternion.Euler(20, lookRot.eulerAngles.y, 0);
                        arrowForThirdPerson.transform.rotation = rotation;
                    }
                }
            }
            if (isInFirstWaypoint)
            {
                if (Vector3.Distance(Player.transform.position, waypoint2.transform.position) < 1.5f && Player.velocity.magnitude < 0.1f && Car.isBussy == true && stayInCar == true)
                {
                    //ajunge la destinatie
                    car.Brake();
                    Accelerate.interactable = false;
                    car.NormalTexture();
                    isInFirstWaypoint = false;
                    anim.SetBool("isWaving", false);
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isGettingInRightDoor", false);
                    anim.SetBool("isGettingInLeftDoor", false);
                    if (doorPlayedAnim == false)
                    {
                        if (BLNearest)
                        {
                            anim.SetBool("isGettingOutLeftDoor", true);
                            openBLDoorAnim.Play();
                        }
                        else if (BRNearest)
                        {
                            anim.SetBool("isGettingOutRightDoor", true);
                            openBRDoorAnim.Play();
                        }
                        else if (FRNearest)
                        {
                            anim.SetBool("isGettingOutRightDoor", true);
                            openFRDoorAnim.Play();
                        }
                        doorPlayedAnim = true;
                    }
                    timePanel.SetActive(false);
                }
            }
            else
            {
                if (Vector3.Distance(Player.transform.position, waypoint1.transform.position) < 1.5f && Player.velocity.magnitude < 0.1f && Car.isBussy == true && stayInCar == true)
                {
                    //ajunge la destinatie
                    car.Brake();
                    Accelerate.interactable = false;
                    car.NormalTexture();
                    isInFirstWaypoint = true;
                    anim.SetBool("isWaving", false);
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isGettingInRightDoor", false);
                    anim.SetBool("isGettingInLeftDoor", false);
                    if (doorPlayedAnim == false)
                    {
                        if (BLNearest)
                        {
                            anim.SetBool("isGettingOutLeftDoor", true);
                            openBLDoorAnim.Play();
                        }
                        else if (BRNearest)
                        {
                            anim.SetBool("isGettingOutRightDoor", true);
                            openBRDoorAnim.Play();
                        }
                        else if (FRNearest)
                        {
                            anim.SetBool("isGettingOutRightDoor", true);
                            openFRDoorAnim.Play();
                        }
                        doorPlayedAnim = true;
                    }
                    timePanel.SetActive(false);
                }
            }
            if (Vector3.Distance(transform.position, Player.transform.position) > 5f)
            {
                if (collidersAreEnabled == false && stayInCar == false && renderer.enabled == true)
                {
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        colliders[i].enabled = true;
                    }
                    collidersAreEnabled = true;
                }
            }
            //cat timp nu poate merge cu taxi dupa cursa
            if (canGoWithTheTaxi == false)
            {
                timeToCanGoWithTheTaxi += Time.deltaTime;
                if (timeToCanGoWithTheTaxi > 10f)
                {
                    if (Vector3.Distance(transform.position, Player.transform.position) > 20f)
                    {
                        canGoWithTheTaxi = true;
                        timeToCanGoWithTheTaxi = 0f;
                        transform.SetParent(null);
                        renderer.enabled = true;
                    }
                }
            }
            if (time <= 0f)
            {
                isOutOfTime = true;
                if (isOutOfTime)
                {
                    isOutOfTime = false;
                    //scad banii primiti
                }
            }
            if (isDead == true || transform.position.y > 2 || transform.position.y < -2)
            {
                //respawn dupa ce moare
                if (isInFirstWaypoint)
                {
                    if (Vector3.Distance(waypoint1.transform.position, Player.transform.position) > 20f && Vector3.Distance(transform.position, Player.transform.position) > 15f)
                    {
                        transform.position = waypoint1.transform.position;
                        transform.rotation = Quaternion.Euler(0, rotationWP01, 0);
                        rb.isKinematic = true;
                        isDead = false;
                        canGoWithTheTaxi = false;
                        anim.enabled = true;
                        anim.SetBool("isWaving", false);
                        anim.SetBool("isGettingInRightDoor", false);
                        anim.SetBool("isGettingOutRightDoor", false);
                        anim.SetBool("isGettingInLeftDoor", false);
                        anim.SetBool("isGettingOutLeftDoor", false);
                        anim.SetBool("isWalking", true);
                    }
                }
                else
                {
                    if (Vector3.Distance(waypoint2.transform.position, Player.transform.position) > 20f && Vector3.Distance(transform.position, Player.transform.position) > 15f)
                    {
                        transform.position = waypoint2.transform.position;
                        transform.rotation = Quaternion.Euler(0, rotationWP02, 0);
                        rb.isKinematic = true;
                        isDead = false;
                        canGoWithTheTaxi = false;
                        anim.enabled = true;
                        anim.SetBool("isWaving", false);
                        anim.SetBool("isGettingInRightDoor", false);
                        anim.SetBool("isGettingOutRightDoor", false);
                        anim.SetBool("isGettingInLeftDoor", false);
                        anim.SetBool("isGettingOutLeftDoor", false);
                        anim.SetBool("isWalking", true);
                    }
                }
            }
        }
        else
        {
            anim.enabled = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "car" && Player.velocity.magnitude > 0.1f)
        {
            rb.isKinematic = false;
            canGoWithTheTaxi = false;
            isDead = true;
            anim.enabled = false;
            carArea.SetActive(false);
        }
    }
    private void HideTheArea()
    {
        arrivingArea1.SetActive(false);
        arrivingArea2.SetActive(false);
        arrowForFirstPerson.SetActive(false);
        arrowForThirdPerson.SetActive(false);
    }
    public void StayInCarEqualsTrue()
    {
        Accelerate.interactable = true;
        /*if (BLNearest)
        {
            transform.SetParent(whereToEnterInCarBL.transform);
        }
        else if (BRNearest)
        {
            transform.SetParent(whereToEnterInCarBR.transform);
        }
        else if (FRNearest)
        {
            transform.SetParent(whereToEnterInCarFR.transform);
        }*//*
        stayInCar = true;
        carArea.SetActive(false);
        if (isInFirstWaypoint)
        {
            arrivingArea2.SetActive(true);
        }
        else
        {
            arrivingArea1.SetActive(true);
        }
        car.DontBrake();
        time = theTime;
        timePanel.SetActive(true);
        doorPlayedAnim = false;
        car.TaxiDome();
    }
    public void StayInCarEqualsFalse()
    {
        Accelerate.interactable = true;
        BLNearest = false;
        BRNearest = false;
        FRNearest = false;
        transform.SetParent(null);
        stayInCar = false;
        HideTheArea();
        if (isInFirstWaypoint == true)
        {
            transform.SetParent(waypoint1.transform);
            transform.rotation = Quaternion.Euler(0, rotationWP01, 0);
        }
        else
        {
            transform.SetParent(waypoint2.transform);
            transform.rotation = Quaternion.Euler(0, rotationWP02, 0);
        }
        anim.SetBool("isGettingInRightDoor", false);
        anim.SetBool("isGettingOutRightDoor", false);
        anim.SetBool("isGettingInLeftDoor", false);
        anim.SetBool("isGettingOutLeftDoor", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isWaving", true);
        canGoWithTheTaxi = false;
        Car.isBussy = false;
        car.DontBrake();
        calculatedTheNearestDoor = false;
        renderer.enabled = false;
        transform.localPosition = Vector3.zero;
        car.TaxiDome();
    }
}
*/
public class ManAIClient : MonoBehaviour
{
    //[HideInInspector]
    private GameObject arrivingWaypoint;
    public float minDistanceForArrivingWaypoint;
    //private GameObject arrivingArea;

    [HideInInspector]
    public List<Transform> waypoints = new List<Transform>();

    public GameObject toTakeWPs;

    private Collider[] colliders;

    //[HideInInspector]
    public List<GameObject> otherClients = new List<GameObject>();

    //[HideInInspector]
    private Vector3 initialPosition;

    private bool collidersAreEnabled = true;

    private float moveSpeed = 0.4f;
    private float rotSpeed = 3f;

    private Animator anim;
    private Rigidbody rb;
    private bool isDead = false;

    private Rigidbody Player;
    public GameObject arrow;
    public GameObject carArea;

    private Animation openFRDoorAnim;
    private Animation openBRDoorAnim;
    private Animation openBLDoorAnim;
    private bool doorPlayedAnim = false;
    private float FRDist;
    private float BRDist;
    private float BLDist;

    private bool stayInCar = false;

    private GameObject whereToEnterInCarFR;
    private GameObject whereToEnterInCarBR;
    private GameObject whereToEnterInCarBL;
    private bool FRNearest = false;
    private bool BRNearest = false;
    private bool BLNearest = false;
    private bool calculatedTheNearestDoor = false;

    public GameObject timePanel;
    public Text timeText;
    private float time;
    private float theTime = 20f;
    private bool isOutOfTime = false;

    private Car car;
    public AISpawner aiSpawner;

    private void Awake()
    {
        car = FindObjectOfType(typeof(Car)) as Car;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        openFRDoorAnim = GameObject.FindGameObjectWithTag("openFRDoorAnim").GetComponent<Animation>();
        openBRDoorAnim = GameObject.FindGameObjectWithTag("openBRDoorAnim").GetComponent<Animation>();
        openBLDoorAnim = GameObject.FindGameObjectWithTag("openBLDoorAnim").GetComponent<Animation>();
        rb = GetComponent<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        whereToEnterInCarFR = GameObject.FindGameObjectWithTag("whereToEnterInCarFR");
        whereToEnterInCarBR = GameObject.FindGameObjectWithTag("whereToEnterInCarBR");
        whereToEnterInCarBL = GameObject.FindGameObjectWithTag("whereToEnterInCarBL");
        timeText.text = $"TIME: {(int)time / 60}:{time % 60:00}";

        foreach (Transform child in toTakeWPs.transform)
        {
            waypoints.Add(child.GetComponent<Transform>());
        }

        int i = Random.Range(0, waypoints.Count);
        if (Vector3.Distance(waypoints[i].position, transform.position) > minDistanceForArrivingWaypoint)
        {
            arrivingWaypoint = waypoints[i].gameObject;
        }
        else while(Vector3.Distance(waypoints[i].position, transform.position) < minDistanceForArrivingWaypoint)
            {
                i = Random.Range(0, waypoints.Count);
            }
        arrivingWaypoint = waypoints[i].gameObject;
    }
    private void Start()
    {
        //arrivingArea = arrivingWaypoint.transform.GetChild(0).gameObject;
        carArea.SetActive(false);
        timePanel.SetActive(false);
        initialPosition = transform.position;
        arrow.SetActive(false);
        //arrivingArea.SetActive(false);
        StartCoroutine(GetTheOtherClients());
    }
    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < 60 && isDead == false)
        {
            anim.enabled = true;

            if (Vector3.Distance(transform.position, Player.transform.position) > 5)
            {
                Vector3 lookPosition = Player.transform.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(lookPosition, Vector3.up);
                Quaternion lookR = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
                transform.rotation = lookR;
            }

            //face cu mana
            if (Car.isBussy == false && isDead == false)
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isGettingInRightDoor", false);
                anim.SetBool("isGettingOutRightDoor", false);
                anim.SetBool("isGettingInLeftDoor", false);
                anim.SetBool("isGettingOutLeftDoor", false);
                anim.SetBool("isWaving", true);
                carArea.SetActive(true);
                //arrivingArea.SetActive(false);
            }
            else
            {
                if (stayInCar == false && calculatedTheNearestDoor == false && isDead == false)
                {
                    carArea.SetActive(false);
                    //arrivingArea.SetActive(false);
                    if (collidersAreEnabled)
                    {
                        for (int i = 0; i < colliders.Length; i++)
                        {
                            colliders[i].enabled = false;
                        }
                        collidersAreEnabled = false;
                    }
                }
            }
            //urca in masina
            if (Car.isBussy == false && isDead == false && Player.velocity.magnitude < 0.1f && (Vector3.Distance(whereToEnterInCarBL.transform.position, transform.position) < 1.5f || Vector3.Distance(whereToEnterInCarBR.transform.position, transform.position) < 1.5f || Vector3.Distance(whereToEnterInCarFR.transform.position, transform.position) < 1.5f))
            {
                if (calculatedTheNearestDoor == false)
                {
                    FRDist = Vector3.Distance(transform.position, whereToEnterInCarFR.transform.position);
                    BRDist = Vector3.Distance(transform.position, whereToEnterInCarBR.transform.position);
                    BLDist = Vector3.Distance(transform.position, whereToEnterInCarBL.transform.position);
                    calculatedTheNearestDoor = true;
                    if (FRDist < BRDist && FRDist < BLDist)
                    {
                        FRNearest = true;
                        BRNearest = false;
                        BLNearest = false;
                    }
                    else if (BRDist < FRDist && BRDist < BLDist)
                    {
                        FRNearest = false;
                        BRNearest = true;
                        BLNearest = false;
                    }
                    else
                    {
                        FRNearest = false;
                        BRNearest = false;
                        BLNearest = true;
                    }
                }
                if (FRNearest)
                {
                    car.Brake();
                    Accelerate.interactable = false;
                    car.NormalTexture();
                    anim.SetBool("isWaving", false);
                    anim.SetBool("isGettingInRightDoor", false);
                    anim.SetBool("isGettingOutRightDoor", false);
                    anim.SetBool("isGettingInLeftDoor", false);
                    anim.SetBool("isGettingOutLeftDoor", false);
                    anim.SetBool("isWalking", true);
                    carArea.SetActive(false);

                    Vector3 dir = whereToEnterInCarFR.transform.position - transform.position;
                    Quaternion rot = Quaternion.LookRotation(dir);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * rotSpeed);
                    transform.position = Vector3.MoveTowards(transform.position, whereToEnterInCarFR.transform.position, Time.deltaTime * moveSpeed);


                    if (Vector3.Distance(transform.position, whereToEnterInCarFR.transform.position) == 0f)
                    {
                        Car.isBussy = true;
                        transform.position = whereToEnterInCarFR.transform.position;
                        transform.rotation = whereToEnterInCarFR.transform.rotation;
                        anim.SetBool("isWaving", false);
                        anim.SetBool("isWalking", false);
                        anim.SetBool("isGettingInRightDoor", true);
                        anim.SetBool("isGettingOutRightDoor", false);
                        anim.SetBool("isGettingInLeftDoor", false);
                        anim.SetBool("isGettingOutLeftDoor", false);
                        openFRDoorAnim.Play();
                    }
                }
                else if (BRNearest)
                {
                    car.Brake();
                    Accelerate.interactable = false;
                    car.NormalTexture();
                    anim.SetBool("isWaving", false);
                    anim.SetBool("isGettingInRightDoor", false);
                    anim.SetBool("isGettingOutRightDoor", false);
                    anim.SetBool("isGettingInLeftDoor", false);
                    anim.SetBool("isGettingOutLeftDoor", false);
                    anim.SetBool("isWalking", true);
                    carArea.SetActive(false);

                    Vector3 dir = whereToEnterInCarBR.transform.position - transform.position;
                    Quaternion rot = Quaternion.LookRotation(dir);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * rotSpeed);
                    transform.position = Vector3.MoveTowards(transform.position, whereToEnterInCarBR.transform.position, Time.deltaTime * moveSpeed);


                    if (Vector3.Distance(transform.position, whereToEnterInCarBR.transform.position) == 0f)
                    {
                        Car.isBussy = true;
                        transform.position = whereToEnterInCarBR.transform.position;
                        transform.rotation = whereToEnterInCarBR.transform.rotation;
                        anim.SetBool("isWaving", false);
                        anim.SetBool("isWalking", false);
                        anim.SetBool("isGettingInRightDoor", true);
                        anim.SetBool("isGettingOutRightDoor", false);
                        anim.SetBool("isGettingInLeftDoor", false);
                        anim.SetBool("isGettingOutLeftDoor", false);
                        openBRDoorAnim.Play();
                    }
                }
                else if (BLNearest)
                {
                    car.Brake();
                    Accelerate.interactable = false;
                    car.NormalTexture();
                    anim.SetBool("isWaving", false);
                    anim.SetBool("isGettingInRightDoor", false);
                    anim.SetBool("isGettingOutRightDoor", false);
                    anim.SetBool("isGettingInLeftDoor", false);
                    anim.SetBool("isGettingOutLeftDoor", false);
                    anim.SetBool("isWalking", true);
                    carArea.SetActive(false);

                    Vector3 dir = whereToEnterInCarBL.transform.position - transform.position;
                    Quaternion rot = Quaternion.LookRotation(dir);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * rotSpeed);
                    transform.position = Vector3.MoveTowards(transform.position, whereToEnterInCarBL.transform.position, Time.deltaTime * moveSpeed);


                    if (Vector3.Distance(transform.position, whereToEnterInCarBL.transform.position) == 0f)
                    {
                        Car.isBussy = true;
                        transform.position = whereToEnterInCarBL.transform.position;
                        transform.rotation = whereToEnterInCarBL.transform.rotation;
                        anim.SetBool("isWaving", false);
                        anim.SetBool("isWalking", false);
                        anim.SetBool("isGettingInRightDoor", false);
                        anim.SetBool("isGettingOutRightDoor", false);
                        anim.SetBool("isGettingInLeftDoor", true);
                        anim.SetBool("isGettingOutLeftDoor", false);
                        openBLDoorAnim.Play();
                    }
                }
                if (collidersAreEnabled == true)
                {
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        colliders[i].enabled = false;
                    }
                    collidersAreEnabled = false;
                }
            }
            if (stayInCar == true)
            {
                if (FRNearest)
                {
                    transform.position = whereToEnterInCarFR.transform.position;
                    transform.rotation = whereToEnterInCarFR.transform.rotation;
                }
                else if (BRNearest)
                {
                    transform.position = whereToEnterInCarBR.transform.position;
                    transform.rotation = whereToEnterInCarBR.transform.rotation;
                }
                else if (BLNearest)
                {
                    transform.position = whereToEnterInCarBL.transform.position;
                    transform.rotation = whereToEnterInCarBL.transform.rotation;
                }
                if (time >= 0f)
                {
                    time -= Time.deltaTime;
                    timeText.text = $"TIME: {(int)time / 60}:{time % 60:00}";
                }

                arrow.SetActive(true);
                Vector3 lookPos = arrivingWaypoint.transform.position - arrow.transform.position;
                Quaternion lookRot = Quaternion.LookRotation(lookPos, Vector3.up);
                Quaternion rotation = Quaternion.Euler(15, lookRot.eulerAngles.y, 0);
                arrow.transform.rotation = rotation;
            }

            if (Vector3.Distance(Player.transform.position, arrivingWaypoint.transform.position) < 1.5f && Player.velocity.magnitude < 0.1f && Car.isBussy == true && stayInCar == true)
            {
                //ajunge la destinatie
                car.Brake();
                Accelerate.interactable = false;
                car.NormalTexture();
                anim.SetBool("isWaving", false);
                anim.SetBool("isWalking", false);
                anim.SetBool("isGettingInRightDoor", false);
                anim.SetBool("isGettingInLeftDoor", false);
                if (doorPlayedAnim == false)
                {
                    if (BLNearest)
                    {
                        anim.SetBool("isGettingOutLeftDoor", true);
                        openBLDoorAnim.Play();
                    }
                    else if (BRNearest)
                    {
                        anim.SetBool("isGettingOutRightDoor", true);
                        openBRDoorAnim.Play();
                    }
                    else if (FRNearest)
                    {
                        anim.SetBool("isGettingOutRightDoor", true);
                        openFRDoorAnim.Play();
                    }
                    doorPlayedAnim = true;
                }
                timePanel.SetActive(false);
            }

            if (Vector3.Distance(transform.position, Player.transform.position) > 5f)
            {
                if (collidersAreEnabled == false && stayInCar == false)
                {
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        colliders[i].enabled = true;
                    }
                    collidersAreEnabled = true;
                }
            }

            if (time <= 0f)
            {
                isOutOfTime = true;
                if (isOutOfTime)
                {
                    isOutOfTime = false;
                    //scad banii primiti
                }
            }

            if (isDead == true || transform.position.y > 2 || transform.position.y < -2)
            {
                //respawn dupa ce moare
                if (Vector3.Distance(arrivingWaypoint.transform.position, Player.transform.position) > 30f && Vector3.Distance(transform.position, Player.transform.position) > 15f)
                {
                    transform.position = initialPosition;
                    rb.isKinematic = true;
                    isDead = false;
                    anim.enabled = true;
                    anim.SetBool("isWaving", false);
                    anim.SetBool("isGettingInRightDoor", false);
                    anim.SetBool("isGettingOutRightDoor", false);
                    anim.SetBool("isGettingInLeftDoor", false);
                    anim.SetBool("isGettingOutLeftDoor", false);
                    anim.SetBool("isWalking", true);
                }
            }
        }
        else
        {
            anim.enabled = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            rb.isKinematic = false;
            isDead = true;
            anim.enabled = false;
            carArea.SetActive(false);
        }
    }

    IEnumerator GetTheOtherClients()
    {
        yield return new WaitForSeconds(5f);

        foreach (GameObject client in GameObject.FindGameObjectsWithTag("client"))
        {
            if (client != gameObject)
            {
                otherClients.Add(client);
            }
        }
    }

    private void DestroyOtherClient(GameObject gameObj)
    {
        otherClients.Remove(gameObj);
        Destroy(gameObj);
    }

    public void StayInCarEqualsTrue()
    {
        Accelerate.interactable = true;
        if (BLNearest)
        {
            transform.SetParent(whereToEnterInCarBL.transform);
        }
        else if (BRNearest)
        {
            transform.SetParent(whereToEnterInCarBR.transform);
        }
        else if (FRNearest)
        {
            transform.SetParent(whereToEnterInCarFR.transform);
        }
        stayInCar = true;
        carArea.SetActive(false);
        arrivingWaypoint.SetActive(true);
        car.DontBrake();
        time = theTime;
        timePanel.SetActive(true);
        doorPlayedAnim = false;
        car.TaxiDome();
        for (int i = otherClients.Count - 1; i >= 0; i--)
        {
            DestroyOtherClient(otherClients[i].gameObject);
        }
    }

    public void StayInCarEqualsFalse()
    {
        Accelerate.interactable = true;
        BLNearest = false;
        BRNearest = false;
        FRNearest = false;
        transform.SetParent(null);
        stayInCar = false;
        Car.isBussy = false;
        car.DontBrake();
        car.TaxiDome();
        arrivingWaypoint.SetActive(false);
        arrow.SetActive(false);
        aiSpawner.Spawn();
        Destroy(gameObject);
    }
}