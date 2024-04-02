using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using UnityEditor.Experimental.GraphView;

public class PlayerInputHandler : MonoBehaviour
{

    private PlayerInput playerInput;
    private Mover mover;
    private BombController bombController;
    private EnergyBallController energyBallController;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        /*
        Debug.Log("PlayerInputHandler Awake");
        Debug.Log(playerInput);
        Debug.Log(playerInput.playerIndex);
        */
        var movers = FindObjectsOfType<Mover>();
        var bombControllers = FindObjectsOfType<BombController>();
        var index = playerInput.playerIndex;
        var energyBallControllers = FindObjectsOfType<EnergyBallController>();
        mover = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);
        bombController = bombControllers.FirstOrDefault(b => b.GetPlayerIndex() == index);
        energyBallController = energyBallControllers.FirstOrDefault(e => e.GetPlayerIndex() == index);
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

    public void OnShootEnergyBall(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            energyBallController.SetEnergyBall();
        }
    }
}
