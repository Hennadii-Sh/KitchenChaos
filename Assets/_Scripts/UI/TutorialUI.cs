using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _keyMoveUpText;
    [SerializeField] TextMeshProUGUI _keyMoveDownText;
    [SerializeField] TextMeshProUGUI _keyMoveLeftText;
    [SerializeField] TextMeshProUGUI _keyMoveRightText;
    [SerializeField] TextMeshProUGUI _keyInteractText;
    [SerializeField] TextMeshProUGUI _keyInteractAlternateText;
    [SerializeField] TextMeshProUGUI _keyPauseText;
    [SerializeField] TextMeshProUGUI _keyGamepadInteractText;
    [SerializeField] TextMeshProUGUI _keyGamepadAlternateInteractText;
    [SerializeField] TextMeshProUGUI _keyGamepadPauseText;

    private void Start()
    {
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        UpdateVisual();
        Show();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void GameInput_OnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        _keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        _keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        _keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        _keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        _keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        _keyInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        _keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        _keyGamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        _keyGamepadAlternateInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        _keyGamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
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
