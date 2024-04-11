using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNPCMover : MonoBehaviour
{
    public float speed = 2f; // Movement speed
    public float minY; // Minimum Y coordinate
    public float maxY; // Maximum Y coordinate

    private bool movingUp = false; // Initially moving down
    private Mover mover;
    private float initialX; // Initial X coordinate

    void Awake()
    {
        mover = GetComponent<Mover>(); // Get the Mover component
        initialX = transform.position.x; // Store the initial X position
        StartCoroutine(CheckPosition()); // Start the coroutine
    }

    void Update()
    {
        speed = mover.moveSpeed; // Update speed from Mover component

        // Move the NPC up or down based on the direction
        transform.Translate(Vector2.up * speed * Time.deltaTime * (movingUp ? 1 : -1));

        // Check if the NPC reaches the boundaries, then change direction
        if (transform.position.y >= maxY)
        {
            movingUp = false;
        }
        else if (transform.position.y <= minY)
        {
            movingUp = true;
        }

        // Control sprite rendering based on movement direction
        if (movingUp)
        {
            mover.SetDirection(Vector2.up, mover.spriteRendererUp);
        }
        else
        {
            mover.SetDirection(Vector2.down, mover.spriteRendererDown);
        }
    }

    IEnumerator CheckPosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(5); // Wait for 5 seconds

            // If the X position has changed, reset it to the initial position
            if (transform.position.x != initialX)
            {
                transform.position = new Vector3(initialX, transform.position.y, transform.position.z);

                // Reset the NPC's velocity to only be upwards or downwards
                Rigidbody2D rb = mover.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
            }
        }
    }
}
