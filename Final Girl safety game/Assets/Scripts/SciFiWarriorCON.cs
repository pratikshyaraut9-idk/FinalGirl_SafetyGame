using UnityEngine;

public class SciFiWarriorCON : MonoBehaviour
{
    public Transform player;
    public GameObject copPrefab;

    public float triggerDistance = 3f;

    private bool copSpawned = false;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= triggerDistance && !copSpawned)
        {
            copSpawned = true;

            GameObject cop = Instantiate(copPrefab, transform.position, Quaternion.identity);

            CopAI copAI = cop.GetComponent<CopAI>();

            if (copAI != null)
            {
                copAI.player = player;
            }
        }
    }
}