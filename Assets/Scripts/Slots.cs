using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slots : MonoBehaviour, IDropHandler
{

    [System.Serializable]
    public enum SlotsType
    {
        Inventory,
        Helmet,
        Armor,
        RightShoulder,
        RightHand,
        Weapon,
        LeftShoulder,
        LeftHand,
        Shield,
        Belt,
        Trousers,
        RightFoot,
        LeftFoot,
        RightRing,
        LeftRing,
        Necklace,
        Cloak
    }
    public SlotsType SlotType;

    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!item)
        {
            DragItem.ItemBeginDragged.GetComponent<DragItem>().SetParent(transform, this);
        }
    }
}
