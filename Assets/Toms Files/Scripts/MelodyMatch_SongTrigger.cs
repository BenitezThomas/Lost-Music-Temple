using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyMatch_SongTrigger : MonoBehaviour
{
    [SerializeField] AK.Wwise.Event blockSound;
    [SerializeField] AK.Wwise.Event song;
    [SerializeField] Animator animator;

    bool isPlayingSong = false;
    bool isInDefaultPosition = true;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlayingSong)
        {
            blockSound.Post(gameObject);
            song.Post(gameObject);
            animator.Play("PlateDown");

            isInDefaultPosition = false;
            isPlayingSong = true;

            StartCoroutine(RefreshSongTimer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isInDefaultPosition)
        {
            animator.Play("PlateUp");
            blockSound.Post(gameObject);
            isInDefaultPosition = true;
        }
    }

    IEnumerator RefreshSongTimer()
    {
        yield return new WaitForSeconds(5f);

        isPlayingSong = false;

        if (!isInDefaultPosition)
        {
            animator.Play("PlateUp");
            blockSound.Post(gameObject);
            isInDefaultPosition = true;
        }
    }
}
