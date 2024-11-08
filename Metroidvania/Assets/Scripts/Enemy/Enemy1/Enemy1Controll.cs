using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controll : MonoBehaviour
{
    public Transform gun;
    Transform player;
    public GameObject projectile;
    private float timeBTWshoot;
    public float startTimeBTWshoot;
    public bool playerInRadius = false;

    private bool facingLeft = true;

    private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBTWshoot = startTimeBTWshoot;
    }
    private void FixedUpdate()
    {
        if(player.position.x < gameObject.transform.position.x &facingLeft == false)
        {
            Flip();
        }
        if (player.position.x > gameObject.transform.position.x & facingLeft == true)
        {
            Flip();
        }
        if (((gameObject.transform.position.x - player.transform.position.x > -4) && (gameObject.transform.position.x - player.transform.position.x < 4)) && ((gameObject.transform.position.y - player.transform.position.y > -4) && (gameObject.transform.position.y - player.transform.position.y < 4)))
        {
            playerInRadius = true;
            anim.SetTrigger("Found");
        }
        else
        {
            playerInRadius = false;
            anim.SetTrigger("Hide");
        }
        
        if (timeBTWshoot <= 0 & playerInRadius == true)
        {
            Instantiate(projectile, gun.position, Quaternion.identity);
            timeBTWshoot = startTimeBTWshoot;
        }
        else
        {
            timeBTWshoot -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            Destroy(gameObject);
        }
    }

    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
