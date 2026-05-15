using UnityEngine;

public class HouseTrigger : MonoBehaviour
{
    // Drag your WinPanel directly into this field in the Inspector
    public GameObject winPanel;
    
    // Optional: Reference to disable player movement
    public GameObject player;
    public MonoBehaviour[] scriptsToDisable;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name);
        
        // Check if the object entering is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER reached the safe house!");
            
            // Show win panel
            if (winPanel != null)
            {
                winPanel.SetActive(true);
                Debug.Log("WIN PANEL ACTIVATED!");
            }
            else
            {
                Debug.LogError("Win Panel is not assigned in HouseTrigger!");
                // Try to find it automatically
                winPanel = GameObject.Find("WinPanel");
                if (winPanel != null)
                {
                    winPanel.SetActive(true);
                    Debug.Log("Found and activated WinPanel automatically!");
                }
            }
            
            // Pause the game
            Time.timeScale = 0f;
            
            // Disable player movement
            if (player != null)
            {
                CharacterController controller = player.GetComponent<CharacterController>();
                if (controller != null) controller.enabled = false;
            }
            
            // Disable any movement scripts
            foreach (MonoBehaviour script in scriptsToDisable)
            {
                if (script != null) script.enabled = false;
            }
        }
    }
}