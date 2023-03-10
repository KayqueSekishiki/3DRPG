using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject SlotParent;
    public List<Slots> slots = new List<Slots>();

    public static Inventory instance;

    void Start()
    {
        instance = this;
        GetSlots();
    }

    public void GetSlots()
    {
        foreach (Slots s in SlotParent.GetComponentsInChildren<Slots>())
        {
            slots.Add(s);
        }

        //        Createitem();
    }

    public void Createitem(Item item)
    {

        foreach (Slots s in slots)
        {
            if (s.transform.childCount == 0)
            {
                GameObject CurrentItem = Instantiate(GameController.instance.ItemPrebab, s.transform);
                CurrentItem.GetComponent<DragItem>().item = item;
                return;
            }
        }
    }
}
