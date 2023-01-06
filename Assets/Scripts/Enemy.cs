using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float TotalHealth;
    public float CurrentHealth;
    public float AttackDamage;
    public float MovimentSpeed;

    public void GetHit()
    {
        Debug.Log("Morri!");
        Destroy(gameObject);
    }
}
