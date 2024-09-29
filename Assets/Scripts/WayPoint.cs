using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WayPoint : MonoBehaviour
{
    public Text distance;
    public Transform target;
    void Start()
    {
        distance = transform.GetChild(1).gameObject.GetComponent<Text>();
    }

    void Update()
    {
        float minX = transform.GetChild(0).gameObject.GetComponent<Image>().GetPixelAdjustedRect().width/2;
        float maxX = Screen.width-minX;
        float minY = transform.GetChild(0).gameObject.GetComponent<Image>().GetPixelAdjustedRect().height/2;
        float maxY = Screen.height-minY;
        Vector2 pos = Camera.main.WorldToScreenPoint(target.position);
        pos.x = Mathf.Clamp(pos.x,minX,maxX);
        pos.y = Mathf.Clamp(pos.x,minY,maxY);
        transform.position = pos;
    }
}
