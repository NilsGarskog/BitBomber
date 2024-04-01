using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 5f;

    [SerializeField]
    private int playerIndex = 0;

    //public Rigidbody2D rigidbody { get; private set; }
    private Rigidbody2D rigidbody;

    private Vector2 moveDirection = Vector2.zero;
    private Vector2 inputVector = Vector2.zero;

    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    public AnimatedSpriteRenderer spriteRendererDeath;
    private AnimatedSpriteRenderer activeSpriteRenderer;

    public Ghost ghost;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
    }

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    public void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
    }

    void Update()
    {
        if (inputVector == Vector2.up)
        {
            SetDirection(Vector2.up, spriteRendererUp);
            ghost.makeGhost = true;
        }
        else if (inputVector == Vector2.down)
        {
            SetDirection(Vector2.down, spriteRendererDown);
            ghost.makeGhost = true;
        }
        else if (inputVector == Vector2.left)
        {
            SetDirection(Vector2.left, spriteRendererLeft);
            ghost.makeGhost = true;
        }
        else if (inputVector == Vector2.right)
        {
            SetDirection(Vector2.right, spriteRendererRight);
            ghost.makeGhost = true;
        }
        else
        {
            SetDirection(Vector2.zero, activeSpriteRenderer);
            ghost.makeGhost = false;
        }

        /*
        moveDirection = new Vector2(inputVector.x, inputVector.y);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;

        controller.Move(moveDirection * Time.deltaTime);
        */
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = moveDirection * moveSpeed * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
    }

    private void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
    {
        moveDirection = newDirection;

        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = newDirection == Vector2.zero;
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();


        foreach (SpriteRenderer sr in spriteRenderers)
        {
            if (sr.enabled)
            {

                ghost.activeSpriteRenderer = sr;
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            DeathSequence();
        }
    }

    private void DeathSequence()
    {
        enabled = false;
        GetComponent<BombController>().enabled = false;

        spriteRendererDown.enabled = false;
        spriteRendererUp.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;
        spriteRendererDeath.enabled = true;

        Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }

    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().CheckWinState();
    }
}
