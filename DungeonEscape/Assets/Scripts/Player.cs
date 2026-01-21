using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Vector3 areaCenter = Vector3.zero;
    [SerializeField] private Vector3 areaExtents = new Vector3(5f, 0f, 5f);
    [SerializeField] private float targetReachThreshold = 0.1f;
    [SerializeField] private float retargetInterval = 3f;

    private Vector3 _currentTarget;
    private float _nextRetargetTime;
    private float _fixedY;

    void Awake()
    {
        _fixedY = transform.position.y;
        PickNewTarget();
        _nextRetargetTime = Time.time + retargetInterval;
    }

    void Update()
    {
        if (Time.time >= _nextRetargetTime || IsAtTarget())
        {
            PickNewTarget();
            _nextRetargetTime = Time.time + retargetInterval;
        }

        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (_currentTarget - transform.position).normalized;
        Vector3 step = direction * moveSpeed * Time.deltaTime;

        // Keep Y locked to the initial height.
        Vector3 nextPosition = transform.position + step;
        nextPosition.y = _fixedY;
        transform.position = nextPosition;
    }

    private bool IsAtTarget()
    {
        Vector3 flatPosition = new Vector3(transform.position.x, _fixedY, transform.position.z);
        return Vector3.Distance(flatPosition, _currentTarget) <= targetReachThreshold;
    }

    private void PickNewTarget()
    {
        float x = Random.Range(areaCenter.x - areaExtents.x, areaCenter.x + areaExtents.x);
        float z = Random.Range(areaCenter.z - areaExtents.z, areaCenter.z + areaExtents.z);
        _currentTarget = new Vector3(x, _fixedY, z);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 center = new Vector3(areaCenter.x, Application.isPlaying ? _fixedY : transform.position.y, areaCenter.z);
        Vector3 size = new Vector3(areaExtents.x * 2f, 0.05f, areaExtents.z * 2f);
        Gizmos.DrawWireCube(center, size);
        Gizmos.DrawSphere(_currentTarget, 0.1f);
    }
}
