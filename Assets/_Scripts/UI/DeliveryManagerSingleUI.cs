using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _recipeNameText;
    [SerializeField] private Transform _iconContainer;
    [SerializeField] private Transform _iconTemplate;


    private void Awake()
    {
        _iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        _recipeNameText.SetText(recipeSO._recipeName);

        foreach (Transform child in _iconContainer)
        {
            if (child == _iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in recipeSO._kitchenObjectSOList)
        {
            Transform iconTranform = Instantiate(_iconTemplate, _iconContainer);
            iconTranform.gameObject.SetActive(true);
            iconTranform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}
