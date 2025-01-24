using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Item 
{
    public string name;
    public string type;
    public int id;
    public int price;
    public Item(string name, string type, int id, int price)
    {
        this.name = name;
        this.type = type;
        this.id = id;
        this.price = price;
    }
}
public class Armor: Item
{
    public int armor;
    public string Enchant;
    public Armor(int armor, string Enchant, string name, string type, int id, int price) :base(name, type, id, price)
    {
        this.armor = armor;
        this.Enchant = Enchant;
    }
}
public class Potion : Item
{
    public int duration;
    public int Amount;
    public Potion(int duration, int Amount, string name, string type, int id, int price) : base(name, type, id, price)
    {
        this.duration = duration;
        this.Amount = Amount;
    }
}
public class ShopItemsPool
{
    private static List<string> ItemTypes = new()
    {
        "Belt"
    };
    private static List<string> PotionTypes = new()
    {
        "Heal"
    };
    public static List<Item> items = new()
    {
        new Armor(10, "", "Belt_1", "Belt", 0, 12),
        new Armor(10, "", "Belt_2", "Belt", 1, 18),
        new Armor(10, "", "Belt_3", "Belt", 2, 11),
        new Armor(10, "", "Belt_4", "Belt", 3, 16),
        new Armor(10, "", "Belt_5", "Belt", 4, 19),
        new Armor(10, "", "Belt_6", "Belt", 5, 10),
        new Armor(10, "", "Belt_7", "Belt", 6, 13),
        new Armor(10, "", "Belt_8", "Belt", 7, 14),
        new Armor(10, "", "Belt_9", "Belt", 8, 15),
        new Armor(10, "", "Belt_10", "Belt",9, 17),
        new Potion(10, 5, "Potion_1", "Heal", 10, 12),
        new Potion(10, 6, "Potion_2", "Heal", 11, 18),
    };
    public static List<Item> CreatePool(string PoolType)
    {
        switch(PoolType)
        {
            case "Items":
                {
                    return SelectPool(ItemTypes);
                }
            case "Potions":
                {
                    return SelectPool(PotionTypes);
                }
            default:
                {
                    return null;
                }
        }
    }
    public static List<Item> SelectPool(List<string> Pool)
    {
        List<Item> CurrentItems = new();
        for (int i = 0; i < items.Count; i++)
        {
            for (int j = 0; j < Pool.Count; j++)
            {
                if (items[i].type == Pool[j])
                {
                    CurrentItems.Add(items[i]);
                }
            }
        }
        return CurrentItems;
    }
    public static Item ItemByID(int ID)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].id == ID)
            {
                return items[i];
            }
        }
        return null;
    }
}
