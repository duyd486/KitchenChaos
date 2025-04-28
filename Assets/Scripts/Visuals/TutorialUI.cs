using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI keyMoveDownText;
    [SerializeField] private TextMeshProUGUI keyMoveRightText;
    [SerializeField] private TextMeshProUGUI keyInteractText;
    [SerializeField] private TextMeshProUGUI keyInteractAltText;
    [SerializeField] private TextMeshProUGUI keyPauseText;
    [SerializeField] private TextMeshProUGUI keyGamepadInteractText;
    [SerializeField] private TextMeshProUGUI keyGamepadInteractAltText;
    [SerializeField] private TextMeshProUGUI keyGamepadPauseText;


    private void Start()
    {
        GameInput.Instance.OnBidingRebind += GameInput_OnBidingRebind;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        UpdateVisual();
        Show();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStart())
        {
            Hide();
        }
    }

    private void GameInput_OnBidingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        keyInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlter);
        keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        keyGamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        keyGamepadInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlter);
        keyGamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);

    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
