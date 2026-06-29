using System.Collections;
using UnityEngine;

public class FishRide : NPC
{
    [Header("Ride Settings")]
    [SerializeField] private Transform seatPoint;
    [SerializeField] private Transform pointB;
    [SerializeField] private Transform dropOffPoint;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float mountDelay = 1f;

    [Header("Stick Offset")]
    [SerializeField] private Vector3 rideOffset = new Vector3(0, 1f, 0);

    private bool isMoving;
    private bool rideCompleted;
    private bool riding;

    private GameObject player;
    private CharacterController3D playerMovement;

    protected override void Start()
    {
        base.Start(); // Registers this NPC with the DialogueManager

        onBeginDialogue += HandleDialogue;
    }

    private void OnDisable()
    {
        onBeginDialogue -= HandleDialogue;
    }

    private void HandleDialogue()
    {
        if (rideCompleted || riding)
            return;

        if (DialogueManager.Instance == null)
            return;

        string npcName = GetRefName();

        if (!DialogueManager.Instance.m_npcDictionary.ContainsKey(npcName))
        {
            Debug.LogWarning($"FishRide: NPC '{npcName}' is not registered.");
            return;
        }

        int dialogueIndex = DialogueManager.Instance.m_npcDictionary[npcName];

        if (!DialogueManager.Instance.m_dialogueDictionary.ContainsKey((npcName, dialogueIndex)))
        {
            Debug.LogWarning($"FishRide: Dialogue ({npcName}, {dialogueIndex}) not found.");
            return;
        }

        riding = true;
        StartCoroutine(WaitThenRide());
    }

    private IEnumerator WaitThenRide()
    {
        yield return new WaitUntil(() =>
            DialogueManager.Instance != null &&
            DialogueManager.Instance.isdialogueActive);

        yield return new WaitUntil(() =>
            DialogueManager.Instance == null ||
            !DialogueManager.Instance.isdialogueActive);

        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && !rideCompleted)
        {
            yield return StartCoroutine(RideFish());
        }

        riding = false;
    }

    private IEnumerator RideFish()
    {
        isMoving = true;

        playerMovement = player.GetComponent<CharacterController3D>();

        if (playerMovement != null)
        {
            playerMovement.canMove = false;
            playerMovement.enableJump = false;
        }

        // Snap player to seat
        player.transform.position = seatPoint.position;
        player.transform.rotation = seatPoint.rotation;

        yield return new WaitForSeconds(mountDelay);

        // Move fish and player together
        while (Vector3.Distance(transform.position, pointB.position) > 0.05f)
        {
            Vector3 nextFishPos = Vector3.MoveTowards(
                transform.position,
                pointB.position,
                moveSpeed * Time.deltaTime
            );

            Vector3 delta = nextFishPos - transform.position;

            transform.position = nextFishPos;

            player.transform.position += delta;

            yield return null;
        }

        transform.position = pointB.position;

        rideCompleted = true;
        isMoving = false;

        DropPlayer();
    }

    private void DropPlayer()
    {
        StopAllCoroutines();

        if (player != null)
        {
            if (dropOffPoint != null)
            {
                player.transform.position = dropOffPoint.position;
                player.transform.rotation = dropOffPoint.rotation;
            }
            else
            {
                player.transform.position = transform.position + transform.right * 2f;
            }
        }

        if (playerMovement != null)
        {
            playerMovement.canMove = true;
            playerMovement.enableJump = true;
        }
    }
}