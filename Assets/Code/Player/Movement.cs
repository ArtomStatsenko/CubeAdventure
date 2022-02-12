using UnityEngine;
using UnityEngine.AI;

public sealed class Movement
{
    private NavMeshAgent _navMeshAgent;
    private float _turnSpeedParam = 100f;
    private float _acceleration = 50f;

    public Movement(NavMeshAgent navMeshAgent, float moveSpeed, float turnSpeed)
    {
        _navMeshAgent = navMeshAgent;
        _navMeshAgent.speed = moveSpeed;
        _navMeshAgent.angularSpeed = turnSpeed * _turnSpeedParam;
        _navMeshAgent.acceleration = _acceleration;
        _navMeshAgent.autoBraking = false;
    }

    public void SetDestination()
    {
        var exit = Object.FindObjectOfType<Exit>().transform.position;
        _navMeshAgent.ResetPath();
        _navMeshAgent.SetDestination(new Vector3(exit.x, 1f, exit.z));
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
