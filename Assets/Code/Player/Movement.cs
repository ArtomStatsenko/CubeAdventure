﻿using UnityEngine;
using UnityEngine.AI;

public sealed class Movement
{
    private Transform _exitPoint;
    NavMeshAgent _navMeshAgent;
    private float _turnSpeedParam = 100f;
    private float _acceleration = 50f;

    public Movement(NavMeshAgent navMeshAgent, float moveSpeed, float turnSpeed)
    {
        _navMeshAgent = navMeshAgent;
        _navMeshAgent.speed = moveSpeed;
        _navMeshAgent.angularSpeed = turnSpeed * _turnSpeedParam;
        _navMeshAgent.acceleration = _acceleration;
        _exitPoint = Object.FindObjectOfType<Exit>().transform;
    }

    public void SetDestination()
    {
        _navMeshAgent.destination = _exitPoint.position;
    }

    public void EnableMovement()
    {
        _navMeshAgent.isStopped = false;
    }

    public void DisableMovement()
    {
        _navMeshAgent.isStopped = true;
    }
}
