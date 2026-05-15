using UnityEngine;
using System.Collections;

public class SciFiWarriorCON : MonoBehaviour
{
    private GameObject player;
    private Animator anim;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float chaseDistance = 15f;
    [SerializeField] private float stopDistance = 2f;

    [Header("Start Delay")]
    [SerializeField] private float startDelay = 5f;

    [Header("UI")]
    public GameObject startText;
    public GameObject alertText;
    public GameObject choicePanel;

    [Header("Result UI")]
    public GameObject warningText;   // A result
    public GameObject correctText;   // B result

    private bool canMove = false;
    private bool choiceShown = false;
    private bool choiceMade = false;

    private int speedHash = Animator.StringToHash("Speed");

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");

        StartCoroutine(StartAfterDelay());

        if (startText != null) startText.SetActive(true);
        if (alertText != null) alertText.SetActive(false);
        if (choicePanel != null) choicePanel.SetActive(false);

        if (warningText != null) warningText.SetActive(false);
        if (correctText != null) correctText.SetActive(false);
    }

    IEnumerator StartAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);
        canMove = true;
    }

    void Update()
    {
        if (player == null || !canMove)
            return;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        // NPC MOVE
        if (distance < chaseDistance && distance > stopDistance)
        {
            anim.SetFloat(speedHash, 0.7f);

            Vector3 targetPos = player.transform.position;
            targetPos.y = transform.position.y;

            transform.LookAt(targetPos);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetFloat(speedHash, 0f);
        }

        // SHOW CHOICES
        if (distance <= stopDistance && !choiceShown)
        {
            choiceShown = true;

            if (startText != null) startText.SetActive(false);
            if (alertText != null) alertText.SetActive(true);

            StartCoroutine(ShowChoices());
        }
    }

    IEnumerator ShowChoices()
    {
        yield return new WaitForSeconds(2f);

        if (alertText != null)
            alertText.SetActive(false);

        if (choicePanel != null)
            choicePanel.SetActive(true);
    }

    // =========================
    // CHOICE A (WARNING)
    // =========================
    public void ChooseA_Stranger()
    {
        if (choiceMade) return;
        choiceMade = true;

        Debug.Log("WARNING CHOICE");

        if (choicePanel != null)
            choicePanel.SetActive(false);

        if (warningText != null)
            warningText.SetActive(true);

        if (correctText != null)
            correctText.SetActive(false);
    }

    // =========================
    // CHOICE B (CORRECT)
    // =========================
    public void ChooseB_Police()
    {
        if (choiceMade) return;
        choiceMade = true;

        Debug.Log("SAFE CHOICE");

        if (choicePanel != null)
            choicePanel.SetActive(false);

        if (correctText != null)
            correctText.SetActive(true);

        if (warningText != null)
            warningText.SetActive(false);
    }
}