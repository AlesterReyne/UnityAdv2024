using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform[] listOfWaypoints;
    [SerializeField] private int prIndex;

    private bool _isArrived = false;
    private bool _isMoving;
    private bool _canMove = false;


    void Start()
    {
        prIndex = -1;
        if (navMeshAgent == null || listOfWaypoints == null) return;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ToggleMoving(true);
            if (_isMoving && !navMeshAgent.isStopped && navMeshAgent.remainingDistance <= 0.1f) _isArrived = true;

            if (_isArrived)
            {
                prIndex++;
                if (prIndex >= listOfWaypoints.Length)
                {
                    prIndex = 0;
                }

                SetDestination(listOfWaypoints[prIndex]);
                _isArrived = false;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (!(_isMoving && !navMeshAgent.isStopped && navMeshAgent.remainingDistance <= 0.1f))
            {
                prIndex--;
            }

            ToggleMoving(false);
        }
    }

    public void SetDestination(Transform targetTransformWaypoint)
    {
        if (navMeshAgent == null)
        {
            return;
        }

        navMeshAgent.SetDestination(targetTransformWaypoint.position);
    }

    public void ToggleMoving(bool shouldMove)
    {
        _isMoving = shouldMove;
        if (navMeshAgent) navMeshAgent.enabled = _isMoving;
    }
}