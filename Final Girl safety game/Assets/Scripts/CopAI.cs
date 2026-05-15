using UnityEngine;
using UnityEngine.AI;

public class CopAI : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            GameObject p = GameObject.FindWithTag("Player");
            if (p != null)
                player = p.transform;
        }
    }

    void Update()
    {
        if (player == null) return;
        if (agent == null) return;
        if (!agent.isOnNavMesh) return;

        agent.SetDestination(player.position);
    }
}