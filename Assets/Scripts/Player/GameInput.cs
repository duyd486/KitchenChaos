using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{

    public static GameInput Instance { get; private set; }

    const string PLAYER_PREFS_BINDING = "InputBinding";

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        InteractAlter,
        Pause
    }

    private PlayerInputAction playerInputAction;

    private void Awake() {
        Instance = this;

        playerInputAction = new PlayerInputAction();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDING))
        {
            playerInputAction.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDING));
        }

        playerInputAction.Player.Enable();

        playerInputAction.Player.Interact.performed += Interact_performed;
        playerInputAction.Player.InteratAlternate.performed += InteratAlternate_performed;
        playerInputAction.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        playerInputAction.Player.Interact.performed -= Interact_performed;
        playerInputAction.Player.InteratAlternate.performed -= InteratAlternate_performed;
        playerInputAction.Player.Pause.performed -= Pause_performed;

        playerInputAction.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteratAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();


        inputVector = inputVector.normalized;

        return inputVector;

    }

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Move_Up:
                return playerInputAction.Player.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return playerInputAction.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return playerInputAction.Player.Move.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return playerInputAction.Player.Move.bindings[4].ToDisplayString();

            case Binding.Interact:
                return playerInputAction.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlter:
                return playerInputAction.Player.InteratAlternate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return playerInputAction.Player.Pause.bindings[0].ToDisplayString();
        }
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        playerInputAction.Player.Disable();

        InputAction inputAction;
        int index;

        switch (binding)
        {
            default:
            case Binding.Move_Up:
                inputAction = playerInputAction.Player.Move;
                index = 1;
                break;
            case Binding.Move_Down:
                inputAction = playerInputAction.Player.Move;
                index = 2;
                break;
            case Binding.Move_Left:
                inputAction = playerInputAction.Player.Move;
                index = 3;
                break;
            case Binding.Move_Right:
                inputAction = playerInputAction.Player.Move;
                index = 4;
                break;
            case Binding.Interact:
                inputAction = playerInputAction.Player.Interact;
                index = 0;
                break;
            case Binding.InteractAlter:
                inputAction = playerInputAction.Player.InteratAlternate;
                index = 0;
                break;
            case Binding.Pause:
                inputAction = playerInputAction.Player.Pause;
                index = 0;
                break;

        }

        inputAction.PerformInteractiveRebinding(index)
            .OnComplete(callback =>
            {
                callback.Dispose();
                playerInputAction.Player.Enable();
                onActionRebound();

                PlayerPrefs.SetString(PLAYER_PREFS_BINDING, playerInputAction.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
            })
            .Start();
    }
}
