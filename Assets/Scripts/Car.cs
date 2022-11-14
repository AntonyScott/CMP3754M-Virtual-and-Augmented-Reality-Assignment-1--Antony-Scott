using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public List<Transform> wps;
    public List<Transform> route;
    public int routeNumber = 0;
    public int targetWP = 0;

    public bool go = false;
    public float initialDelay;

    public Rigidbody rb;

    TrafficLights trafficLights;

    // Start is called before the first frame update
    void Start()
    {
        wps = new List<Transform>();
        GameObject wp;

        wp = GameObject.Find("CWP1");
        wps.Add(wp.transform);

        wp = GameObject.Find("CWP2");
        wps.Add(wp.transform);

        wp = GameObject.Find("CWP3");
        wps.Add(wp.transform);

        wp = GameObject.Find("CWP4");
        wps.Add(wp.transform);

        initialDelay = Random.Range(2.0f, 12.0f);
        transform.position = new Vector3(0.0f, -1.5f, 0.0f);

        rb = GetComponent<Rigidbody>();

        SetRoute();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!go)
        {
            initialDelay -= Time.deltaTime;
            if (initialDelay <= 0.0f)
            {
                go = true;
                SetRoute();
            }
            else return;
        }

        Vector3 displacement = route[targetWP].position - transform.position;
        displacement.y = 0f;
        float dist = displacement.magnitude;

        if (dist < 0.1f)
        {
            targetWP++;
            if (targetWP >= route.Count)
            {
                SetRoute();
                return;
            }
        }

        //calculate velocity of the frame
        Vector3 velocity = displacement;
        velocity.Normalize();
        velocity *= 10f;
        //apply velocity
        Vector3 newPosition = transform.position;
        newPosition += velocity * Time.deltaTime;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.MovePosition(newPosition);

        //align to velocity
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, velocity, 10.0f * Time.deltaTime, 0f);
        Quaternion rotation = Quaternion.LookRotation(desiredForward);
        rb.MoveRotation(rotation);



    }

    void SetRoute()
    {
        //randomise the next route
        routeNumber = Random.Range(0, 4);

        //set the route waypoints
        if (routeNumber == 0) 
            route = new List<Transform> 
            { 
                wps[0], 
                wps[1] 
            };
        else if (routeNumber == 2) 
            route = new List<Transform> 
            { 
                wps[2], 
                wps[3] 
            };

        //initialise position and waypoint counter
        transform.position = new Vector3(route[0].position.x, 0.5f, route[0].position.z);
        targetWP = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        rb.constraints = RigidbodyConstraints.FreezePosition;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Left Collision");

        rb.constraints = RigidbodyConstraints.None;
    }
}
