using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingPlatform : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rigidBody.isKinematic = false;
        }
    }
}
