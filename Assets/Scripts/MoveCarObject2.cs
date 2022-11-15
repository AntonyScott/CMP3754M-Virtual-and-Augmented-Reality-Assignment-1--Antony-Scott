using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCarObject2 : MonoBehaviour
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
            if(state == 1)
            {
                transform.position = new Vector3(-3, 1, -7);

                state = 0;
            }

            else if (state == 0)
            {
                transform.position = new Vector3(3, 15, 7);

                state = 1;
            }

            stateTimer = 10;
        }
    }
}
