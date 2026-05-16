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

    [Header("Follow Settings")]
    [SerializeField] private float followSpeed = 3f;
    [SerializeField] private float catchDistance = 1.5f;

    [Header("Start Delay")]
    [SerializeField] private float startDelay = 5f;

    [Header("UI")]
    public GameObject startText;
    public GameObject alertText;
    public GameObject choicePanel;

    public GameObject warningText;
    public GameObject correctText;

    public GameObject loseText;
    public GameObject winText;

    [Header("Home")]
    public Transform homePoint;
    public float winDistance = 3f;

    private bool canMove = false;
    private bool choiceShown = false;
    private bool choiceMade = false;
    private bool strangerFollowing = false;
    private bool gameEnded = false;

    private int speedHash = Animator.StringToHash("Speed");

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");

        StartCoroutine(StartAfterDelay());

        // UI setup
        if (startText != null) startText.SetActive(true);
        if (alertText != null) alertText.SetActive(false);
        if (choicePanel != null) choicePanel.SetActive(false);

        if (warningText != null) warningText.SetActive(false);
        if (correctText != null) correctText.SetActive(false);

        if (loseText != null) loseText.SetActive(false);
        if (winText != null) winText.SetActive(false);
    }

    IEnumerator StartAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);
        canMove = true;
    }

    void Update()
    {
        if (player == null || gameEnded)
            return;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        // =========================
        // NPC MOVE BEFORE CHOICE
        // =========================
        if (!strangerFollowing && canMove)
        {
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
        }

        // =========================
        // SHOW CHOICE
        // =========================
        if (distance <= stopDistance && !choiceShown)
        {
            choiceShown = true;

            if (startText != null)
                startText.SetActive(false);

            if (alertText != null)
                alertText.SetActive(true);

            StartCoroutine(ShowChoices());
        }

        // =========================
        // INPUT (I / O)
        // =========================
        if (choicePanel != null && choicePanel.activeSelf && !choiceMade)
        {
            if (Input.GetKeyDown(KeyCode.I))
                ChooseA_Stranger();

            if (Input.GetKeyDown(KeyCode.O))
                ChooseB_Police();
        }

        // =========================
        // FOLLOW SYSTEM (A ONLY)
        // =========================
        if (strangerFollowing)
        {
            Vector3 targetPos = player.transform.position;
            targetPos.y = transform.position.y;

            transform.LookAt(targetPos);
            transform.position += transform.forward * followSpeed * Time.deltaTime;

            float catchDist = Vector3.Distance(transform.position, player.transform.position);

            if (catchDist <= catchDistance)
            {
                gameEnded = true;

                if (loseText != null)
                    loseText.SetActive(true);

                Debug.Log("LOSE - CAUGHT");
            }
        }

        // =========================
        // WIN CONDITION
        // =========================
        if (homePoint != null)
        {
            float homeDist = Vector3.Distance(player.transform.position, homePoint.position);

            if (homeDist <= winDistance)
            {
                gameEnded = true;

                if (winText != null)
                    winText.SetActive(true);

                Debug.Log("WIN - SAFE");
            }
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
    // AUTO HIDE UI FUNCTION
    // =========================
    IEnumerator HideAfterSeconds(GameObject uiObject, float time)
    {
        yield return new WaitForSeconds(time);

        if (uiObject != null)
            uiObject.SetActive(false);
    }

    // =========================
    // A = DANGER
    // =========================
    public void ChooseA_Stranger()
    {
        if (choiceMade) return;

        choiceMade = true;

        if (choicePanel != null)
            choicePanel.SetActive(false);

        StartCoroutine(WarningThenFollow());
    }

    IEnumerator WarningThenFollow()
    {
        if (warningText != null)
            warningText.SetActive(true);

        // AUTO HIDE WARNING
        StartCoroutine(HideAfterSeconds(warningText, 3f));

        yield return new WaitForSeconds(3f);

        strangerFollowing = true;
    }

    // =========================
    // B = SAFE
    // =========================
    public void ChooseB_Police()
    {
        if (choiceMade) return;

        choiceMade = true;

        if (choicePanel != null)
            choicePanel.SetActive(false);

        if (correctText != null)
            correctText.SetActive(true);

        // AUTO HIDE SAFE MESSAGE
        StartCoroutine(HideAfterSeconds(correctText, 3f));

        strangerFollowing = false;
    }
}