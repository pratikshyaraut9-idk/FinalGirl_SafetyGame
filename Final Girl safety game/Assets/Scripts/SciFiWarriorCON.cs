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
    public GameObject warningText;
    public GameObject correctText;

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

        // SHOW CHOICE
        if (distance <= stopDistance && !choiceShown)
        {
            choiceShown = true;

            if (startText != null) startText.SetActive(false);
            if (alertText != null) alertText.SetActive(true);

            StartCoroutine(ShowChoices());
        }

        // ⭐ KEYBOARD INPUT (I / O)
        if (choicePanel != null && choicePanel.activeSelf && !choiceMade)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                ChooseA_Stranger();
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                ChooseB_Police();
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
    // A = WARNING (I KEY)
    // =========================
    public void ChooseA_Stranger()
    {
        if (choiceMade) return;
        choiceMade = true;

        if (choicePanel != null)
            choicePanel.SetActive(false);

        if (warningText != null)
            warningText.SetActive(true);

        if (correctText != null)
            correctText.SetActive(false);

        Debug.Log("CHOICE A (I KEY) - WARNING");
    }

    // =========================
    // B = CORRECT (O KEY)
    // =========================
    public void ChooseB_Police()
    {
        if (choiceMade) return;
        choiceMade = true;

        if (choicePanel != null)
            choicePanel.SetActive(false);

        if (correctText != null)
            correctText.SetActive(true);

        if (warningText != null)
            warningText.SetActive(false);

        Debug.Log("CHOICE B (O KEY) - CORRECT");
    }
}