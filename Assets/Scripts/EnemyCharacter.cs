using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyCharacter : MonoBehaviour
{
    [SerializeField] Animator animator;

    private Camera mainCamera;
    private NavMeshAgent navMeshAgent;
    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        mainCamera = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();
        GetComponent<Health>().onDeath += OnDeath;
    }

    void Update()
    {
        navMeshAgent.SetDestination(target.position);
        animator.SetFloat("speed", navMeshAgent.velocity.magnitude);
    }

    void OnDeath()
    {
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(4.0f);
        Destroy(gameObject);
    }
}
