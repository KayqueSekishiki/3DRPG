using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator anim;
    public float ColliderRadius;
    public bool isOpened = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        GetPlayer();
    }

    void GetPlayer()
    {
        foreach (Collider c in Physics.OverlapSphere((transform.position + transform.forward * ColliderRadius), ColliderRadius))
        {
            if (c.gameObject.CompareTag("Player"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    OpenChest();
                }
            }
        }
    }

    void OpenChest()
    {
        anim.SetTrigger("open");
        isOpened = true;
    }
}
