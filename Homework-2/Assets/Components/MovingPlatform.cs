using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private bool horizontal = true;

    [SerializeField]
    private float radius = 1.5f;

    [SerializeField]
    private float speed = 3f;

    private Rigidbody2D rigidBody;

    private Vector3 initialPosition;

    private bool movingPositive = true;

    private int lastTimeChangeDirection = 99999;
    private int minTimeChangeDirection = 15;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        Vector2 distance = transform.position - initialPosition;

        if (horizontal)
        {
            if (movingPositive)
            {
                rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
            }
            else
            {
                rigidBody.velocity = new Vector2(-speed, rigidBody.velocity.y);
            }
            if (Mathf.Abs(distance.x) > radius && lastTimeChangeDirection >= minTimeChangeDirection)
            {
                movingPositive = !movingPositive;
                lastTimeChangeDirection = 0;
            }
        }
        else
        {
            if (movingPositive)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, speed);
            }
            else
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, -speed);
            }
            if (Mathf.Abs(distance.y) > radius && lastTimeChangeDirection >= minTimeChangeDirection)
            {
                movingPositive = !movingPositive;
                lastTimeChangeDirection = 0;
            }
        }

        lastTimeChangeDirection++;
    }
}
