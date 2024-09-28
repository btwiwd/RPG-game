using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.CaptainCatSparrow.ArmorAndJewelry.Demo
{
    public class DropObject : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;


            Demo.Instance.ValidateAndApplyDrop(eventData.pointerDrag, gameObject);
        }
    }
}
