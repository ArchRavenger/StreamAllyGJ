using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Enemynav : MonoBehaviour
{
    public int detectionRadius = 200;
    public int offsetFromFloor = 5;

    private GameObject _player;
    private NavMeshAgent _agent;
    
    private GameObject _actualPathPointTarget;
    private GameObject[] _pathPoints;

    private GameObject _target;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _agent = GetComponent<NavMeshAgent>();

        _pathPoints = GameObject.FindGameObjectsWithTag("PathPoint");
        _actualPathPointTarget = _pathPoints[Random.Range(0, _pathPoints.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTarget();
        MoveToTarget();
    }

    private void UpdateTarget()
    {
        //todo update flag when player is in save zone
        bool isPlayerInSavetyZone = false;

        if (Vector3.Distance(transform.position, _player.transform.position) < detectionRadius && !isPlayerInSavetyZone)
        {
            _target = _player;
        }
        else
        {
            if (Vector3.Distance(transform.position, _actualPathPointTarget.transform.position) < 2)
            {
                _actualPathPointTarget = _pathPoints[Random.Range(0, _pathPoints.Length)];
            }

            _target = _actualPathPointTarget;
        }
    }

    private void MoveToTarget()
    {
        var distanceToFlor = 0f;
        RaycastHit hit;
        if (Physics.Raycast(_target.transform.position, Vector3.down, out hit))
        {
            distanceToFlor = hit.distance;
        }

        var transformPosition = _target.transform.position;
        transformPosition.y = transformPosition.y - distanceToFlor;
        _agent.destination = transformPosition;
        
        AdjustHeight();
    }

    private void AdjustHeight()
    {
        var heightDelta = 0f;
        if (_target.transform.position.y < transform.position.y)
        {
            heightDelta = -0.1f;
        }
        else
        {
            heightDelta = 0.1f;
        }

        _agent.baseOffset = Mathf.Max(_agent.baseOffset + heightDelta, offsetFromFloor);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.DrawLine(transform.position, _target.transform.position);
    }
}