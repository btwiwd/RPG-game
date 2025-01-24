using UnityEngine;
using UnityEngine.UI;

public class ShopItem
{
    public int ItemCount;
    public Item item;
    public ShopItem(int ItemCount, Item item){
        this.ItemCount = ItemCount;
        this.item = item;

    }
}
