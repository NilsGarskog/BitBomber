using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemTimer : MonoBehaviour
{
    public bool timer = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SwitchTimer());
    }

    private IEnumerator SwitchTimer()
    {
        yield return new WaitForSeconds(20);
        timer = true;
    }
}
