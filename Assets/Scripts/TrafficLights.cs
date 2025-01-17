using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLights : MonoBehaviour
{
    public Transform t1;
    public Transform t2;
    public Transform t3;

    public GameObject t1green;
    public GameObject t1red;

    public GameObject t2green;
    public GameObject t2red;

    public GameObject t3green;
    public GameObject t3red;

    public float stateTimer;
    public int state;

    public GameObject trafficLightBlocker1;
    public GameObject trafficLightBlocker2;
    public GameObject trafficLightBlocker3;

    // Start is called before the first frame update
    void Start()
    {
        t1 = transform.Find("TL1");
        t2 = transform.Find("TL2");
        t3 = transform.Find("TL3");

        t1green = t1.Find("Green light").gameObject;
        t1red = t1.Find("Red light").gameObject;
        t2green = t2.Find("Green light").gameObject;
        t2red = t2.Find("Red light").gameObject;
        t3green = t3.Find("Green light").gameObject;
        t3red = t3.Find("Red light").gameObject;

        Instantiate(trafficLightBlocker1);
        Instantiate(trafficLightBlocker2);
        Instantiate(trafficLightBlocker3);

        trafficLightBlocker1.transform.position = new Vector3(3, 1, 7);
        trafficLightBlocker2.transform.position = new Vector3(-1, 1, -8);
        trafficLightBlocker3.transform.position = new Vector3(7, 1, -3);

        stateTimer = 10.0f;
        SetState(1);
    }

    // Update is called once per frame
    void Update()
    {
        stateTimer = stateTimer -= Time.deltaTime;
        if(stateTimer < 0)
        {
            if (state == 1)
            {
                SetState(0);
            }
            else if (state == 0)
            {
                SetState(1);
            }
            StateTimerReset();
        }
    }

    void SetState(int c)
    {
        state = c;

        if(c == 1)
        {
            //green, green, red
            t1green.SetActive(true);
            t1red.SetActive(false);
            t2green.SetActive(false);
            t2red.SetActive(true);
            t3green.SetActive(false);
            t3red.SetActive(true);
        }
        else
        {
            //red, green, green
            t1green.SetActive(false);
            t1red.SetActive(true);
            t2green.SetActive(true);
            t2red.SetActive(false);
            t3green.SetActive(true);
            t3red.SetActive(false);
        }
    }

    void StateTimerReset()
    {
        stateTimer = 10.0f;
    }
}
