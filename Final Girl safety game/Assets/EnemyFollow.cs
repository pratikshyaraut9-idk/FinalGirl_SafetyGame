using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;     // Player object
    public float speed = 3f;     // Enemy speed
    public float stopDistance = 2f; // Stop distance

    void Update()
    {
        // Distance between enemy and player
        float distance = Vector3.Distance(transform.position, player.position);

        // If player is farther than stop distance
        if(distance > stopDistance)
        {
            // Move toward player
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );
        }
    }
}