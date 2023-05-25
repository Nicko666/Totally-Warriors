using UnityEngine;
using UnityEngine.AI;

public class WarriorSpeedControl : MonoBehaviour
{
    [SerializeField] NavMeshAgent _agent;

    float _basicSpeed;
    int _slowDown;

    public void Inst(float speed)
    {
        _basicSpeed = speed;
        _slowDown = 1 << NavMesh.GetAreaFromName("SlowDown");

    }

    void Update()
    {
        if (!_agent.enabled) return;

        NavMeshHit hit;

        if (!_agent.SamplePathPosition(NavMesh.AllAreas, 1.0f, out hit))
        {               
            if ((hit.mask & _slowDown) != 0)
            {
                _agent.speed = _basicSpeed / _agent.GetAreaCost(NavMesh.GetAreaFromName("SlowDown"));

                return;
            }
        }

        _agent.speed = _basicSpeed;

    }
}
