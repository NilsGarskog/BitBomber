using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    //public bool start = false;
    public float duration = 1f;

    // Update is called once per frame
    /*
    void Update()
    {
        if (start)
        {
            StartCoroutine(Shaking());
            start = false;
        }
    }
    */

    public void StartShake()
    {
        StartCoroutine(Shaking());

    }

    IEnumerator Shaking()
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = startPos + Random.insideUnitSphere * 10f;
            yield return null;
        }

        transform.position = startPos;
    }
}
