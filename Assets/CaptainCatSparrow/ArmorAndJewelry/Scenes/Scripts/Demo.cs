using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CaptainCatSparrow.ArmorAndJewelry.Demo
{
    public class Demo : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] belts, body, boots, bracers, earrings, gloves, helmets, necklace, pants, rings, shoulders;
        [SerializeField]
        private GameObject iconPrefab;

        private Button _previousButton, _nextButton;
        private ScrollRect _scroll;
        private TextMeshProUGUI _label;
        private Transform _helmet, _necklace, _earrings1, _earrings2, _shoulders1, _shoulders2, _bracers1, _bracers2, _belt, _gloves1, _gloves2, _pants, _boots, _ring1, _ring2;
        private ArmorJewelry _selectedArmorJewelry;

        public static Demo Instance { get; private set; }
        public Transform Canvas { get; private set; }
        private enum ArmorJewelry
        {
            Helmet,
            Necklace,
            Earrings,
            Shoulders,
            Bracers,
            Belt,
            Gloves,
            Pants,
            Boots,
            Rings
        }

        void Awake()
        {
            Instance = this;
            Canvas = GameObject.Find("Canvas").transform;
            _previousButton = Canvas.Find("Scroll/Previous").GetComponent<Button>();
            _nextButton = Canvas.Find("Scroll/Next").GetComponent<Button>();
            _label = Canvas.Find("Scroll/SelectedLabel/Text").GetComponent<TextMeshProUGUI>();
            _scroll = Canvas.Find("Scroll/Scroll View").GetComponent<ScrollRect>();
            _helmet = Canvas.Find("Equiped/Helmet");
            _necklace = Canvas.Find("Equiped/Necklace");
            _earrings1 = Canvas.Find("Equiped/Earrings_1");
            _earrings2 = Canvas.Find("Equiped/Earrings_2");
            _shoulders1 = Canvas.Find("Equiped/Shoulders_1");
            _shoulders2 = Canvas.Find("Equiped/Shoulders_2");
            _bracers1 = Canvas.Find("Equiped/Bracers_1");
            _bracers2 = Canvas.Find("Equiped/Bracers_2");
            _belt = Canvas.Find("Equiped/Belt");
            _gloves1 = Canvas.Find("Equiped/Gloves_1");
            _gloves2 = Canvas.Find("Equiped/Gloves_2");
            _pants = Canvas.Find("Equiped/Pants");
            _boots = Canvas.Find("Equiped/Boots");
            _ring1 = Canvas.Find("Equiped/Ring_1");
            _ring2 = Canvas.Find("Equiped/Ring_2");

            _previousButton.onClick.AddListener(() =>
            {
                MoveArmorJewelry(-1);
            });

            _nextButton.onClick.AddListener(() =>
            {
                MoveArmorJewelry(1);
            });

            SelectArmorJewelry();
        }

        public void ValidateAndApplyDrop(GameObject dragGO, GameObject dropGO)
        {
            var dropTransform = dropGO.transform;
            switch (_selectedArmorJewelry)
            {
                case ArmorJewelry.Helmet:
                    if (dropTransform == _helmet)
                    {
                        var icon = Instantiate(dragGO);
                        DestroyChildren(_helmet.transform);
                        icon.transform.SetParent(_helmet.transform, false);
                        StretchIcon(icon.GetComponent<RectTransform>());
                    }
                    break;
                case ArmorJewelry.Necklace:
                    if (dropTransform == _necklace)
                    {
                        var icon = Instantiate(dragGO);
                        DestroyChildren(_necklace.transform);
                        icon.transform.SetParent(_necklace.transform, false);
                        StretchIcon(icon.GetComponent<RectTransform>());
                    }
                    break;
                case ArmorJewelry.Earrings:
                    if (dropTransform == _earrings1 || dropTransform == _earrings2)
                    {
                        var icon = Instantiate(dragGO);
                        var icon2 = Instantiate(dragGO);
                        DestroyChildren(_earrings1.transform);
                        DestroyChildren(_earrings2.transform);
                        icon.transform.rotation = Quaternion.Euler(0, 180, 0);
                        icon.transform.SetParent(_earrings1.transform, false);
                        icon2.transform.SetParent(_earrings2.transform, false);
                        StretchIcon(icon.GetComponent<RectTransform>());
                        StretchIcon(icon2.GetComponent<RectTransform>());
                    }
                    break;
                case ArmorJewelry.Shoulders:
                    if (dropTransform == _shoulders1 || dropTransform == _shoulders2)
                    {
                        var icon = Instantiate(dragGO);
                        var icon2 = Instantiate(dragGO);
                        DestroyChildren(_shoulders1.transform);
                        DestroyChildren(_shoulders2.transform);
                        icon.transform.rotation = Quaternion.Euler(0, 180, 0);
                        icon.transform.SetParent(_shoulders1.transform, false);
                        icon2.transform.SetParent(_shoulders2.transform, false);
                        StretchIcon(icon.GetComponent<RectTransform>());
                        StretchIcon(icon2.GetComponent<RectTransform>());
                    }
                    break;
                case ArmorJewelry.Bracers:
                    if (dropTransform == _bracers1 || dropTransform == _bracers2)
                    {
                        var icon = Instantiate(dragGO);
                        var icon2 = Instantiate(dragGO);
                        DestroyChildren(_bracers1.transform);
                        DestroyChildren(_bracers2.transform);
                        icon2.transform.rotation = Quaternion.Euler(0, 180, 0);
                        icon.transform.SetParent(_bracers1.transform, false);
                        icon2.transform.SetParent(_bracers2.transform, false);
                        StretchIcon(icon.GetComponent<RectTransform>());
                        StretchIcon(icon2.GetComponent<RectTransform>());
                    }
                    break;
                case ArmorJewelry.Belt:
                    if (dropTransform == _belt)
                    {
                        var icon = Instantiate(dragGO);
                        icon.transform.SetParent(_belt.transform, false);
                        StretchIcon(icon.GetComponent<RectTransform>());
                    }
                    break;
                case ArmorJewelry.Gloves:
                    if (dropTransform == _gloves1 || dropTransform == _gloves2)
                    {
                        var icon = Instantiate(dragGO);
                        var icon2 = Instantiate(dragGO);
                        DestroyChildren(_gloves1.transform);
                        DestroyChildren(_gloves2.transform);
                        icon2.transform.rotation = Quaternion.Euler(0, 180, 0);
                        icon.transform.SetParent(_gloves1.transform, false);
                        icon2.transform.SetParent(_gloves2.transform, false);
                        StretchIcon(icon.GetComponent<RectTransform>());
                        StretchIcon(icon2.GetComponent<RectTransform>());
                    }
                    break;
                case ArmorJewelry.Pants:
                    if (dropTransform == _pants)
                    {
                        var icon = Instantiate(dragGO);
                        DestroyChildren(_pants.transform);
                        icon.transform.SetParent(_pants.transform, false);
                        StretchIcon(icon.GetComponent<RectTransform>());
                    }
                    break;
                case ArmorJewelry.Boots:
                    if (dropTransform == _boots)
                    {
                        var icon = Instantiate(dragGO);
                        DestroyChildren(_boots.transform);
                        icon.transform.SetParent(_boots.transform, false);
                        StretchIcon(icon.GetComponent<RectTransform>());
                    }
                    break;
                case ArmorJewelry.Rings:
                    if (dropTransform == _ring1)
                    {
                        var icon = Instantiate(dragGO);
                        DestroyChildren(_ring1.transform);
                        icon.transform.SetParent(_ring1.transform, false);
                        StretchIcon(icon.GetComponent<RectTransform>());
                    }
                    else if (dropTransform == _ring2)
                    {
                        var icon = Instantiate(dragGO);
                        DestroyChildren(_ring2.transform);
                        icon.transform.SetParent(_ring2.transform, false);
                        StretchIcon(icon.GetComponent<RectTransform>());
                    }
                    break;
            }
        }

        private void StretchIcon(RectTransform rt)
        {
            rt.anchorMax = Vector3.one;
            rt.anchorMin = Vector3.zero;
            rt.anchoredPosition= Vector3.zero;
        }

        private void DestroyChildren(Transform parent)
        {
            if (parent.childCount>0)
                foreach (Transform child in parent)
                    Destroy(child.gameObject);
        }

        private void MoveArmorJewelry(int step)
        {
            var newValue = (int)_selectedArmorJewelry + step;
            if (Enum.IsDefined(typeof(ArmorJewelry), newValue))
                _selectedArmorJewelry = (ArmorJewelry)newValue;
            else if (newValue < 0)
                _selectedArmorJewelry = ArmorJewelry.Rings;
            else if (newValue > (int)ArmorJewelry.Rings)
                _selectedArmorJewelry = ArmorJewelry.Helmet;

            SelectArmorJewelry();
        }

        private void PopulateGrid(Sprite[] sprites, Color color)
        {
            foreach (var sprite in sprites)
            {
                var icon = Instantiate(iconPrefab);
                icon.GetComponent<Image>().color = color;
                var coreImage = icon.transform.Find("Core").GetComponent<Image>();
                coreImage.sprite = sprite;
                icon.transform.SetParent(_scroll.content);
            }
        }

        private void SelectArmorJewelry()
        {
            foreach (Transform child in _scroll.content)
                Destroy(child.gameObject);
            switch (_selectedArmorJewelry)
            {
                case ArmorJewelry.Helmet:
                    PopulateGrid(helmets, new Color(0.4f, 0.6f, 0.5f));
                    break;
                case ArmorJewelry.Necklace:
                    PopulateGrid(necklace, new Color(0.5f, 1f, 0.7f));
                    break;
                case ArmorJewelry.Earrings:
                    PopulateGrid(earrings, new Color(1f, 0.6f, 0.4f));
                    break;
                case ArmorJewelry.Shoulders:
                    PopulateGrid(shoulders, new Color(0.2f, 0.4f, 0.6f));
                    break;
                case ArmorJewelry.Bracers:
                    PopulateGrid(bracers, new Color(0.4f, 0.6f, 1f));
                    break;
                case ArmorJewelry.Belt:
                    PopulateGrid(belts, new Color(1f, 0.8f, 0.5f));
                    break;
                case ArmorJewelry.Gloves:
                    PopulateGrid(gloves, new Color(0.5f, 1f, 0.7f));
                    break;
                case ArmorJewelry.Pants:
                    PopulateGrid(pants, new Color(1f, 0.6f, 0.4f));
                    break;
                case ArmorJewelry.Boots:
                    PopulateGrid(boots, new Color(0.3f, 0.6f, 1f));
                    break;
                case ArmorJewelry.Rings:
                    PopulateGrid(rings, new Color(1f, 0.4f, 0.4f));
                    break;
            }
            _label.text = _selectedArmorJewelry.ToString();
        }

    }

}
