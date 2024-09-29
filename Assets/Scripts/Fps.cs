using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fps : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Info.IsFps)
        {
        transform.GetChild(0).GetComponent<Text>().text = (Mathf.RoundToInt(1/Time.deltaTime)).ToString();
        }
    }
}
