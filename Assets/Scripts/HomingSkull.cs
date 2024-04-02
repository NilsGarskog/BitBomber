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
        Transform furthestPlayer = null;
        float maxDistance = 0; // Keep track of the furthest distance found

        foreach (GameObject player in players)
        {
            // Calculate the distance from the missile to the current player
            float distance = Vector3.Distance(transform.position, player.transform.position);

            // If this player is further away than the current maxDistance, update furthestPlayer and maxDistance
            if (distance > maxDistance)
            {
                furthestPlayer = player.transform;
                maxDistance = distance;
            }
        }

        // Assign the furthest player as the target
        target = furthestPlayer;

        if (target == null)
        {
            Debug.LogWarning("HomingSkull: No player found.");
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 skullDirection = (Vector2)target.position-rb.position;

        skullDirection.Normalize();
        float rotateAmount = Vector3.Cross(skullDirection, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * skullSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject); // Destroy the missile
        Instantiate(chewingEffect, transform.position, transform.rotation);
    }
    
}
