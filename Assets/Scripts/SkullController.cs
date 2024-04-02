using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullController : MonoBehaviour
{
    [Header("Skull")]
    public GameObject skullPrefab;
    public int skullsRemaining = 0;

    [SerializeField]
    private int playerIndex = 0;

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    private void Awake()
    {
        // skullsRemaining = 0;
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
        skullsRemaining--;
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
