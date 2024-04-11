using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject moveCanvas;
    public GameObject bombCanvas;
    private BombController bombController;
    private Mover mover;
    private Vector3 position;
    private bool bombEvent;
    private bool hasEntered = false;


    private void Awake()
    {
        position = transform.position;
        moveCanvas.SetActive(true);
        bombCanvas.SetActive(false);
        bombController = GetComponent<BombController>();
        mover = GetComponent<Mover>();
        bombController.bombAmount = 0;
        bombController.bombsRemaining = 0;
    }

    private void Update()
    {
        if (transform.position != position)
        {
            moveCanvas.SetActive(false);
        }

        if(hasEntered == false && transform.position.x <= -3.8 && transform.position.x >= -4.5 && transform.position.y >= 4.5 && transform.position.y <= 5.5)
        {
            hasEntered = true;
            bombCanvas.SetActive(true);
            mover.enabled = false;
            bombController.bombAmount = 1;
            bombController.bombsRemaining = 1;
            bombEvent = false;
        }

        if(bombController.bombsRemaining == 0 && bombEvent == false)
        { 
            bombEvent = true;
            bombCanvas.SetActive(false);
            mover.enabled = true;
        }
    }
}
