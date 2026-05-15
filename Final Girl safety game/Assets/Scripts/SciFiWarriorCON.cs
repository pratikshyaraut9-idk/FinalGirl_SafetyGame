using UnityEngine;

public class SciFiWarriorCON : MonoBehaviour
{
    [Header("Movement")]
    public Transform player;
    public float moveSpeed = 7f;
    public float stopDistance = 6f;
    public float startDelay = 5f;

    [Header("UI")]
    public GameObject startText;
    public GameObject alertText;
    public GameObject choicePanel;
    public GameObject warningText;
    public GameObject correctText;
    public GameObject winPanel;
    public GameObject losePanel;

    [Header("House")]
    public Transform houseGoal;
    public Transform policeBooth;

    private bool choiceMade = false;
    private bool hasChosenCorrectly = false;
    private bool gameEnded = false;
    private bool chaseStarted = false;

    void Start()
    {
        if (choicePanel != null)
            choicePanel.SetActive(false);

        if (warningText != null)
            warningText.SetActive(false);

        if (correctText != null)
            correctText.SetActive(false);

        if (winPanel != null)
            winPanel.SetActive(false);

        if (losePanel != null)
            losePanel.SetActive(false);

        Invoke("StartChasing", startDelay);
    }

    void StartChasing()
    {
        chaseStarted = true;
        Debug.Log("Enemy started chasing");
    }

    void Update()
    {
        if (gameEnded)
            return;

        if (chaseStarted && !choiceMade && player != null)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime
            );

            transform.LookAt(player);

            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= stopDistance)
            {
                choiceMade = true;

                if (choicePanel != null)
                    choicePanel.SetActive(true);

                Debug.Log("Choice panel opened");
            }
        }

        if (choiceMade)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("Pressed I");

                if (warningText != null)
                    warningText.SetActive(true);

                if (losePanel != null)
                    losePanel.SetActive(true);

                gameEnded = true;
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                Debug.Log("Pressed O");

                hasChosenCorrectly = true;

                if (correctText != null)
                    correctText.SetActive(true);

                if (choicePanel != null)
                    choicePanel.SetActive(false);

                // Disable enemy so player can walk safely
                chaseStarted = false;
                
                Debug.Log("Correct choice made! Walk to the house to win.");
            }
        }
    }

    // THIS METHOD IS REQUIRED - HouseTrigger calls this
    public void OnReachedHouse()
    {
        Debug.Log("OnReachedHouse called");

        if (hasChosenCorrectly && !gameEnded)
        {
            gameEnded = true;

            if (winPanel != null)
            {
                winPanel.SetActive(true);
                Debug.Log("WIN PANEL SHOWN - Player reached the house!");
            }
        }
        else if (!hasChosenCorrectly)
        {
            Debug.Log("Player reached house but didn't make correct choice yet");
        }
    }
}