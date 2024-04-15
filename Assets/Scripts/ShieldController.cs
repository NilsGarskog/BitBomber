using System.Collections;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public GameObject shieldPrefab;
    public float baseShieldTime = 7f;
    public float currentShieldTime = 0f;
    private GameObject shieldInstance;
    private Coroutine shieldCoroutine;

    public void ActivateShield(GameObject player)
    {
        if (!player.GetComponent<Player>().isShielded)
        {
            AddShieldTime(player);
            CreateShield(player);

        }

        else
        {
            AddShieldTime(player);
        }

    }

    private void CreateShield(GameObject player)
    {
        // Instantiate shieldPrefab as a child of the player
        if (shieldInstance == null)
        {
            Transform playerTransform = player.transform;
            shieldInstance = Instantiate(shieldPrefab, playerTransform);
            // Optionally set the position and rotation of the shield relative to the player
            shieldInstance.transform.localPosition = Vector3.zero;
            shieldInstance.transform.localRotation = Quaternion.identity;

            // Set sorting order of the shield sprite to be higher than the player sprite
            SpriteRenderer playerRenderer = player.GetComponent<SpriteRenderer>();
            SpriteRenderer shieldRenderer = shieldInstance.GetComponent<SpriteRenderer>();
            shieldRenderer.sortingOrder = 6;

        }
        // Ensure the shield is active and visible
        shieldInstance.SetActive(true);
    }

    private void HideShield()
    {
        if (shieldInstance != null)
        {
            shieldInstance.SetActive(false);
        }
    }

    private void AddShieldTime(GameObject player)
    {
        if (shieldCoroutine != null)
        {
            StopCoroutine(shieldCoroutine);
        }
        currentShieldTime = baseShieldTime;
        shieldCoroutine = StartCoroutine(ShieldCountdown(player));
    }

    private IEnumerator ShieldCountdown(GameObject player)
    {
        Player playerComponent = player.GetComponent<Player>();
        playerComponent.isShielded = true;

        while (currentShieldTime > 0f)
        {
            // Update the shield time each frame
            currentShieldTime -= Time.deltaTime;

            // Optionally, update UI or display remaining shield time

            yield return null; // Wait until the next frame
        }

        playerComponent.isShielded = false;
        HideShield();
        currentShieldTime = 0f;
    }

    // Optional: Call this method to immediately deactivate the shield
    public void DeactivateShield()
    {
        if (shieldInstance != null)
        {
            HideShield();
        }
    }
}
