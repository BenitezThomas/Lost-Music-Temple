using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PuzzleCameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera puzzleCamera;
    [SerializeField] OpenGate puzzleGate;

    [SerializeField] Transform gatePieces;

    bool isPlayerNear = false;
    bool isPuzzleFinished = false;

    private void Update()
    {
        if (!isPuzzleFinished)
        {
            if (isPlayerNear && Input.GetKey(KeyCode.E)) puzzleCamera.m_Priority = 10;
            else puzzleCamera.m_Priority = 0;
        } 
    }

    public IEnumerator FinishPuzzle()
    {
        isPuzzleFinished = true;

        puzzleCamera.m_Priority = 10;

        yield return new WaitForSeconds(2f);

        puzzleGate.TryOpenGate();
        gatePieces.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        puzzleCamera.m_Priority = 0;
    }

    //Detection of player proximity
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("collider");
        }
    }

    //Player move away detection
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
