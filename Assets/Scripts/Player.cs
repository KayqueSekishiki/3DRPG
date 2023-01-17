using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float TotalHealth;
    public float CurrentHealth;
    public float Speed;
    public float RotSpeed;
    public float Gravity;
    private float Rotation;
    public float AttackDamage;

    public Image HealthBar;

    Vector3 MoveDirection;

    CharacterController controller;
    Animator anim;

    bool isReady;
    public bool isAlive;

    List<Transform> EnemiesList = new List<Transform>();
    public float ColliderRadius;



    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        CurrentHealth = TotalHealth;
        isAlive = true;
    }


    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            Move();
            GetMouseInput();
        }
    }

    void Move()
    {

        if (controller.isGrounded)
            Debug.Log("no chão");
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (!anim.GetBool("isAttacking"))
                {
                    anim.SetBool("isWalking", true);
                    anim.SetInteger("transition", 1);
                    MoveDirection = Vector3.forward * Speed;
                    MoveDirection = transform.TransformDirection(MoveDirection);

                }
                else
                {
                    anim.SetBool("isWalking", false);
                    MoveDirection = Vector3.zero;
                    //  StartCoroutine(Attack(1));
                }
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetBool("isWalking", false);
                anim.SetInteger("transition", 0);
                MoveDirection = Vector3.zero;
            }
        }

        Rotation += Input.GetAxisRaw("Horizontal") * RotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, Rotation, 0);

        MoveDirection.y -= Gravity * Time.deltaTime;
        controller.Move(MoveDirection * Time.deltaTime);
    }

    void GetMouseInput()
    {
        if (controller.isGrounded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (anim.GetBool("isWalking"))
                {
                    anim.SetBool("isWalking", false);
                    anim.SetInteger("transition", 0);
                }

                if (!anim.GetBool("isWalking"))
                {
                    StartCoroutine("Attack");
                }

            }
        }
    }

    IEnumerator Attack()
    {
        if (!isReady && !anim.GetBool("isHiting"))
        {
            isReady = true;
            anim.SetBool("isAttacking", true);
            anim.SetInteger("transition", 2);

            yield return new WaitForSeconds(0.5f);

            GetEnemiesRange();

            foreach (Transform enemies in EnemiesList)
            {
                Enemy enemy = enemies.GetComponent<Enemy>();

                if (enemy != null)
                {
                    enemy.GetHit(AttackDamage);
                }
            }


            yield return new WaitForSeconds(0.8f);


            anim.SetBool("isAttacking", false);
            anim.SetInteger("transition", 0);
            isReady = false;
        }
    }


    void GetEnemiesRange()
    {
        EnemiesList.Clear();
        foreach (Collider c in Physics.OverlapSphere((transform.position + transform.forward * ColliderRadius), ColliderRadius))
        {
            if (c.gameObject.CompareTag("Enemy"))
            {
                EnemiesList.Add(c.transform);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward, ColliderRadius);
    }

    public void GetHit(float Damage)
    {
        CurrentHealth -= Damage;
        HealthBar.fillAmount = CurrentHealth / TotalHealth;

        if (CurrentHealth > 0)
        {
            StopCoroutine("Attack");
            anim.SetInteger("transition", 3);
            anim.SetBool("isHiting", true);
            StartCoroutine(RecoveryFromHit());
        }
        else
        {
            anim.SetInteger("transition", 4);
            isAlive = false;

        }

    }

    IEnumerator RecoveryFromHit()
    {
        yield return new WaitForSeconds(1f);
        anim.SetInteger("transition", 0);
        anim.SetBool("isHiting", false);
        isReady = false;
        anim.SetBool("isAttacking", false);
    }

    public void IncreaseStats(float IncreaseHealth, float IncreasePower, float IncreaseSpeed)
    {
        TotalHealth += IncreaseHealth;
        CurrentHealth += IncreaseHealth;
        AttackDamage += IncreasePower;
        Speed += IncreaseSpeed;
    }

    public void DecreaseStats(float IncreaseHealth, float IncreasePower, float IncreaseSpeed)
    {
        TotalHealth -= IncreaseHealth;
        CurrentHealth -= IncreaseHealth;
        AttackDamage -= IncreasePower;
        Speed -= IncreaseSpeed;
    }
}
