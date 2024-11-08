using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;

    public Transform[] moveSpot;
    public int a;

    public float waitTime;

    private void Start()
    {
        a = 0;
        speed = 1;
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpot[a].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpot[a].position) <= 0.2f)
        {
            StartCoroutine(wait());
            a++;
        }
        if (a > 1)
        {
            a = 0;
        }
    }

    IEnumerator wait()
    {
        speed = 0;
        yield return new WaitForSeconds(waitTime);
        speed = 1;
    }
}
