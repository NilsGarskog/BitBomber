using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class SwimPlayerInputHandler : MonoBehaviour
{

    private PlayerInput playerInput;
    private SwimMover mover;
    private BombController bombController;
    private BowController bowController;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        /*
        Debug.Log("PlayerInputHandler Awake");
        Debug.Log(playerInput);
        Debug.Log(playerInput.playerIndex);
        */
        var movers = FindObjectsOfType<SwimMover>();
        var bombControllers = FindObjectsOfType<BombController>();
        var bowControllers = FindObjectsOfType<BowController>();
        var index = playerInput.playerIndex;
        mover = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);
        bombController = bombControllers.FirstOrDefault(b => b.GetPlayerIndex() == index);
        bowController = bowControllers.FirstOrDefault(b => b.GetPlayerIndex() == index);
    }

    public void OnMove(CallbackContext context)
    {
        mover.SetInputVector(context.ReadValue<Vector2>());
    }

    public void OnDropBomb(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            bombController.SetBombDrop();
        }
    }

    public void OnShootArrow(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            bowController.SetArrowShot();
        }
    }
}
