using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerMove : MonoBehaviour
{
    //хелс - здоровье сейчас, номбер лайв - максимальное здоровье на данный момент, ливес - максимальное здоровьее вообще!
    public int health;
    public int numberLive;
    public Image[] lives;
    public Sprite fullhealth;
    public Sprite empetyhealth;
    public GameObject bonusHpObject;

    public float speed = 1.5f;
    public float jumpForce;
    private float moveInput;

    private bool facingRight = true;

    private Rigidbody2D rb;

    private bool isGround;
    public Transform groundChek;
    public float radiusChek;
    public LayerMask whatIsGround;
    private bool isUpDirt;

    bool canAttack;
    public GameObject attackHitBox;
    public Transform attacka;

    private Animator anim;
    public Animator hpUpAnim;
    public Animator speedUpAnim;
    public Animator pickOnAnim;

    float coyotStart;
    float coyotTimer;
    float jumpBufferStart;
    float jumpBuffer;

    //ѕеременные которые олицитвор€ют баффы
    private bool hpUp;
    private bool bonusHp;
    private bool pickAxe;
    private bool speedUp;
    private void Start()
    {
        bonusHp = false;
        hpUp = false;
        pickAxe = false;
        speedUp = false;

        speed = 1.5f;

        health = numberLive;
        
        canAttack = true;
        
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        hpUpAnim.SetBool("HpUp", false);
        speedUpAnim.SetBool("SpeedBuffOFF", true);
        pickOnAnim.SetBool("PickON", false);

        coyotStart = 0.2f;
        jumpBufferStart = 0.2f;
    }
    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if(facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if(facingRight == true && moveInput < 0)
        {
            Flip();
        }
        
        if(moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }
       
        if(hpUp == true)
        {
            numberLive = 4;
        }
        else
        {
            numberLive = 3;
        }

        if (bonusHp == true)
        {
            bonusHpObject.SetActive(true);
        } else
        {
            bonusHpObject.SetActive(false);
        }
    }
    private void Update()
    {
        isGround = Physics2D.OverlapCircle(groundChek.position, radiusChek, whatIsGround);
        if (isGround || isUpDirt)
        {
            coyotTimer = coyotStart;
        } 
        else
        {
            coyotTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBuffer = jumpBufferStart;
        }
        else
        {
            jumpBuffer -= Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            coyotTimer = 0f; 
        }

        if ((coyotTimer > 0f) && (jumpBuffer > 0f))
        {
            rb.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("takeOff");
        }

        if(isGround == true)
        {
                anim.SetBool("isJumping", false);
        }
        else
        {
                anim.SetBool("isJumping", true);
        }

        if (Input.GetButtonDown("Fire1") && canAttack == true)
        {
            canAttack = false;
            anim.SetTrigger("isAttack");
            StartCoroutine(DoAttack());
        }

        if (health > numberLive)
        {
            health = numberLive;
        }

        for (int i = 0; i < lives.Length; i++)
        {
            if (i < health)
            {
                lives[i].sprite = fullhealth;
            }
            else
            {
                lives[i].sprite = empetyhealth;
            }
            
            if (i < numberLive)
            {
                lives[i].enabled = true;     
            }
            else
            {
                lives[i].enabled = false;
            }
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (pickAxe ==true && collision.gameObject.tag == "UpDirt")
        {
            isUpDirt = true;
        }else
        {
            isUpDirt = false;
        }
        if (collision.gameObject.tag == "EnemyAttack")
        {
            if (bonusHp == true)
            {
                bonusHp = false;
            }
            else
            {
                health--;
            }
        }
        if(collision.gameObject.tag == "MovingPlatform")
        {
            this.transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            this.transform.parent = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HpUp"))
        {
            StartCoroutine(Hpup());
        }
        
        if (collision.CompareTag("HpBonus"))
        {
            bonusHp = true;
        }

        if (collision.CompareTag("PickAxe"))
        {
            pickOnAnim.SetBool("PickON", true);
            pickAxe = true;
        }

        if (collision.CompareTag("SpeedBuffs"))
        {
            speedUpAnim.SetBool("SpeedBuffOFF", false);
            speed = 2.2f;
            speedUp = true;
        }

        if (collision.CompareTag("EnemyAttack"))
        {
            if (bonusHp == true)
            {
                bonusHp = false;
            }
            else
            {
                health--;
            }
        }
    }

    IEnumerator DoAttack()
    {
        Instantiate(attackHitBox, attacka.position, Quaternion.identity);
        yield return new WaitForSeconds(.25f);
        canAttack = true;
    }
    IEnumerator Hpup()
    {
        hpUpAnim.SetBool("HpUp", true);
        hpUp = true;
        yield return new WaitForSeconds(0.1f);
        health++;
    }

}
