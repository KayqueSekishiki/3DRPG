using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    private bool isReady;

    private void Start()
    {
        anim = GetComponent<Animator>();
        cap = GetComponent<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();

        CurrentHealth = TotalHealth;
    }

    private void Update()
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
                StartCoroutine(Attack());
                LookTarget();
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

    IEnumerator Attack()
    {
        if (!isReady)
        {
            isReady = true;
            anim.SetBool("isAttacking", true);
            anim.SetBool("isWalking", false);
            anim.SetInteger("transition", 2);

            yield return new WaitForSeconds(1f);
            isReady = false;
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
        if (CurrentHealth > 0)
        {
            anim.SetInteger("transition", 3);
            StartCoroutine(RecoveryFromHit());
        }
        else
        {
            anim.SetInteger("transition", 4);
            cap.enabled = false;
        }

    }

    IEnumerator RecoveryFromHit()
    {
        yield return new WaitForSeconds(1f);
        anim.SetInteger("transition", 0);
    }


    void Die()
    {
        if (CurrentHealth <= 0)
        {

            //  Destroy(gameObject, 3f);
        }
    }
}
