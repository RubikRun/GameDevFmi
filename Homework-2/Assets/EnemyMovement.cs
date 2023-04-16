using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private float jumpPower = 9f;

    private bool onPlatform = false;
    private GameObject platform;
    private BoxCollider2D platformCollider;

    GameObject player;

    private Rigidbody2D rigidBody;

    // how many seconds it takes for the enemy to react to an approaching edge and do a jump
    private float jumpReactTime = 0.1f;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vecToPlayer = player.transform.position - transform.position;
        float horizontalMove = 1f;
        if (vecToPlayer.x > 0f)
        {
            transform.localScale = new Vector3
            (
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
        else
        {
            horizontalMove = -1f;
            transform.localScale = new Vector3
            (
                -Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }

        if (onPlatform && shouldJump(horizontalMove))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpPower);
            print("Jump");
        }

        rigidBody.velocity = new Vector2(horizontalMove * speed, rigidBody.velocity.y);

        animator.SetBool("isWalking", horizontalMove != 0 && onPlatform);
    }

    private bool shouldJump(float horizontalMove)
    {
        Vector3 nextPosition = transform.position + new Vector3(horizontalMove * speed * jumpReactTime, 0f, 0f);
        return (nextPosition.x > platform.transform.position.x + platformCollider.size.x / 2
             || nextPosition.x < platform.transform.position.x - platformCollider.size.x / 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onPlatform = true;
            platform = collision.gameObject;
            platformCollider = platform.GetComponent<BoxCollider2D>();
            animator.SetBool("isJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onPlatform = false;
            animator.SetBool("isJumping", true);
        }
    }
}
