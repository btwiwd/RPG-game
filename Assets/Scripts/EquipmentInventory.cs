using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentInventory : MonoBehaviour
{
    public List<GameObject> EquipmentSlots;
    public GameObject Description;
    void Start()
    {
        
    }
    void Update()
    {
        for (int i = 0; i < EquipmentSlots.Count; i++)
        {
            if (EquipmentSlots[i].GetComponent<EquipSlot>().ItemID == -1)
            {
                EquipmentSlots[i].transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}
