using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SkullController : MonoBehaviour
{
    public AudioClip chewingSound;
    [Header("Skull")]
    public GameObject skullPrefab;
    public int skullsRemaining = 3;

    [Header("Settings")]
    public float destructionDelay = 7f; // Time before the skull is destroyed
    public float collisionEnableDelay = 1f; // Time before enabling collision

    [SerializeField]
    private int playerIndex = 0;

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    private void Awake()
    {
        // Initialization can be done here if needed
    }

    public void SetSkullDrop()
    {
        if (skullsRemaining > 0)
        {
            PlaceSkull();
        }
    }

    private void PlaceSkull()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y) + 0.5f;

        GameObject skull = Instantiate(skullPrefab, position, Quaternion.identity);
        // Initially disable the collider
        Collider2D collider = skull.GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false;

        skullsRemaining--;

        // Start coroutines for enabling collider and destroying the skull
        StartCoroutine(EnableCollisionAfterDelay(skull, collisionEnableDelay));
        StartCoroutine(DestroyAfterDelay(skull, destructionDelay));
    }

    private IEnumerator DestroyAfterDelay(GameObject objectToDestroy, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(objectToDestroy);
    }

    private IEnumerator EnableCollisionAfterDelay(GameObject objectToEnable, float delay)
    {
        yield return new WaitForSeconds(delay);
            Collider2D collider = objectToEnable.GetComponent<Collider2D>();
        if (collider != null) collider.enabled = true;
        if (chewingSound != null)
        {
            AudioSource.PlayClipAtPoint(chewingSound, objectToEnable.transform.position);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Skull"))
        {
            other.isTrigger = false;
        }
    }


    public void AddSkull()
    {
        Debug.Log("Picked up skull. Skulls remaining: " + skullsRemaining);
        skullsRemaining++;
    }
}
