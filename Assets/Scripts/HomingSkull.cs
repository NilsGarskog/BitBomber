using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingSkull : MonoBehaviour
{
    public Transform target;

    public float skullSpeed = 5f;
    public float rotateSpeed = 200f;

    public GameObject chewingEffect;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Find all players in the scene
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Transform closestPlayer = null;
        Transform secondClosestPlayer = null;
        float closestDistance = float.MaxValue;
        float secondClosestDistance = float.MaxValue;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance < closestDistance)
            {
                // Update the second closest before updating the closest
                secondClosestPlayer = closestPlayer;
                secondClosestDistance = closestDistance;

                // Update the closest
                closestPlayer = player.transform;
                closestDistance = distance;
            }
            else if (distance < secondClosestDistance)
            {
                // Update the second closest
                secondClosestPlayer = player.transform;
                secondClosestDistance = distance;
            }
        }
        // Assign the second closest player as the target
        target = secondClosestPlayer;

        if (target == null)
        {
            target = closestPlayer; // Fallback to the closest player if there's only one player
        }
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 directionToTarget = (Vector2)target.position - rb.position;

            directionToTarget.Normalize();
            float rotateAmount = Vector3.Cross(directionToTarget, transform.up).z;

            rb.angularVelocity = -rotateAmount * rotateSpeed;
            rb.velocity = transform.up * skullSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // Ensure it's the target object
        {
            // Calculate the midpoint for the effect's instantiation
            Vector2 effectPosition = other.transform.position;
            effectPosition.y += 0.3f;
            Debug.Log(other.transform.position);
            Debug.Log(effectPosition);
            
            // Instantiate the chewingEffect at the midpoint
            GameObject effect = Instantiate(chewingEffect, effectPosition, Quaternion.identity);

            // Destroy the effect after 1 second
            Destroy(effect, 1f); // 1f represents the delay in seconds before the GameObject is destroyed.
            
            // Destroy the skull
            Destroy(gameObject);
        }
    }
}
