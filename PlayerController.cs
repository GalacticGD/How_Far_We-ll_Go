using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private int extraJumps;
    public int extraJumpsValue;
    private bool facingRight = false;

    public KeyCode left;
    public KeyCode right;
    public KeyCode up;
    public KeyCode interact;

    private Rigidbody2D rb;
    private Animator anim;


    public Transform groundCheck;
    private bool isGrounded;
    public float groundRadius;
    public LayerMask whatIsGround;

    private bool carryingObject = false;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetKey(up) && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
        } else if (Input.GetKey(up) && extraJumps > 0)
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
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        if (Input.GetKey(left))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            anim.SetBool("IsRunning", true);
            if (facingRight)
            {
                Flip();
                facingRight = false;
            }
        }
        else if (Input.GetKey(right))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
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
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    
}
