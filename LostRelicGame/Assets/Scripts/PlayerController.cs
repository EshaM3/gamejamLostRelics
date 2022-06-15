using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator playerAnimation;
    public WebGrapple webGrp;

    public float speed = 5.0f;
    public float airForce = 2.0f;
    public float jumpForce = 5.0f;

    public bool isGrounded = true;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimation = GetComponent<Animator>();
        webGrp = gameObject.GetComponent<WebGrapple>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get left/right movement input
        float horizontalInput = Input.GetAxis("Horizontal");

        // Flip character to the correct direction when moving
        if (horizontalInput > 0.01 && !facingRight)
        {
            FlipFacing();
        } else if (horizontalInput < 0 && facingRight)
        {
            FlipFacing();
        }

        // Checks for on ground movement
        if (!webGrp.isGrappling)
        {
            transform.Translate(Vector2.right * horizontalInput * speed * Time.deltaTime);
        } else
        {
            rb.AddForce(new Vector2(horizontalInput * airForce, 0));
        }

        // Checks for jump input
        bool jump = Input.GetKey(KeyCode.Space);
        if (jump && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            isGrounded = false;
        }

        // Check if moving
        if (horizontalInput != 0)
        {
            playerAnimation.SetBool("moving", true);
        } else {
            playerAnimation.SetBool("moving", false);
        }

        // Check if swinging
        if (webGrp.isGrappling)
        {
            playerAnimation.SetBool("swinging", true);
        } else
        {
            playerAnimation.SetBool("swinging", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    //trigger - inventory :)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item" &&
            collision.TryGetComponent<ItemObject>(out ItemObject item))
        {
            item.PickupItem();
        }
    }

    void FlipFacing()
    {
        facingRight = !facingRight;
        Vector3 charScale = transform.localScale;
        charScale.x *= -1.0f;
        transform.localScale = charScale;
    }
}
