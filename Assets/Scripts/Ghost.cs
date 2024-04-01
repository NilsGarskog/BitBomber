using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float ghostDelay;
    private float ghostDelaySeconds;
    public GameObject ghost;
    public SpriteRenderer activeSpriteRenderer;
    public bool makeGhost = false;
    private Mover mover;


    // Start is called before the first frame update
    void Start()
    {
        ghostDelaySeconds = ghostDelay;
        mover = GetComponent<Mover>();

    }

    // Update is called once per frame
    void Update()
    {
        if (makeGhost == true)
        {


            if (ghostDelaySeconds > 0)
            {
                ghostDelaySeconds -= Time.deltaTime;
            }

            else
            {
                Vector3 instantiationPosition = transform.position + new Vector3(0.096f, 0.426f, 1);
                GameObject newGhost = Instantiate(ghost, instantiationPosition, transform.rotation);
                SpriteRenderer ghostRenderer = newGhost.GetComponent<SpriteRenderer>();
                Destroy(newGhost, 1f);
                if (ghostRenderer != null)
                {
                    ghostRenderer.sprite = activeSpriteRenderer.sprite;
                }
                ghostDelaySeconds = ghostDelay;

            }
        }
    }
}
