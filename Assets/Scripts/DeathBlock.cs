using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBlock : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        StartCoroutine(makeNormalBlock());
    }

    IEnumerator makeNormalBlock()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.tag = "normal";
        Collider2D collider = gameObject.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = false;
        }
    }
}
