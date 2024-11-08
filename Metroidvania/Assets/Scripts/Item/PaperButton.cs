using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperButton : MonoBehaviour
{
    public GameObject e;
    public Transform spawn;
    GameObject clone;
    public Animator anim;
    private bool player;

    private void Start()
    {
        player = false;
    }
    private void FixedUpdate()
    {
        if (player == true && Input.GetKey(KeyCode.E))
        {
            anim.SetBool("isPlayer",true);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = true;
            clone = Instantiate(e, spawn);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = false;
            Destroy(clone.gameObject);
        }
    }
    public void destroy()
    {
        anim.SetBool("isPlayer", false);
    }
}
