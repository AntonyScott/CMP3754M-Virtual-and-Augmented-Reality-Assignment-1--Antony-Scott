using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianRoute : MonoBehaviour
{
    public List<Transform> wps;
    public List<Transform> route;
    public int routeNumber = 0;
    public int targetWP;
    public float dist;
    public Rigidbody rb;

    public bool go = false;
    public float initialDelay;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        wps = new List<Transform>();
        GameObject wp;

        wp = GameObject.Find("WP1");
        wps.Add(wp.transform);

        wp = GameObject.Find("WP2");
        wps.Add(wp.transform);

        wp = GameObject.Find("WP3");
        wps.Add(wp.transform);

        wp = GameObject.Find("WP4");
        wps.Add(wp.transform);

        wp = GameObject.Find("WP5");
        wps.Add(wp.transform);

        wp = GameObject.Find("WP6");
        wps.Add(wp.transform);

        wp = GameObject.Find("WP7");
        wps.Add(wp.transform);

        wp = GameObject.Find("WP8");
        wps.Add(wp.transform);

        SetRoute();

        //randomises delay and hides pedestrian till ready to start 1st route
        initialDelay = Random.Range(2.0f, 12.0f);
        transform.position = new Vector3(0.0f, -5.0f, 0.0f);
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
        displacement.y = 0;
        //float dist = displacement.magnitude;
        dist = displacement.magnitude;

        if(dist < 0.1f)
        {
            targetWP++;
            if(targetWP >= route.Count)
            {
                SetRoute();
                return;
            }
        }

        //calculates velocity for each frame
        Vector3 velocity = displacement;
        velocity.Normalize();
        velocity *= 2.5f;
        //applies said velocity
        Vector3 newPosition = transform.position;
        newPosition += velocity * Time.deltaTime;
        rb.MovePosition(newPosition);

        //align rotation to velocity
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, velocity, 10.0f * Time.deltaTime, 0.0f); //(10.0f is rotation speed)
        Quaternion rotation = Quaternion.LookRotation(desiredForward);
        rb.MoveRotation(rotation);
    }

    void SetRoute()
    {
        routeNumber = Random.Range(0, 12);

        if (routeNumber == 0)
            route = new List<Transform>
            {
                wps[0],
                wps[4],
                wps[5],
                wps[6]
            };
        if (routeNumber == 1)
            route = new List<Transform>
            {
                wps[0],
                wps[4],
                wps[5],
                wps[7]
            };
        if (routeNumber == 2)
            route = new List<Transform>
            {
                wps[2],
                wps[1],
                wps[4],
                wps[5],
                wps[6]
            };
        if (routeNumber == 3)
            route = new List<Transform>
            {
                wps[2],
                wps[1],
                wps[4],
                wps[5],
                wps[7]
            };
        if (routeNumber == 4)
            route = new List<Transform>
            {
                wps[3],
                wps[4],
                wps[5],
                wps[6],
            };
        if (routeNumber == 5)
            route = new List<Transform>
            {
                wps[3],
                wps[4],
                wps[5],
                wps[7],
            };
        if (routeNumber == 6)
            route = new List<Transform>
            {
                wps[6],
                wps[5],
                wps[4],
                wps[0],
            };
        if (routeNumber == 7)
            route = new List<Transform>
            {
                wps[6],
                wps[5],
                wps[4],
                wps[3],
            };
        if (routeNumber == 8)
            route = new List<Transform>
            {
                wps[6],
                wps[5],
                wps[4],
                wps[0],
            };
        if (routeNumber == 9)
            route = new List<Transform>
            {
                wps[7],
                wps[5],
                wps[4],
                wps[0],
            };
        if (routeNumber == 10)
            route = new List<Transform>
            {
                wps[7],
                wps[5],
                wps[4],
                wps[3],
            };
        else if (routeNumber == 11)
            route = new List<Transform>
            {
                wps[7],
                wps[5],
                wps[4],
                wps[1],
                wps[2]
            };
        transform.position = new Vector3(route[0].position.x, 0.0f, route[0].position.z);
        targetWP = 1;
    }
}
