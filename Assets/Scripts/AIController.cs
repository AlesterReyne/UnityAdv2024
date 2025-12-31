using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class AIController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform[] listOfWaypoints;
    [SerializeField] private GameObject popUpMessage;
    [SerializeField] private UnityEvent<string> onArrived;
    [SerializeField] private string animationName = "isWalking";

    private Animator _animator;
    private int _prIndex;
    private bool _isArrived;
    private bool _isStarted;


    void Start()
    {
        _animator = GetComponent<Animator>();
        _prIndex = 0;
        _isArrived = false;
        _isStarted = false;
    }

    void Update()
    {
        PlayerPressedButton();
        IsArrived();
        AnimationHandler();
    }

    private void SetDestination(Transform targetTransformWaypoint)
    {
        if (navMeshAgent == null) return;

        navMeshAgent.SetDestination(targetTransformWaypoint.position);
    }

    private void PlayerPressedButton()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isStarted = true;
            SetDestination(listOfWaypoints[_prIndex]);
        }
    }

    private void IsArrived()
    {
        if (_isStarted && !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            onArrived.Invoke(this.gameObject.name);
            _isArrived = true;
            _isStarted = false;
        }
    }

    private void AnimationHandler()
    {
        if (!_isArrived && _isStarted)
        {
            _animator.SetBool(animationName, true);
        }
        else if (_isArrived && !_isStarted)
        {
            _animator.SetBool(animationName, false);
        }
    }
}