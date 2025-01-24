using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveItem : MonoBehaviour
{
    public Inventory Inv;
    public Color UnActiveColor;
    public GameObject ParentSlot;
    public GameObject EquipmentSlot;
    public int ItemID = -1;
    public bool IsEquipment = false;

    private void Start()
    {
        Inv = GameObject.Find("Hud/Inventory").GetComponent<Inventory>();
    }
    void Update()
    {
        transform.position = Input.mousePosition;
        if (!Input.GetMouseButton(0))
        {
            ParentSlot.transform.GetChild(0).GetComponent<Image>().color = UnActiveColor;
            Inv.ActiveSlot = null;
            Inv.ActiveID = -1;
            Inv.PreviousActiveSlot = null;
            Destroy(gameObject);
        }
    }
}
