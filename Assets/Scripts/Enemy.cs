using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    public enum State
    {
        Idle,
        Chasing,
        Attacking
    };

    private State _currentState;

    private NavMeshAgent _pathfinder;
    private Transform _target;
    private Material _skinMaterial;

    private Color _originalColour;

    private float _attackDistanceThreshold = .5f;
    private float _timeBetweenAttacks = 1;

    private float _nextAttackTime;
    private float _myCollisionRadius;
    private float _targetCollisionRadius;

    protected override void Start()
    {
        base.Start();
        _pathfinder = GetComponent<NavMeshAgent>();
        _skinMaterial = GetComponent<Renderer>().material;
        _originalColour = _skinMaterial.color;

        _currentState = State.Chasing;
        _target = GameObject.FindGameObjectWithTag("Player").transform;

        _myCollisionRadius = GetComponent<CapsuleCollider>().radius;
        _targetCollisionRadius = _target.GetComponent<CapsuleCollider>().radius;

        StartCoroutine(UpdatePath());
    }

    void Update()
    {
        if (Time.time > _nextAttackTime)
        {
            var sqrDstToTarget = (_target.position - transform.position).sqrMagnitude;
            if (sqrDstToTarget < Mathf.Pow(_attackDistanceThreshold + _myCollisionRadius + _targetCollisionRadius, 2))
            {
                _nextAttackTime = Time.time + _timeBetweenAttacks;
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        _currentState = State.Attacking;
        _pathfinder.enabled = false;

        var originalPosition = transform.position;
        var dirToTarget = (_target.position - transform.position).normalized;
        var attackPosition = _target.position - dirToTarget * (_myCollisionRadius);

        float attackSpeed = 3;
        float percent = 0;

        _skinMaterial.color = Color.red;

        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            var interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

            yield return null;
        }

        _skinMaterial.color = _originalColour;
        _currentState = State.Chasing;
        _pathfinder.enabled = true;
    }

    IEnumerator UpdatePath()
    {
        var refreshRate = .25f;

        while (_target != null)
        {
            if (_currentState == State.Chasing)
            {
                Vector3 dirToTarget = (_target.position - transform.position).normalized;
                Vector3 targetPosition = _target.position -
                                         dirToTarget * (_myCollisionRadius + _targetCollisionRadius +
                                                        _attackDistanceThreshold / 2);
                if (!dead)
                {
                    _pathfinder.SetDestination(targetPosition);
                }
            }

            yield return new WaitForSeconds(refreshRate);
        }
    }
}