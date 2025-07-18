using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator playerAnimation;
    private SpringGrapple springGrapple;

    public float speed = 5.0f;
    public float airForce = 2.0f;
    public float jumpForce = 5.0f;

    public bool isGrounded = true;
    private bool facingRight = true;

    public float downForce = -1.0f;
    public float maxHeight = 7;

    //walking limit
    public float xRangeLeft = -39.0f;
    public float xRangeRight = 65.0f;

    // Audio
    public AudioClip reallyDeepAndDisturbingHumanoidYetNonEuclideanBreathingSound;
    public AudioClip movingSound;
    public AudioClip jumpSound;
    public AudioClip hitSound;
    public AudioClip itemPickup;

    //Health
    public GameObject[] HealthHearts = new GameObject[6];
    int numOfHits = 0;
    public float knockBack = 30.0f;
    public float knockUp = 20.0f;

    public ParticleSystem dust;

    public GameObject antInRange;
    public bool antIsInRange;
    public InventoryItemData referenceItem_Candy;
    public InventoryItemData referenceItem_Rope;


    // Start is called before the first frame update
    void Start()
    {
        playerAnimation = GetComponent<Animator>();
        springGrapple = gameObject.GetComponent<SpringGrapple>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerAnimation.SetBool("jumping", !isGrounded);
        playerAnimation.SetBool("swinging", springGrapple.isGrappling);

        // Prevent player from moving above screen
        if (transform.position.y > maxHeight)
        {
            rb.velocity = new Vector2(rb.velocity.x, downForce);
        }

        // Get left/right movement input
        float horizontalInput = Input.GetAxis("Horizontal");

        // Flip character to the correct direction when moving
        if (horizontalInput > 0.01 && !facingRight)
        {
            FlipFacing();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            FlipFacing();
        }

        // Checks for movement without grappling
        if (!springGrapple.isGrappling)
        {
            // Horizontal movement on ground
            transform.Translate(Vector2.right * horizontalInput * speed * Time.deltaTime);

            // Checks for jump input
            bool jump = Input.GetKey(KeyCode.Space);
            if (jump && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, speed);
                CreateDust();
                SoundManager.instance.PlaySound(jumpSound);
            }
        }
        else
        {
            // Horizontal movement in air
            rb.AddForce(new Vector2(horizontalInput * airForce, 0));
        }



        // Check if moving
        if (horizontalInput != 0)
        {
            playerAnimation.SetBool("moving", true);

            if (isGrounded)
            {
                CreateDust();
            }
        }
        else
        {
            playerAnimation.SetBool("moving", false);
        }

        //walking x-value limit
        if (gameObject.transform.position.x < xRangeLeft) gameObject.transform.position =
                new Vector2(xRangeLeft, gameObject.transform.position.y);
        if (gameObject.transform.position.x > xRangeRight) gameObject.transform.position =
                new Vector2(xRangeRight, gameObject.transform.position.y);

        // End of game - freeze spider
        if (ButtonEvents.end)
        {
            InventorySystem.current.Remove(referenceItem_Rope);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (antIsInRange && (antInRange != null) && ButtonEvents.candyForAnt)
        {
            antInRange.GetComponent<Ant>().freeze();
            InventorySystem.current.Remove(referenceItem_Candy);
            ButtonEvents.candyForAnt = false;
            SoundManager.instance.PlaySound(itemPickup);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        //Need to tag ant as "Enemy"
        if (collision.gameObject.CompareTag("Enemy") && numOfHits < 6 && collision.gameObject.GetComponent<Ant>().isMoving)
        {
            HealthHearts[numOfHits].gameObject.SetActive(false);
            numOfHits++;

            float knockBackDirection = transform.position.x - collision.gameObject.transform.position.x;
            knockBackDirection /= Mathf.Abs(knockBackDirection);

            rb.AddForce(new Vector2(knockBackDirection * knockBack, knockUp));

            SoundManager.instance.PlaySound(hitSound);
        }

        if (collision.gameObject.CompareTag("Enemy") && numOfHits == 6)
        {
            numOfHits++;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            SoundManager.instance.PlaySound(hitSound);
            StartCoroutine(killTime());
        }
    }

    IEnumerator killTime()
    {
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("GameOver");
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    //trigger - inventory :)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item" &&
            collision.TryGetComponent<ItemObject>(out ItemObject item))
        {
            item.PickupItem();
            SoundManager.instance.PlaySound(itemPickup);
        }

        //Need to tag any harmful areas like the ditches as "Killbox"
        if (collision.gameObject.CompareTag("Killbox"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            SoundManager.instance.PlaySound(hitSound);
            StartCoroutine(killTime());
        }

        if (collision.gameObject.CompareTag("OuterCandyZone"))
        {
            antInRange = collision.gameObject.transform.parent.gameObject;
            antIsInRange = true;
        }

        // //this doesn't work....
        // if (collision.gameObject.CompareTag("OuterCandyZone") && ButtonEvents.candyForAnt)
        // {
        //     Debug.Log("Something did register");
        //     if (collision.gameObject.transform.parent.gameObject.GetComponent<LocationTrigger>().inLocation)
        //     {
        //         collision.gameObject.transform.parent.gameObject.GetComponent<Ant>().freeze();
        //         ButtonEvents.candyForAnt = false;

        //     }
        // }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("OuterCandyZone"))
        {
            antInRange = null;
            antIsInRange = false;
        }
    }

    void FlipFacing()
    {
        facingRight = !facingRight;
        Vector3 charScale = transform.localScale;
        charScale.x *= -1.0f;
        transform.localScale = charScale;
    }

    public void CreateDust()
    {
        dust.Play();
    }
}
