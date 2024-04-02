using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnergyBallController : MonoBehaviour
{
    [Header("EnergyBall")]
    public GameObject energyBallPrefab;
    public int energyBallAmount = 1;
    private int energyBallsRemaining;
    public float energyBallSpeed = 15f;

    private Vector2 facingDirection = Vector2.zero;

    private Mover mover;
    private PlayerInputHandler playerInputHandler;

    // [Header("Explosion")]
    // public Explosion explosionPrefab;
    // public LayerMask explosionLayerMask;
    // public float explosionDuration = 1f;
    // public int explosionRadius = 1;

    [SerializeField]
    private int playerIndex = 0;

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    private void Awake()
    {
        energyBallsRemaining = energyBallAmount;
        mover = GetComponent<Mover>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
    }

    public void SetEnergyBall()
    {
        if (energyBallsRemaining > 0)
        {
            ShootEnergyBall();
        }
    }

    private void ShootEnergyBall()
    {   
        facingDirection = mover.GetFacingDirection();
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Vector2 spawnPosition = position + facingDirection;

        GameObject energyBall = Instantiate(energyBallPrefab, spawnPosition, Quaternion.identity);

        float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
        energyBall.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Rigidbody2D energyBallRb = energyBall.GetComponent<Rigidbody2D>();
        energyBallRb.velocity = facingDirection * energyBallSpeed;
        energyBallsRemaining--;

        // Destroy(energyBall);
        energyBallsRemaining++;
    }

    // private void Explode(Vector2 position, Vector2 direction, int length)
    // {
    //     if (length <= 0)
    //     {
    //         return;
    //     }

    //     position += direction;


    //     if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
    //     {
    //         ClearDestructible(position);
    //         return;
    //     }


    //     Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
    //     explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
    //     explosion.SetDirection(direction);
    //     explosion.DestroyAfter(explosionDuration);

    //     Explode(position, direction, length - 1);
    // }


    private IEnumerator SlowPlayerMovement(Mover mover, float duration, float slowFactor)
    {
        // Store the original movement speed of the player
        float originalSpeed = mover.moveSpeed;

        // Reduce the movement speed of the player
        mover.moveSpeed *= slowFactor;

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Restore the original movement speed of the player
        mover.moveSpeed = originalSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("EnergyBall"))
        {
            Mover hitPlayerMover = mover.GetComponent<Mover>();
            int hitPlayerIndex = hitPlayerMover.GetPlayerIndex();
            Debug.Log("Hit player index: " + hitPlayerIndex);

            StartCoroutine(SlowPlayerMovement(hitPlayerMover, 10f, 0.5f));

            Destroy(other.gameObject);
        }
    }

    public void AddenergyBall()
    {
        energyBallsRemaining++;
        energyBallAmount++;
    }
}
