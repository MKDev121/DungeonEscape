using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 4f;
    [SerializeField] private float attackCooldown = 1.5f;

    private Transform _player;
    private float _nextAttackTime;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (_player == null)
        {
            Debug.LogWarning("Enemy could not find a GameObject with the Player tag.");
        }
    }

    void Update()
    {
        if (_player == null)
        {
            return;
        }

        float sqrDistanceToPlayer = (_player.position - transform.position).sqrMagnitude;
        float sqrDetectionRadius = detectionRadius * detectionRadius;

        if (sqrDistanceToPlayer <= sqrDetectionRadius)
        {
            TryAttack();
        }
    }

    // Logs an attack placeholder that can later be replaced with an animation trigger.
    private void TryAttack()
    {
        if (Time.time < _nextAttackTime)
        {
            return;
        }

        _nextAttackTime = Time.time + attackCooldown;
        Debug.Log($"Enemy {name} attacks!");
        // To trigger an animation later:
        //  animator.SetTrigger("Attack");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
