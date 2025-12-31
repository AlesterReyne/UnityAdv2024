using System;
using UnityEngine;
using UnityEngine.AI;

public class MoveToMouse : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float sampleDistance = 0.5f;
    [SerializeField] LayerMask groundLayer;

    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = moveSpeed;
    }

    private void Update()
    {
        MoveToMouseHandler();
    }

    private void MoveToMouseHandler()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, groundLayer))
            {
                if (NavMesh.SamplePosition(hit.point, out NavMeshHit hitNavMeshHit, sampleDistance, NavMesh.AllAreas))
                {
                    _agent.SetDestination(hitNavMeshHit.position);
                }
                else
                    Debug.Log(
                        $"Ray hit {hit.collider.name} at {hit.point}, but no NavMesh was found within {sampleDistance} units.");
            }
            else Debug.Log("Raycast didn't hit anything on the ground layer.");
        }
    }
}