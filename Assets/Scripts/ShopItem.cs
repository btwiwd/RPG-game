using UnityEngine;
using UnityEngine.UI;

public class ShopItem
{
    public Sprite ItemSprite;
    public int ItemPrice;
    public int ItemCount;
    public int ItemID;
    public ShopItem(Sprite ItemSprite, int ItemPrice, int ItemCount, int ItemID){
        this.ItemSprite = ItemSprite;
        this.ItemPrice = ItemPrice;
        this.ItemCount = ItemCount;
        this.ItemID = ItemID;
    }
}
