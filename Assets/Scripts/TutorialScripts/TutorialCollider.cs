using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCollider : MonoBehaviour
{
    private Mover mover;
    public GameObject slowedEffectPrefab;

    private void Awake()
    {
        mover = GetComponent<Mover>();
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
}
