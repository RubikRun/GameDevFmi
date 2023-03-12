using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 6f;

    [SerializeField]
    private float jumpPower = 6f;

    private Rigidbody2D rigidBody;

    private int keysCollected = 0;

    private bool onSpring = false;
    private bool onPlatform = false;

    private Vector3 startingPosition;

    [SerializeField]
    private Vector2 mapBorderMin = new Vector2(-10f, -10f);

    [SerializeField]
    private Vector2 mapBorderMax = new Vector2(55f, 15f);

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(horizontalMove * speed, rigidBody.velocity.y);

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
            transform.position = startingPosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onPlatform = true;
        }
        if (collision.gameObject.CompareTag("Spring"))
        {
            onSpring = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onPlatform = false;
        }
        if (collision.gameObject.CompareTag("Spring"))
        {
            onSpring = false;
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
