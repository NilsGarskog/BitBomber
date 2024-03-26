using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BowController : MonoBehaviour
{


    private AnimatedSpriteRenderer activeSpriteRenderer;
    private Mover mover;

    [Header("Bow")]
    public GameObject arrowPrefab;
    public float arrowSpeed = 15f;
    public int arrowAmount = 1;
    public int arrowsRemaining;

    [SerializeField]
    private int playerIndex = 0;

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    private void Awake()
    {
        arrowsRemaining = arrowAmount;
        mover = GetComponent<Mover>();

    }

    public void SetArrowShot()
    {
        if (arrowsRemaining > 0)
        {
            StartCoroutine(ShootArrow());
        }
    }

    private IEnumerator ShootArrow()
    {
        Vector2 position = transform.position;

        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Vector2 direction = findDirection();

        position += direction;

        float angle = Mathf.Atan2(direction.y, direction.x);
        quaternion rotation = arrowPrefab.transform.rotation * Quaternion.AngleAxis((angle) * Mathf.Rad2Deg, Vector3.forward);

        //arrowPrefab.transform.rotation
        GameObject arrow = Instantiate(arrowPrefab, position, rotation);
        arrowsRemaining--;

        yield return new WaitForSeconds(0.1f);

        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.velocity = direction * arrowSpeed;
    }

    private Vector2 findDirection()
    {
        activeSpriteRenderer = mover.activeSpriteRenderer;
        if (activeSpriteRenderer == mover.spriteRendererUp)
        {
            return Vector2.up;
        }
        else if (activeSpriteRenderer == mover.spriteRendererDown)
        {
            return Vector2.down;
        }
        else if (activeSpriteRenderer == mover.spriteRendererLeft)
        {
            return Vector2.left;
        }
        else if (activeSpriteRenderer == mover.spriteRendererRight)
        {
            return Vector2.right;
        }
        else
        {
            return Vector2.zero;
        }
    }

}
