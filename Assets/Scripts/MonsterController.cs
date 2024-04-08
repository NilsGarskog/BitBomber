using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float baseMoveSpeed = 2.5f; // Default movement speed for NPC
    public float detectedMoveSpeed = 5f;
    public float detectionRange = 5f; // Units in a circle around the NPC where it detects players
    public float reverseDuration = 2f;  // Reset time where the NPC reverse its velocity 
                                        // after colliding with a player
    public float obstacleDetectionRange = 1f; // Distance to check for obstacles ahead

    public float minX = -6f; // Tile map axis boundaries, update if tile map size is changed 
    public float maxX = 6f;  // from default
    public float minY = -5f;
    public float maxY = 5f;

    private GameObject[] players;
    private Transform target;
    private Vector2 randomTarget;
    private bool isFollowingPlayer = false;
    private bool isReversing = false;
    private Vector2 originalDirection;
    private float reverseTimer = 0f;

    private float currentMoveSpeed;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        randomTarget = GetRandomPositionWithinBounds();
        currentMoveSpeed = baseMoveSpeed;
    }

    void Update() // Updates distances to players and calls for moving 
                  // towards player and avoid getting stuck
    {
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

        if (target != null && closestDistance <= detectionRange)
        {
            isFollowingPlayer = true;
        }
        else
        {
            isFollowingPlayer = false;
            // Additional logic to prevent getting stuck
            AvoidGettingStuck();
            MoveTowards(randomTarget);
        }

        if (isFollowingPlayer && !isReversing)
        {
            currentMoveSpeed = Mathf.Lerp(currentMoveSpeed, detectedMoveSpeed, Time.deltaTime);
            MoveTowards(target.position);
        }
        else
        {
            currentMoveSpeed = baseMoveSpeed;
        }

        if (isReversing)
        {
            reverseTimer += Time.deltaTime;
            if (reverseTimer >= reverseDuration)
            {
                isReversing = false;
                reverseTimer = 0f;
                target = FindClosestPlayer();
            }
            else // reverse velocity isReversing is true, i.e. collision with player has happened
            {
                transform.Translate(-originalDirection * currentMoveSpeed * Time.deltaTime);
            }
        }
    }

    void MoveTowards(Vector2 targetPosition)
    {
        Vector2 moveDirection = (targetPosition - (Vector2)transform.position).normalized;
        originalDirection = moveDirection;
        transform.Translate(moveDirection * currentMoveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f && !isFollowingPlayer)
        {
            randomTarget = GetRandomPositionWithinBounds();
        }
    }

    Vector2 GetRandomPositionWithinBounds()
    {
        // Floats based on the size of the tile map, has to be updated if map size
        // is changed from default size.
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector2(randomX, randomY);
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
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Monster collided with player.");
            isReversing = true;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            randomTarget = GetRandomPositionWithinBounds();
            Debug.Log("Monster collided with wall, changing direction.");
        }
    }

    void AvoidGettingStuck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, originalDirection, obstacleDetectionRange);
        if (hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            // Change direction if an obstacle is directly ahead
            randomTarget = GetRandomPositionWithinBounds();
        }
    }
}
