using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCarObject3 : MonoBehaviour
{
    public float stateTimer;
    public int state;

    void Start()
    {
        stateTimer = 10f;
    }

    void FixedUpdate()
    {
        if(stateTimer >= 0)
        {
            stateTimer = stateTimer - Time.deltaTime;
        }

        if(stateTimer <= 0)
        {
            if(state == 0)
            {
                transform.position = new Vector3(7, 1, -3);

                state = 1;
            }

            else if (state == 1)
            {
                transform.position = new Vector3(7, 15, -3);

                state = 0;
            }

            stateTimer = 10;
        }
    }
}
