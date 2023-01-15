using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public Sprite icon;
    public string Name;
    public float Value;

    [System.Serializable]
    public enum Type
    {
        Potion,
        Elixir,
        Crystal
    }
    public Type ItemType;

    [System.Serializable]
    public enum SlotsType
    {
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

    public void GetAction()
    {
        switch (ItemType)
        {
            case Type.Potion:
                Debug.Log("Health +" + Value);
                break;

            case Type.Elixir:
                Debug.Log("Elixir +" + Value);
                break;

            case Type.Crystal:
                Debug.Log("Crystal +" + Value);
                break;
        }
    }
}
