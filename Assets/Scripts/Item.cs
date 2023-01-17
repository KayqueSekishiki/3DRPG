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

    public Player player;

    public void GetAction()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        switch (ItemType)
        {
            case Type.Potion:
                player.IncreaseStats(Value, 0, 0);
                break;

            case Type.Elixir:
                player.IncreaseStats(0, Value, 0);
                break;

            case Type.Crystal:
                //  Debug.Log("Crystal +" + Value);
                player.IncreaseStats(0, 0, Value);
                break;
        }
    }

    public void RemoveStats()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        switch (ItemType)
        {
            case Type.Potion:
                player.DecreaseStats(Value, 0, 0);
                break;

            case Type.Elixir:
                player.DecreaseStats(0, Value, 0);
                break;

            case Type.Crystal:
                //  Debug.Log("Crystal +" + Value);
                player.DecreaseStats(0, 0, Value);
                break;
        }
    }
}
