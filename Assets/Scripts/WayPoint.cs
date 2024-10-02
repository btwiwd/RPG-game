using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WayPoint : MonoBehaviour
{
    public GameObject Waypoint;
    public Transform target;
    public bool QuestTaken = false;
    void Start()
    {
        Waypoint = GameObject.Find("Hud/WayPoint");
    }

    void Update()
    {
        if (QuestTaken)
        {
            float minX = Waypoint.transform.GetChild(0).gameObject.GetComponent<Image>().GetPixelAdjustedRect().width/2;
            float maxX = Screen.width-minX;
            Vector2 pos = Camera.main.WorldToScreenPoint(target.position);
            if (Vector3.Dot((target.position - transform.position), transform.forward)< 0)
            {
                if (pos.x < Screen.width/2)
                {
                    pos.x = maxX;
                }
                else
                {
                    pos.x = minX;
                }
            }
            Waypoint.transform.position = pos;
            Waypoint.transform.GetChild(1).gameObject.GetComponent<Text>().text = Mathf.Floor(Vector3.Distance(target.position, transform.position)).ToString();
        }
    }
}
