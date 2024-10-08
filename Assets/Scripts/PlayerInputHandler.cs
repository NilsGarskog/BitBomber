using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{

    private PlayerInput playerInput;
    private Mover mover;
    private BombController bombController;
    private BowController bowController;
    private EnergyBallController energyBallController;
    private SkullController skullController;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var movers = FindObjectsOfType<Mover>();
        var bombControllers = FindObjectsOfType<BombController>();
        var bowControllers = FindObjectsOfType<BowController>();
        var energyBallControllers = FindObjectsOfType<EnergyBallController>();
        var skullControllers = FindObjectsOfType<SkullController>();
        var index = playerInput.playerIndex;
        mover = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);
        bombController = bombControllers.FirstOrDefault(b => b.GetPlayerIndex() == index);
        bowController = bowControllers.FirstOrDefault(b => b.GetPlayerIndex() == index);
        energyBallController = energyBallControllers.FirstOrDefault(b => b.GetPlayerIndex() == index);
        skullController = skullControllers.FirstOrDefault(b => b.GetPlayerIndex() == index);

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

    public void OnShootEnergyBall(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            energyBallController.SetEnergyBall();
        }
    }

    public void OnSkullDrop(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            skullController.SetSkullDrop();
        }
    }

}
