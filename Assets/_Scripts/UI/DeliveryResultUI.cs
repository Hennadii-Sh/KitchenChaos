using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    private const string POPUP = "Popup";

    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _messageText;

    [SerializeField] private Color _successColor;
    [SerializeField] private Color _failedColor;
    [SerializeField] private Sprite _successSprite;
    [SerializeField] private Sprite _failedSprite;

    private Animator _animator;

    private string _successMessageText;
    private string _failedMessageText;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }



    private void Start()
    {

        _successMessageText = "DELIVERY\nSUCCESS";
        _failedMessageText = "DELIVERY\nFAILED";

        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

        gameObject.SetActive(false);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);

        _iconImage.sprite = _failedSprite;
        _backgroundImage.color = _failedColor;
        _messageText.text = _failedMessageText;

        _animator.SetTrigger(POPUP);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);

        _backgroundImage.color = _successColor;
        _iconImage.sprite = _successSprite;
        _messageText.text = _successMessageText;

        _animator.SetTrigger(POPUP);
    }
}
