using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    public GameObject lava;
    public GameObject vhod;
    public GameObject vihod;
    public Transform spawn;

    private void Start()
    {
        vhod.SetActive(false);
        vihod.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(lava, spawn.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            vhod.SetActive(true);
            vihod.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            vhod.SetActive(false);
            vihod.SetActive(true);
        }
    }
}
