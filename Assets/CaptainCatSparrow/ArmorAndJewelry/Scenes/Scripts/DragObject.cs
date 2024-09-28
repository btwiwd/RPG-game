using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.CaptainCatSparrow.ArmorAndJewelry.Demo
{
    public class DragObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public static GameObject DraggableObject { get; set; }

        public void OnBeginDrag(PointerEventData eventData)
        {
            DraggableObject = Instantiate(eventData.pointerDrag);
            var rt = DraggableObject.GetComponent<RectTransform>();
            rt.sizeDelta = eventData.pointerDrag.GetComponent<RectTransform>().sizeDelta;
            DraggableObject.GetComponent<Image>().raycastTarget = false;
            DraggableObject.transform.SetParent(Demo.Instance.Canvas, false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            DraggableObject.GetComponent<RectTransform>().position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (DraggableObject!=null)
            {
                Destroy(DraggableObject);
                DraggableObject = null;
            }
        }
    }
}
