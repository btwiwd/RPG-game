using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopGenerator : MonoBehaviour
{
    public ShopController ShopController;
    public ShopItemsPool Pool;
    public List<int> ShopItemQuantitis;
    public int ItemCount;
    public List<int> CurrentPrice;
    public List<string> CurrentName;
    public string ShopType;
    public List<ShopItem> CurrentShop;
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
        //ItemCount = 24;
        for(int i = 0; i < ItemCount; i++)
        {
            string SpritePath = CurrentName[i];                 //Сделать загрузку картинки из ресурсов
            if(ItemCount <= CurrentName.Count)
            {
                int ItemNumber = Random.Range(0, CurrentName.Count - 1);
                while (CurrentName[ItemNumber] == null)
                {
                    ItemNumber = Random.Range(0, CurrentName.Count - 1);
                }
                CurrentName[ItemNumber] = null;
                //int ItemNumber = 0;
                ShopItemQuantitis[i] = Random.Range(1, 8);
                CurrentShop.Add(new ShopItem(CurrentName[ItemNumber], CurrentPrice[ItemNumber], ShopItemQuantitis[i], ItemNumber));
            }
        }
    }
    void Update()
    {
        
    }
}
