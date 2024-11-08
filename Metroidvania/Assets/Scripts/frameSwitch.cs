using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frameSwitch : MonoBehaviour
{
    public GameObject activeFrame;

    private void Start()
    {
        activeFrame.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            activeFrame.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            activeFrame.SetActive(false);
        }
    }
}
