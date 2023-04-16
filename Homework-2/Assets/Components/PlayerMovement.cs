using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 6f;

    [SerializeField]
    private float jumpPower = 6f;

    private Rigidbody2D rigidBody;
    private PlayerHealth health;

    private int keysCollected = 0;

    private bool onSpring = false;
    private bool onPlatform = false;

    private Vector3 startingPosition;

    [SerializeField]
    private Vector2 mapBorderMin = new Vector2(-10f, -10f);

    [SerializeField]
    private Vector2 mapBorderMax = new Vector2(55f, 15f);

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        health = GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(horizontalMove * speed, rigidBody.velocity.y);
        if (horizontalMove > 0)
        {
            transform.localScale = new Vector3
            (
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
        else if (horizontalMove < 0)
        {
            transform.localScale = new Vector3
            (
                -Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
        animator.SetBool("isWalking", horizontalMove != 0 && (onPlatform || onSpring));

        if (Input.GetButtonDown("Jump") && onPlatform)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpPower);
            print("Jump");
        }
        else if (onSpring)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpPower * 1.5f);
        }

        if (transform.position.x < mapBorderMin.x || transform.position.x > mapBorderMax.x
            || transform.position.y < mapBorderMin.y || transform.position.y > mapBorderMax.y)
        {
            Die();
        }
    }

    public void Die()
    {
        health.DecreaseHealth();
        if (health.IsGameOver())
        {
            // Start the previous scene. The previous scene before the game scene is supposed to be the menu scene.
            SceneManager.LoadScene("Menu");
        }
        transform.position = startingPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onPlatform = true;
        }
        else if (collision.gameObject.CompareTag("Spring"))
        {
            onSpring = true;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
        if (onPlatform || onSpring)
        {
            animator.SetBool("isJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onPlatform = false;
        }
        else if (collision.gameObject.CompareTag("Spring"))
        {
            onSpring = false;
        }
        if (!onPlatform && !onSpring)
        {
            animator.SetBool("isJumping", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            SpriteRenderer keySpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            BoxCollider2D keyCollider = collision.gameObject.GetComponent<BoxCollider2D>();
            keySpriteRenderer.enabled = false;
            keyCollider.enabled = false;
            keysCollected++;
            if (keysCollected >= 3)
            {
                GameObject spring = GameObject.FindGameObjectWithTag("Spring");
                SpriteRenderer springSpriteRenderer = spring.GetComponent<SpriteRenderer>();
                BoxCollider2D springCollider = spring.GetComponent<BoxCollider2D>();
                springSpriteRenderer.enabled = true;
                springCollider.enabled = true;
            }
        }
    }
}
