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
    private EndGameTiles endGameTiles;
    private bool isDead = false;

    private Vector2 moveDirection = Vector2.zero;
    private Vector2 inputVector = Vector2.zero;

    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    public AnimatedSpriteRenderer spriteRendererDeath;
    public AnimatedSpriteRenderer activeSpriteRenderer;

    public AudioManagerMainScene audioManager;





    private void Awake()
    {

        rigidbody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
        endGameTiles = FindObjectOfType<EndGameTiles>();


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
        }
        else if (inputVector == Vector2.down)
        {
            SetDirection(Vector2.down, spriteRendererDown);
        }
        else if (inputVector == Vector2.left)
        {
            SetDirection(Vector2.left, spriteRendererLeft);
        }
        else if (inputVector == Vector2.right)
        {
            SetDirection(Vector2.right, spriteRendererRight);
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

    public void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
    {
        moveDirection = newDirection;

        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = newDirection == Vector2.zero;
    }

    public Vector2 GetFacingDirection()
    {
        if (activeSpriteRenderer == spriteRendererUp)
        {
            return Vector2.up;
        }
        else if (activeSpriteRenderer == spriteRendererDown)
        {
            return Vector2.down;
        }
        else if (activeSpriteRenderer == spriteRendererLeft)
        {
            return Vector2.left;
        }
        else if (activeSpriteRenderer == spriteRendererRight)
        {
            return Vector2.right;
        }
        else
        {
            return Vector2.down;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = GetComponent<Player>();

        if (other.gameObject.tag == "Death" && !isDead)
        {
            DeathSequence();
        }

        if ((other.gameObject.layer == LayerMask.NameToLayer("Explosion") || other.gameObject.layer == LayerMask.NameToLayer("Arrow") || other.gameObject.layer == LayerMask.NameToLayer("Skull")) && player.isShielded == false && !isDead)
        {


            if (player != null)

            {
                if (other.gameObject.layer == LayerMask.NameToLayer("Arrow"))
                {
                    if (other.gameObject.GetComponent<Arrow>().shooterIndex != playerIndex)
                    {
                        player.TakeDamage(50);
                        audioManager.arrowHit();
                    }
                    else
                    {
                        player.TakeDamage(0);
                    }
                }
                if (other.gameObject.layer == LayerMask.NameToLayer("Skull"))
                {
                    player.TakeDamage(50);
                    audioManager.skullHit();
                }

                if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
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

    private void DeathSequence()
    {
        if (gameObject.tag == "TutorialNPC")
        {
            Player player = GetComponent<Player>();
            player.TakeDamage(-100);
            GetComponent<TileController>().ExitTutorial();
        }
        else
        {
            isDead = true;
            endGameTiles.addDeath();
            enabled = false;
            GetComponent<BombController>().enabled = false;
            GetComponent<SkullController>().enabled = false;
            GetComponent<BowController>().enabled = false;
            GetComponent<ShieldController>().enabled = false;
            GetComponent<EnergyBallController>().enabled = false;

            spriteRendererDown.enabled = false;
            spriteRendererUp.enabled = false;
            spriteRendererLeft.enabled = false;
            spriteRendererRight.enabled = false;
            spriteRendererDeath.enabled = true;

            Invoke(nameof(OnDeathSequenceEnded), 1.25f);
        }
    }

    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
        //FindObjectOfType<GameManager>().setDeathIndex(playerIndex);
        FindObjectOfType<GameManager>().HandlePlayerDeath(playerIndex);
    }
}
