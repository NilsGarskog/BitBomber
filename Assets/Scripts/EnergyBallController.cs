using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnergyBallController : MonoBehaviour
{
    [Header("EnergyBall")]
    public GameObject energyBallPrefab;
    public int energyBallAmount = 0;
    public float energyBallSpeed = 15f;
    public GameObject slowedEffectPrefab;

    private Vector2 facingDirection = Vector2.zero;
    
    private Mover mover;
    private PlayerInputHandler playerInputHandler;

    [SerializeField]
    private int playerIndex = 0;

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    private void Awake()
    {
        mover = GetComponent<Mover>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
    }

    public void SetEnergyBall()
    {
        if (energyBallAmount > 0)
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

        // ignore player that shot the energy ball
        Physics2D.IgnoreCollision(energyBall.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
        energyBall.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Rigidbody2D energyBallRb = energyBall.GetComponent<Rigidbody2D>();
        energyBallRb.velocity = facingDirection * energyBallSpeed;
        energyBallAmount--;

        // if nothing is hit destroy energy ball after 2 seconds
        StartCoroutine(DestroyEnergyBall(energyBall, 2f));

    }

    private IEnumerator DestroyEnergyBall(GameObject energyBall, float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(energyBall);
    }

    private IEnumerator SlowPlayerMovement(Mover mover, float duration, float slowFactor)
    {
        // Reduce the movement speed of the player
        mover.moveSpeed *= slowFactor;

        // Intantiate the slowed effeect sprite
        GameObject slowedEffect = Instantiate(slowedEffectPrefab, mover.transform.position, Quaternion.identity);

        // Keep the slowed effect sprite as a child of the player to make it follow
        slowedEffect.transform.parent = mover.transform;

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Destroy the slowed effect sprite
        Destroy(slowedEffect);

        // Restore the original movement speed of the player
        mover.moveSpeed /= slowFactor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("EnergyBall"))
        {
            Debug.Log("EnergyBall hit");
            Mover hitPlayerMover = mover.GetComponent<Mover>();
            int hitPlayerIndex = hitPlayerMover.GetPlayerIndex();
            // Debug.Log("Hit player index: " + hitPlayerIndex);

            StartCoroutine(SlowPlayerMovement(hitPlayerMover, 5f, 0.5f));

            Destroy(other.gameObject);
        }
    }

    public void AddEnergyBall()
    {
        energyBallAmount++;
    }
}
