using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb = null;
    public int shooterIndex = 0;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Mover>().GetPlayerIndex() != shooterIndex)
            {
                Destroy(gameObject);
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("bomb") || other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            //do nothing
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
