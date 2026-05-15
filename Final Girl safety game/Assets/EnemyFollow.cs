using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;

    public float speed = 3f;

    public float followDistance = 15f;

    void Update()
    {
        // Distance check
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < followDistance)
        {
            // Target position
            Vector3 targetPosition = new Vector3(
                player.position.x,
                transform.position.y,
                player.position.z
            );

            // Direction to player
            Vector3 direction = (targetPosition - transform.position).normalized;

            // Rotate smoothly
            transform.forward = direction;

            // Move toward player
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}