using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float baseMoveSpeed = 2.5f; // Base speed of the monster's movement
    public float detectionRange = 5f; // Distance at which the monster detects the player
    public float reverseDuration = 2f; // Duration to reverse velocity after colliding with the player

    private GameObject[] players; // Array of all players in the scene
    private Transform target; // Current target the monster is following
    private Vector2 randomTarget; // Random target position for the monster to move towards
    private bool isFollowingPlayer = false; // Flag to indicate if the monster is following the player
    private bool isReversing = false; // Flag to indicate if the monster is currently reversing velocity
    private Vector2 originalDirection; // Original direction before reversing velocity
    private float reverseTimer = 0f; // Timer for reversing velocity

    private float currentMoveSpeed; // Current speed of the monster's movement

    void Start()
    {
        // Find all players in the scene
        players = GameObject.FindGameObjectsWithTag("Player");

        // Set an initial random target position for the monster to move towards
        randomTarget = GetRandomPosition();

        // Set the initial move speed
        currentMoveSpeed = baseMoveSpeed;
    }

    void Update()
    {
        // Find the closest player within detection range
        float closestDistance = float.MaxValue;
        foreach (GameObject player in players)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= detectionRange && distanceToPlayer < closestDistance)
            {
                closestDistance = distanceToPlayer;
                target = player.transform;
            }
        }

        // Check if the player is within detection range and select it as the target
        if (target != null && closestDistance <= detectionRange)
        {
            isFollowingPlayer = true;
        }
        else
        {
            isFollowingPlayer = false;
            // If the monster is not following the player, move towards a random target
            MoveTowards(randomTarget);
        }

        // If the monster is following the player, move towards the player
        if (isFollowingPlayer && !isReversing)
        {
            // Increase speed exponentially up to 200% extra velocity
            currentMoveSpeed = Mathf.Lerp(currentMoveSpeed, baseMoveSpeed * 2f, Time.deltaTime);
            MoveTowards(target.position);
        }
        else
        {
            // Reset speed to the base speed when not following a player
            currentMoveSpeed = baseMoveSpeed;
        }

        // Reverse velocity when colliding with the player
        if (isReversing)
        {
            reverseTimer += Time.deltaTime;
            if (reverseTimer >= reverseDuration)
            {
                // Stop reversing after the specified duration
                isReversing = false;
                reverseTimer = 0f;
                // Set the closest player as the new target after reversing
                target = FindClosestPlayer();
            }
            else
            {
                // Reverse velocity
                transform.Translate(-originalDirection * currentMoveSpeed * Time.deltaTime);
            }
        }
    }

    void MoveTowards(Vector2 targetPosition)
    {
        // Calculate the direction towards the target position
        Vector2 moveDirection = (targetPosition - (Vector2)transform.position).normalized;

        // Store the original direction before reversing velocity
        originalDirection = moveDirection;

        // Move the monster towards the target position
        transform.Translate(moveDirection * currentMoveSpeed * Time.deltaTime);

        // If the monster is very close to the target position, set a new random target
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            if (!isFollowingPlayer)
            {
                randomTarget = GetRandomPosition();
            }
        }
    }

    Vector2 GetRandomPosition()
    {
        // Generate a random position within a certain range around the monster
        Vector2 randomPosition = (Vector2)transform.position + new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        return randomPosition;
    }

    Transform FindClosestPlayer()
    {
        Transform closestPlayer = null;
        float closestDistance = float.MaxValue;
        foreach (GameObject player in players)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < closestDistance)
            {
                closestDistance = distanceToPlayer;
                closestPlayer = player.transform;
            }
        }
        return closestPlayer;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Debug log statement indicating collision with a player
            Debug.Log("Monster collided with player.");

            // Reverse velocity for the specified duration
            isReversing = true;
        }
    }
}
