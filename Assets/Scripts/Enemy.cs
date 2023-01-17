using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float TotalHealth;
    public float CurrentHealth;
    public float AttackDamage;
    public float MovimentSpeed;

    public float LookRadius;
    public Transform target;

    private Animator anim;
    private CapsuleCollider cap;
    private NavMeshAgent agent;

    public float ColliderRadius;
    private bool isReady;
    private bool PlayerIsAlive;

    public Image HealthBar;
    public GameObject CanvasBar;

    private void Start()
    {
        anim = GetComponent<Animator>();
        cap = GetComponent<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();

        CurrentHealth = TotalHealth;
        PlayerIsAlive = true;
    }

    private void Update()
    {
        if (CurrentHealth > 0)
        {

            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= LookRadius)
            {

                agent.isStopped = false;
                if (!anim.GetBool("isAttacking"))
                {
                    agent.SetDestination(target.position);
                    anim.SetInteger("transition", 1);
                    anim.SetBool("isWalking", true);
                }

                if (distance <= agent.stoppingDistance)
                {
                    StartCoroutine("Attack");
                    LookTarget();
                }

                if (distance >= agent.stoppingDistance)
                {
                    anim.SetBool("isAttacking", false);
                }
            }
            else
            {
                anim.SetInteger("transition", 0);
                anim.SetBool("isWalking", false);
                anim.SetBool("isAttacking", false);
                agent.isStopped = true;
            }
        }
    }

    IEnumerator Attack()
    {
        if (!isReady && PlayerIsAlive && !anim.GetBool("isHiting"))
        {
            isReady = true;
            anim.SetBool("isAttacking", true);
            anim.SetBool("isWalking", false);
            anim.SetInteger("transition", 2);

            yield return new WaitForSeconds(1f);
            GetEnemy();
            yield return new WaitForSeconds(1.7f);
            isReady = false;
        }

        if (!PlayerIsAlive)
        {
            anim.SetInteger("transition", 0);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
            agent.isStopped = true;
        }
    }

    void GetEnemy()
    {
        foreach (Collider c in Physics.OverlapSphere((transform.position + transform.forward * ColliderRadius), ColliderRadius))
        {
            if (c.gameObject.CompareTag("Player"))
            {
                c.gameObject.GetComponent<Player>().GetHit(20f);
                PlayerIsAlive = c.gameObject.GetComponent<Player>().isAlive;
            }
        }
    }

    void LookTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, LookRadius);
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
            cap.enabled = false;
            CanvasBar.gameObject.SetActive(false);
        }

    }

    IEnumerator RecoveryFromHit()
    {
        yield return new WaitForSeconds(1f);
        anim.SetInteger("transition", 0);
        anim.SetBool("isHiting", false);
        isReady = false;
    }
}
