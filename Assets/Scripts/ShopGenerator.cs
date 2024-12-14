using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopGenerator : MonoBehaviour
{
    public ShopController ShopController;
    public ShopItemsPool Pool;
    public int ItemCount;
    public List<int> CurrentPrice;
    public List<string> CurrentName;
    public string ShopType;
    public List<ShopItem> CurrentShop = new();
    void Start()
    {
        Pool = GameObject.Find("Hud/Shop").GetComponent<ShopItemsPool>();
        ShopController = GameObject.Find("Hud").GetComponent<ShopController>();
        if (ShopType == "Items")
        {
            CurrentPrice.AddRange(Pool.ItemPrices);
            CurrentName.AddRange(Pool.ItemNames);
        }
        else if (ShopType == "Potions")
        {
            CurrentPrice.AddRange(Pool.PotionPrices);
            CurrentName.AddRange(Pool.PotionNames);
        }
        ItemCount = Random.Range(1, ShopController.ShopSlots.Count-1);
        Pool.CurrentItemsCount = ItemCount;
        for(int i = 0; i < ItemCount; i++)
        {
            if(CurrentName.Count != 0)
            {
                int ItemNumber = Random.Range(0, CurrentName.Count - 1);
                CurrentShop.Add(new ShopItem(CurrentName[ItemNumber], CurrentPrice[ItemNumber], Random.Range(1, 8), ItemNumber));
                CurrentName.Remove(CurrentName[ItemNumber]);
            }
            else
            {
                break;
            }
        }
    }
    void Update()
    {
        
    }
}
