using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private float baseShieldTime = 7f;
    private float currentShieldTime = 0f;
    public enum ItemType
    {
        Extrabomb,
        BlastRadius,
        SpeedIncrease,
        ExtraSkull, 
        EnergyBall,
        ExtraArrow,
        ColdShield,
    }
    private int coldShieldOrderInLayer = 6;
    public ItemType Type;
    private Coroutine shieldCoroutine;
    private void ActivateShield(GameObject player)
    {
        if(shieldCoroutine != null)
        {
            StopCoroutine(shieldCoroutine);
        }
        currentShieldTime += baseShieldTime;
        shieldCoroutine = StartCoroutine(ShieldCountdown(player));
    }

    private IEnumerator ShieldCountdown(GameObject player)
    {
        player.GetComponent<Mover>().isShielded = true;
        yield return new WaitForSeconds(currentShieldTime);
        player.GetComponent<Mover>().isShielded = false;
        currentShieldTime = 0f;
    }
    private void onItemPickup(GameObject player)
    {
        switch (Type)
        {

             case ItemType.Extrabomb:
                player.GetComponent<BombController>().AddBomb();
                Destroy(gameObject);
                break;

            case ItemType.BlastRadius:
                player.GetComponent<BombController>().explosionRadius += 1;
                Destroy(gameObject);
                break;
            case ItemType.SpeedIncrease:
                player.GetComponent<Mover>().moveSpeed += 1;
                Destroy(gameObject);
                break;
            case ItemType.ExtraArrow:
                player.GetComponent<BowController>().arrowsRemaining += 1;
                Destroy(gameObject);
                break;
            case ItemType.EnergyBall:
                player.GetComponent<EnergyBallController>().AddEnergyBall();
                Destroy(gameObject);
                break;
            case ItemType.ExtraSkull:
                player.GetComponent<SkullController>().AddSkull();
                Destroy(gameObject);
                break;


            case ItemType.ColdShield:
                if(player.GetComponent<Mover>().isShielded == false)
                {
                ActivateShield(player);
                StartCoroutine(DestroyAfterDelay(gameObject));
                Transform playerTransform = player.transform;
                transform.SetParent(playerTransform);
                SpriteRenderer itemRenderer = GetComponent<SpriteRenderer>();
                if(itemRenderer != null)
                {
                    itemRenderer.sortingOrder = coldShieldOrderInLayer;
                }
                }
                break;
        }
        
    }
    private void Update()
    {
        if(Type == ItemType.ColdShield && transform.parent != null)
        {
            transform.position = transform.parent.position;
        }
    }

    private IEnumerator DestroyAfterDelay(GameObject obj)
    {
        yield return new WaitForSeconds(currentShieldTime);
        Destroy(obj);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onItemPickup(other.gameObject);
        }
    }
}
