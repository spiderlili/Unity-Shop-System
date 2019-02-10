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
    public Transform contentPanel; //attach list to panel
    public ShopScrollList otherShop;
    public TMP_Text myGoldDisplay; //ref how much gold the player has
    public ButtonObjectPool buttonObjectPool;
    public float gold = 20f;

    // Start is called before the first frame update
    void Start()
    {
        RefreshDisplay(); //display list of buttons
    }

    private void RefreshDisplay()
    {
        AddButtons();
    }


    // go thru list - figure how many items in item list - add buttons as needed for each item
    //call set up func in button
    private void AddButtons()
    {
        for (int i = 0; i < itemList.Count; i++) {
            Item item = itemList[i];

            //get a new obj from obj pool and store a ref to it in a new button
            GameObject newButton = buttonObjectPool.GetObject();

            //parent it to the panel w the vertical layout group on it = new button laid out correctly
            newButton.transform.SetParent(contentPanel);

            //tell the button to set itself up
            ButtonSetup sampleButton = newButton.GetComponent<ButtonSetup>();
            sampleButton.Setup(item, this);
        }
        
    }
}
