using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mushroom1 : MonoBehaviour
{
    public string[] sent;
    public int sentNow;

    bool player;

    public Text textD;
    public GameObject panel;
    public GameObject e;
    public Transform spwan;
    GameObject clone;
    public GameObject hpBonus;
    private void Start()
    {
        player = false;
        sentNow = 0;
        panel.SetActive(false);
    }

    private void Update()
    {
        if (sentNow > sent.Length)
        {
            sentNow = sent.Length;
        }

        if (Input.GetKeyDown(KeyCode.E) && (player == true))
        {
            if(sentNow >= sent.Length)
            {
                Instantiate(hpBonus, spwan.position, Quaternion.identity);
            }
            panel.SetActive(true);
            textD.text = sent[sentNow];
            sentNow++;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = true;
            clone = Instantiate(e, spwan);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = false;
            Destroy(clone.gameObject);
            panel.SetActive(false);
        }
    }
}
