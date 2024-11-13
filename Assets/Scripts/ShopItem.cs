using UnityEngine;
using UnityEngine.UI;

public class ShopItem
{
    public string ItemSpritePath;
    public int ItemPrice;
    public int ItemCount;
    public int ItemID;
    public ShopItem(string ItemSpritePath, int ItemPrice, int ItemCount, int ItemID){
        this.ItemSpritePath = ItemSpritePath;
        this.ItemPrice = ItemPrice;
        this.ItemCount = ItemCount;
        this.ItemID = ItemID;
    }
}
