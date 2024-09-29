using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{
    public List<GameObject> ShopSlots;
    public ShopItemsPool Pool;
    public List<int> ShopItemQuantitis;
    public GameObject BuyButton;
    public GameObject ActiveSlot = null;
    public GameObject PreviousActiveSlot = null;
    public int ActiveID = -1;
    public int ItemCount;
    public List<Sprite> CurrentPool;
    void Start()
    {
        Pool = GetComponent<ShopItemsPool>();
        foreach(Sprite s in Pool.ItemList)
        {
            CurrentPool.Add(s);
        }
        BuyButton = GameObject.Find("Hud/Shop/Buy");
        ItemCount = Random.Range(1, ShopSlots.Count-1);
        //ItemCount = 24;
        for(int i = 0; i < ItemCount; i++)
        {
            if(ItemCount <= CurrentPool.Count)
            {
                int ItemNumber = Random.Range(0, CurrentPool.Count - 1);
                while (CurrentPool[ItemNumber] == null)
                {
                    ItemNumber = Random.Range(0, CurrentPool.Count - 1);
                }
                CurrentPool[ItemNumber] = null;
                //int ItemNumber = 0;
                ShopItemQuantitis[i] = Random.Range(1, 8);
                ShopSlots[i].GetComponent<Slot>().ItemID = ItemNumber;
                ShopSlots[i].transform.GetChild(1).GetComponent<Image>().sprite = Pool.ItemList[ItemNumber];
                ShopSlots[i].transform.GetChild(2).GetComponent<Text>().text = ShopItemQuantitis[i].ToString();
                ShopSlots[i].transform.GetChild(3).GetComponent<Text>().text = Pool.ItemPrices[ItemNumber].ToString();
            }
        }
    }
    void Update()
    {
        if(ActiveSlot == null)
        {
            BuyButton.GetComponent<Button>().interactable = false;
        }
        else if(ActiveSlot != null)
        {
            BuyButton.GetComponent<Button>().interactable = true;
        }
        for(int i = 0; i < ShopSlots.Count; i++)
        {
            if(ShopSlots[i].transform.GetChild(1).GetComponent<Image>().sprite == null||ShopItemQuantitis[i] <= 0)
            {
                ShopSlots[i].transform.GetChild(1).GetComponent<Image>().enabled = false;
                ShopSlots[i].transform.GetComponent<Button>().enabled = false;
                ShopSlots[i].transform.GetChild(2).GetComponent<Text>().enabled = false;
                ShopSlots[i].transform.GetChild(3).GetComponent<Text>().enabled = false;
            }
            else
            {
                ShopSlots[i].transform.GetChild(1).GetComponent<Image>().enabled = true;
                ShopSlots[i].transform.GetComponent<Button>().enabled = true;
                ShopSlots[i].transform.GetChild(2).GetComponent<Text>().enabled = true;
                ShopSlots[i].transform.GetChild(3).GetComponent<Text>().enabled = true;
            }
        }
    }
}
