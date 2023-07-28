using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject _hasProgressGameObject;
    [SerializeField] private Image _barImage;

    [SerializeField] private IHasProgress _hasProgress;


    private void Start()
    {
        _hasProgress = _hasProgressGameObject.GetComponent<IHasProgress>();
        if (_hasProgress == null)
        {
            Debug.LogError("GameObject " + _hasProgressGameObject + " does not have a component, that implements IHaveProgress!");
        }

        _hasProgress.OnProgressChanged += _hasProgress_OnProgressChanged;

        _barImage.fillAmount = 0f;

        Hide();
    }

    private void OnDestroy()
    {
        _hasProgress.OnProgressChanged -= _hasProgress_OnProgressChanged;
    }

    private void _hasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        _barImage.fillAmount = e._progressNormalized;

        if (e._progressNormalized == 0 || e._progressNormalized >= 1)
        {
            Hide();
        } else
        {
            Show();
        }
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
