using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotManager : MonoBehaviour
{
    public List<GameObject> slots;
    public Color ActiveColor;
    public Color UnActiveColor;
    public List<KeyCode> Keys;
    void Start()
    {
        Keys = new()
        {
            KeyCode.Alpha1,
            KeyCode.Alpha2,
            KeyCode.Alpha3,
            KeyCode.Alpha4,
            KeyCode.Alpha5,
            KeyCode.Alpha6,
            KeyCode.Alpha7,
            KeyCode.Alpha8,
            KeyCode.Alpha9,
            KeyCode.Alpha0,
        };
        for (int i = 0; i < transform.childCount; i++)
        {
            slots.Add(transform.GetChild(i).gameObject);
        }
    }

    void Update()
    {
        for (int i = 0; i < Keys.Count; i++)
        {
            if (Input.GetKeyDown(Keys[i]))
            {
                Debug.Log(slots[i].name);
            }
        }
    }
}
