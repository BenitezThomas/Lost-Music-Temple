using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melody_Match_Object : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private int melodyNumber = 0;

    private Melody_Match_Controller melodyMatchController;
    private bool isPlayingMelody = false;

    // Start is called before the first frame update
    void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        melodyMatchController = this.GetComponentInParent<Melody_Match_Controller>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if (!isPlayingMelody)
            {
                melodyMatchController.AddMelody(melodyNumber);
                StartPlayingMelody();
            }
            else
            {
                melodyMatchController.RemoveMelody(melodyNumber);
                StopPlayingMelody();
            }
        }
    }

    public void StartPlayingMelody()
    {
        isPlayingMelody = true;

        if (audioSource == null)
            Debug.Log("Audio source missing for: " + gameObject.name);
        else
            audioSource.Play();
    }

    public void StopPlayingMelody()
    {
        isPlayingMelody = false;

        if (audioSource == null)
            Debug.Log("Audio source missing for: " + gameObject.name);
        else
            audioSource.Stop();
    }

    public bool GetIsPlayingMelody()
    {
        return isPlayingMelody;
    }
}
