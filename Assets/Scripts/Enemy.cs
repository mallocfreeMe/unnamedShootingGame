using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    private NavMeshAgent _pathfinder;
    private Transform _target;

    protected override void Start()
    {
        base.Start();
        _pathfinder = GetComponent<NavMeshAgent>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(UpdatePath());
    }

    private void Update()
    {
    }

    private IEnumerator UpdatePath()
    {
        var refreshRate = 0.25f;

        while (_target != null)
        {
            var targetPosition = new Vector3(_target.position.x, 0, _target.position.z);
            if (!dead)
            {
                _pathfinder.SetDestination(targetPosition);
            }

            yield return new WaitForSeconds(refreshRate);
        }
    }
}