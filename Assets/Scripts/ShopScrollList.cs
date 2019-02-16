using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Pure data class to hold info about items
[System.Serializable] //set the values of this class in the inspector
public class Item {
    public string itemName;
    public Sprite icon;
    public float price = 1.0f;
}

public class ShopScrollList : MonoBehaviour
{
    public List<Item> itemList;
    public Transform shopContentPanel; //attach list to panel
    public ShopScrollList otherShop;
    public TMP_Text myGoldDisplay; //ref how much gold the player has
    public ButtonObjectPool buttonObjectPool;
    public float gold = 20f;

    // Start is called before the first frame update
    void Start()
    {
        RefreshDisplay(); //display list of buttons
    }

    public void RefreshDisplay()
    {
        myGoldDisplay.text = "Gold: " + gold.ToString();
      //  RemoveButtons();//crash if activated
        AddButtons();
    }

    private void RemoveButtons()
    {
        //loop through all child objects of the content panel and remove them all back to object pool
        //repopulate using addbutons
        while (shopContentPanel.childCount > 0)
        {
            GameObject toRemove = transform.GetChild(0).gameObject;
            buttonObjectPool.ReturnObject(toRemove);
        }
    }

    // go thru list - figure how many items in item list - add buttons as needed for each item
    private void AddButtons()
    {
        for (int i = 0; i < itemList.Count; i++) {
            Item item = itemList[i];

            //get a new obj from obj pool and store a ref to it in a new button
            GameObject newButton = buttonObjectPool.GetObject();

            //parent it to the panel w the vertical layout group on it = new button laid out correctly
            newButton.transform.SetParent(shopContentPanel);

            //tell the button to set itself up
            ButtonSetup sampleButton = newButton.GetComponent<ButtonSetup>();
            sampleButton.Setup(item, this);
        }
    }

    public void TryTransferItemToOtherShop(Item item) {
        if (otherShop.gold >= item.price) {
            gold += item.price;
            otherShop.gold -= item.price;
            AddItem(item, otherShop);
            RemoveItem(item, this);
            RefreshDisplay();
            otherShop.RefreshDisplay();
            Debug.Log("enough gold");
        }
    }

    private void AddItem(Item itemToAdd, ShopScrollList shopList) {
        shopList.itemList.Add(itemToAdd);
    }

    private void RemoveItem(Item itemToRemove, ShopScrollList shopList)
    {
        for (int i = shopList.itemList.Count - 1; i >= 0; i--) {
            if (shopList.itemList[i] == itemToRemove) {
                shopList.itemList.RemoveAt(i); //remove item at index
            }
        }
    }
}
