using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    private Rigidbody2D enemyRb;
    public GameObject player;
    private bool facingRight = false;
    public bool isMoving = false;

    [SerializeField] private bool isAgro = false;
    public float agroDistance = 10.0f;

    public float speed = 5.0f;

    public Animator antAnimation;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        antAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isMoving = enemyRb.velocity.magnitude != 0;
        antAnimation.SetBool("moving", isMoving);

        // Flip character to the correct direction when moving
        if (enemyRb.velocity.x >= 0 && !facingRight)
        {
            FlipFacing();
        }
        else if (enemyRb.velocity.x < 0 && facingRight)
        {
            FlipFacing();
        }

        // Turn agro if player is in proximity of enemy
        isAgro = Vector2.Distance(player.transform.position, transform.position) <= agroDistance;

        if (isAgro && !(player.transform.position.y > transform.position.y + 1))
        {
            followPlayer();
        }

        if (ButtonEvents.end)
        {
            enemyRb.constraints = RigidbodyConstraints2D.FreezeAll;
            isMoving = false;
        }

        // if (ButtonEvents.newScene)
        // {
        //     enemyRb.constraints = RigidbodyConstraints2D.None;
        //     enemyRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        // }
    }

    void FlipFacing()
    {
        facingRight = !facingRight;
        Vector3 charScale = transform.localScale;
        charScale.x *= -1.0f;
        transform.localScale = charScale;
    }

    void followPlayer()
    {
        // Ant will horizontally approach player
        Vector3 direction = Vector3.Normalize(new Vector3(player.transform.position.x - transform.position.x, 0));
        enemyRb.velocity = direction * speed;
    }

    public void freeze()
    {
        StartCoroutine(freezeTime());
    }

    IEnumerator freezeTime()
    {
        enemyRb.constraints = RigidbodyConstraints2D.FreezeAll;
        isMoving = false;
        ButtonEvents.candyForAnt = false;
        yield return new WaitForSeconds(8f);
        enemyRb.constraints = RigidbodyConstraints2D.None;
        enemyRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        isMoving = true;
    }
}
