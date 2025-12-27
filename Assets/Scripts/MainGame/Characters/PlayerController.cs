using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform[] listOfWaypoints;
    [SerializeField] private GameObject popUpMessage;
    private TextMeshProUGUI _textMeshProUGUI;

    private int _prIndex;
    private bool _isArrived;
    private bool _isMoving;
    private bool _canMove;


    void Start()
    {
        _textMeshProUGUI = popUpMessage.GetComponent<TextMeshProUGUI>();

        _prIndex = 0;
        _isArrived = false;
        _isMoving = false;
        _canMove = false;

        SetDestination(listOfWaypoints[_prIndex]);
    }

    void Update()
    {
        MoveCycle();
    }

    private void MoveCycle()
    {
        if (navMeshAgent == null || listOfWaypoints == null || popUpMessage == null) return;

        ToggleMoving(true);

        if (_isMoving && !navMeshAgent.isStopped && navMeshAgent.remainingDistance <= 0.1f) _isArrived = true;

        if (_isArrived)
        {
            _prIndex++;
            if (_prIndex >= listOfWaypoints.Length)
            {
                _prIndex = 0;
            }

            //SetDestination(listOfWaypoints[_prIndex]);
            _isArrived = false;

            ToggleMoving(false);
            _textMeshProUGUI.text = this.gameObject.name;
            popUpMessage.SetActive(true);
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