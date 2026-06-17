using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class CameraCastleDynamic : MonoBehaviour
{
    [Header("Player")]
    public CharacterController3D player;

    [Header("Cameras")]
    public CinemachineCamera playerCam;
    public CinemachineCamera cam1;
    public CinemachineCamera cam2;
    public CinemachineCamera cam3;
    public CinemachineCamera cam4;

    [Header("Timing")]
    public float shotTime = 0.5f;
    public float finalHoldTime = 2f;

    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasPlayed) return;

        if (other.CompareTag("Player"))
        {
            hasPlayed = true;
            StartCoroutine(PlaySequence());
        }
    }

    IEnumerator PlaySequence()
    {
        // Freeze player
        if (player != null)
            player.canMove = false;

        Activate(cam1);
        yield return new WaitForSeconds(shotTime);

        Activate(cam2);
        yield return new WaitForSeconds(shotTime);

        Activate(cam3);
        yield return new WaitForSeconds(shotTime);

        Activate(cam4);
        yield return new WaitForSeconds(finalHoldTime);

        Activate(playerCam);

        // Unfreeze player
        if (player != null)
            player.canMove = true;
    }

    void Activate(CinemachineCamera activeCam)
    {
        playerCam.Priority = 0;
        cam1.Priority = 0;
        cam2.Priority = 0;
        cam3.Priority = 0;
        cam4.Priority = 0;

        activeCam.Priority = 100;
    }
}