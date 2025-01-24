using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopGenerator : MonoBehaviour
{
    public ShopController ShopController;
    public int ItemCount;
    public List<Item> CurrentItems = new();
    public string ShopType;
    public List<ShopItem> CurrentShop = new();
    void Start()
    { 
        ShopController = GameObject.Find("Hud/Shop").GetComponent<ShopController>();
        CurrentItems.AddRange(ShopItemsPool.CreatePool(ShopType));
        ItemCount = Random.Range(1, ShopController.ShopSlots.Count-1);
        for(int i = 0; i < ItemCount; i++)
        {
            if(CurrentItems.Count != 0)
            {
                int ItemNumber = Random.Range(0, CurrentItems.Count - 1);
                CurrentShop.Add(new ShopItem(Random.Range(1, 8), CurrentItems[ItemNumber]));
                CurrentItems.Remove(CurrentItems[ItemNumber]);
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
