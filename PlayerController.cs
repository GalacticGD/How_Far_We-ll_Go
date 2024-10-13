using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float slowSpeed;
    private float speedSet;
    public float jumpForce;
    private int extraJumps;
    public int extraJumpsValue;
    private bool facingRight = false;
    public bool bouba;

    public KeyCode left;
    public KeyCode right;
    public KeyCode up;
    public KeyCode interact;

    private Rigidbody2D rb;
    private Animator anim;


    public Transform groundCheck;
    public Transform kikiTeleport;
    public Transform kikiTree;
    private bool isGrounded;
    public float groundRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsWater;

    private bool carryingObject = false;
    private string availableAction = null;

    private bool coroutineStarted = false;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        speedSet = speed;
    }

    void Update()
    {
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetKey(up) && isGrounded && !carryingObject)
        {
            rb.velocity = Vector2.up * jumpForce;
        } else if (Input.GetKey(up) && extraJumps > 0 && !carryingObject)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }

        if (rb.velocity.y > 0.01 && rb.velocity.y < -0.01)
        {
            anim.SetBool("IsJumping", true);
        }
        else
        {
            anim.SetBool("IsJumping", false);
        }

        if (availableAction != null)
        {
            if (availableAction.Equals("Climb") && Input.GetKey(interact))
            {
                StartCoroutine("ClimbTree");
            }
        }

        if (bouba)
        {
            if (Physics2D.OverlapCircle(transform.position, 1, whatIsWater))
            {
                anim.SetBool("IsSwimming", true);
            }
            else
            {
                anim.SetBool("IsSwimming", false);
            }
        } else
        {
            if (Physics2D.OverlapCircle(transform.position, 0.8f, whatIsWater) && !coroutineStarted)
            {
                StartCoroutine("TeleportKiki");
            }
        }
    }

   /* private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colObject = collision.gameObject;
        if (colObject.CompareTag("Door"))
        {
            if (colObject.GetComponent<Door>().isOpen)
            {
                this.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }*/

    public IEnumerator TeleportKiki()
    {
        coroutineStarted = true;
        yield return new WaitForSeconds(0.1f);
        transform.position = kikiTeleport.transform.position;
        anim.SetBool("Angry", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Angry", false);
        coroutineStarted = false;
    }

    public IEnumerator ClimbTree()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + .05f);
        anim.SetBool("IsClimbing", true);
        yield return new WaitForSeconds(.2f);
        transform.position = new Vector2(transform.position.x, transform.position.y + .05f);
        yield return new WaitForSeconds(.2f);
        transform.position = new Vector2(transform.position.x, transform.position.y + .05f);
        yield return new WaitForSeconds(.2f);
        anim.SetBool("IsClimbing", false);
        transform.position = kikiTree.position;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        if (Input.GetKey(left))
        {
            rb.velocity = new Vector2(-speedSet, rb.velocity.y);
            anim.SetBool("IsRunning", true);
            if (facingRight)
            {
                Flip();
                facingRight = false;
            }
        }
        else if (Input.GetKey(right))
        {
            rb.velocity = new Vector2(speedSet, rb.velocity.y);
            anim.SetBool("IsRunning", true);
            if (!facingRight)
            {
                Flip();
                facingRight = true;
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetBool("IsRunning", false);
        }
    }

    public void SetCarrying(bool setCarry)
    {
        carryingObject = setCarry;
        if (setCarry == true)
        {
            speedSet = slowSpeed;
        } else
        {
            speedSet = speed;
        }
    }

    public void SetAvailableAction(string action)
    {
        availableAction = action;
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    
}
