using UnityEngine;

public class SciFiWarriorCON : MonoBehaviour
{
    // Reference to the player
    private GameObject player;

    // Enemy speed
    [SerializeField]
    private float moveSpeed = 2.0f;

    // Distance to detect player
    [SerializeField]
    private float chaseDistance = 15f;

    // Distance to stop near player
    [SerializeField]
    private float stopDistance = 2f;

    // Animator reference
    private Animator anim;

    // Animator parameter hash
    private int speedHash = Animator.StringToHash("Speed");

    void Start()
    {
        // Get Animator
        anim = GetComponent<Animator>();

        // Find player with Player tag
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        // If player not found
        if (player == null)
            return;

        // Calculate distance
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Chase player
        if (distance < chaseDistance && distance > stopDistance)
        {
            // Play walk animation
            anim.SetFloat(speedHash, 0.7f);

            // Look at player
            Vector3 targetPos = player.transform.position;
            targetPos.y = transform.position.y;

            transform.LookAt(targetPos);

            // Move forward
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            Debug.Log("Enemy Moving");
        }
        else
        {
            // Stop animation
            anim.SetFloat(speedHash, 0f);
        }
    }
}