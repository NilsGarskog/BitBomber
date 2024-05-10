using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public GameObject moveCanvas;
    public GameObject bombCanvas;
    public GameObject brickDestroyHelpCanvas;
    public GameObject effectHelpCanvas;
    public GameObject playerUI;
    private BombController bombController;
    private Mover mover;
    private Vector3 position;
    private bool bombEvent;
    private bool hasEntered1 = false;
    private bool destroyBrickHelp = false;
    private bool effectHelp = false;
    private bool activateUI = false;

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

        if (hasEntered1 == false && transform.position.x <= -3.8 && transform.position.x >= -4.5 && transform.position.y >= 4.5 && transform.position.y <= 5.5)
        {
            hasEntered1 = true;
            bombCanvas.SetActive(true);
            mover.enabled = false;
            bombController.bombAmount = 1;
            bombController.bombsRemaining = 1;
            bombEvent = false;
        }
        if (transform.position.x <= 29.5 && transform.position.x >= 28.3 && transform.position.y >= -6.5 && transform.position.y <= -5.5)
        {
            SceneManager.LoadScene("StartScreen");
        }

        if (bombController.bombsRemaining == 0 && bombEvent == false)
        {
            bombEvent = true;
            bombCanvas.SetActive(false);
            mover.enabled = true;
        }
        if (destroyBrickHelp == false && transform.position.x >= -2.5)
        {
            destroyBrickHelp = true;
            brickDestroyHelpCanvas.SetActive(true);
        }

        if (transform.position.x >= 3.5)
        {
            brickDestroyHelpCanvas.SetActive(false);
        }

        if (effectHelp == false && transform.position.x >= 3.5)
        {
            effectHelpCanvas.SetActive(true);
            effectHelp = true;
        }

        if (transform.position.x >= 3.5 && activateUI == false)
        {
            activateUI = true;
            playerUI.SetActive(true);
        }
    }
}
