using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalakSpawn : MonoBehaviour
{
    public Transform start;
    public Transform end;

    public GameObject stalaktit;

    float waitTime;
    public float startWaitTime;

    private void Start()
    {
        waitTime = startWaitTime;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                Vector3 f = new Vector3(Random.Range(start.position.x, end.position.x), start.position.y, start.position.z);
                Instantiate(stalaktit, f, Quaternion.identity);
                waitTime = startWaitTime;
            }
        }
    }
   
}
