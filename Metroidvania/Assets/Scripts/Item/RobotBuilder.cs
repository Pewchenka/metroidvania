using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBuilder : MonoBehaviour
{
    public GameObject robot;
    public Transform spawn;

    float waittime;
    float startwaittime = 3f;

    private void Start()
    {
        waittime = startwaittime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(robot, spawn.position, Quaternion.identity);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            waittime -= Time.deltaTime;
            if(waittime <= 0)
            {
                Instantiate(robot, spawn.position, Quaternion.identity);
                waittime = startwaittime;
            }
        }
    }
}
