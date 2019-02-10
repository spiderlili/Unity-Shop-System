using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonSetup : MonoBehaviour
{

    public Button buttonComponent;
    public TMP_Text nameLabel;
    public Image iconImage;
    public TMP_Text priceText;

    private Item item;
    private ShopScrollList scrollList;

    public void Setup(Item currentItem, ShopScrollList currScrollList)
    {
        item = currentItem;
        nameLabel.text = item.itemName;
        priceText.text = item.price.ToString();
        iconImage.sprite = item.icon;
        scrollList = currScrollList; //button knows what list it currently belongs to

    }

    public void HandleClick()
    {

    }
}
