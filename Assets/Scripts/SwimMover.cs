using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimMover : MonoBehaviour
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
    public AnimatedSpriteRenderer activeSpriteRenderer;



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
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (inputVector == Vector2.down)
        {
            SetDirection(Vector2.down, spriteRendererDown);
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (inputVector == Vector2.left)
        {
            SetDirection(Vector2.left, spriteRendererLeft);
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (inputVector == Vector2.right)
        {
            SetDirection(Vector2.right, spriteRendererRight);
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else
        {
            SetDirection(Vector2.zero, activeSpriteRenderer);
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion") || other.gameObject.layer == LayerMask.NameToLayer("Arrow"))
        {
            Player player = GetComponent<Player>();

            if (player != null)

            {
                if (player.currentHealth <= 0)
                {
                    DeathSequence();
                }
                else
                {
                    if (other.gameObject.layer == LayerMask.NameToLayer("Arrow"))
                    {
                        if (other.gameObject.GetComponent<Arrow>().shooterIndex != playerIndex)
                        {
                            player.TakeDamage(50);
                        }
                        else
                        {
                            player.TakeDamage(0);
                        }
                    }
                    else
                    {
                        player.TakeDamage(20);
                    }
                    if (player.currentHealth <= 0)
                    {
                        DeathSequence();
                    }
                }
            }
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