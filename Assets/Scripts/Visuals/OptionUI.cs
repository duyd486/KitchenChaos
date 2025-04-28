using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public static OptionUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;

    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlterButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private Button gamepadInteractAlterButton;
    [SerializeField] private Button gamepadPauseButton;

    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;

    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAlterText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAlterText;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;

    [SerializeField] private Transform pressToRebindTransform;

    private Action onCloseButtonAction;


    private void Awake()
    {
        Instance = this;

        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() =>
        {
            Hide();
            onCloseButtonAction();
        });

        moveUpButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Up); });
        moveDownButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Down); });
        moveLeftButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Left); });
        moveRightButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Right); });
        interactButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact); });
        interactAlterButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.InteractAlter); });
        pauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Pause); });
        gamepadInteractButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Gamepad_Interact); });
        gamepadInteractAlterButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Gamepad_InteractAlter); });
        gamepadPauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Gamepad_Pause); });

    }
    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

        UpdateVisual();
        HideRebindKey();
        Hide();
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectText.text = "SOUND EFFECT: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10);
        musicText.text = "MUSIC: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10);

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAlterText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlter);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        gamepadInteractAlterText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlter);
        gamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
    }


    private void RebindBinding(GameInput.Binding binding)
    {
        ShowRebindKey();
        GameInput.Instance.RebindBinding(binding, () =>
        {
            HideRebindKey();
            UpdateVisual();
        });
    }

    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);
        soundEffectsButton.Select();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ShowRebindKey()
    {
        pressToRebindTransform.gameObject.SetActive(true);
    }
    public void HideRebindKey()
    {
        pressToRebindTransform.gameObject.SetActive(false);
    }
}
