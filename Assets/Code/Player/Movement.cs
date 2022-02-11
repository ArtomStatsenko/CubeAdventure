using UnityEngine;
using UnityEngine.AI;

public sealed class Movement
{
    private Transform _exitPoint;
    NavMeshAgent _navMeshAgent;
    private float _turnSpeedParam = 100f;

    public Movement(NavMeshAgent navMeshAgent, float moveSpeed, float turnSpeed)
    {
        _navMeshAgent = navMeshAgent;
        _navMeshAgent.speed = moveSpeed;
        _navMeshAgent.angularSpeed = turnSpeed * _turnSpeedParam;
        _exitPoint = Object.FindObjectOfType<ExitMarker>().transform;
        _navMeshAgent.destination = _exitPoint.position;
    }

    public void Move(bool isPaused)
    {
        _navMeshAgent.isStopped = isPaused;
    }
}
