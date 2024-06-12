using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melody_Match_Object : MonoBehaviour
{
    public AudioSource audioSource;
    private Melody_Match_Controller melodyMatchController;
    
    public int melodyNumber = 0;
    private int currentArrayIndex = -1;

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
            if (currentArrayIndex < 0)
            {
                currentArrayIndex = melodyMatchController.GetMelodyArrayIndex();
                melodyMatchController.AddMelody(melodyNumber);
                StartPlayingMelody();
            }
            else
            {
                melodyMatchController.RemoveMelody(currentArrayIndex);
                currentArrayIndex = -1;
                StopPlayingMelody();
            }
        }
    }

    public void StartPlayingMelody()
    {
        if (audioSource == null)
            Debug.Log("Audio source missing for: " + gameObject.name);
        else
            audioSource.Play();

        Debug.Log(gameObject.name + ": PLAYING MELODY");
    }

    public void StopPlayingMelody()
    {
        if (audioSource == null)
            Debug.Log("Audio source missing for: " + gameObject.name);
        else
            audioSource.Stop();

        Debug.Log(gameObject.name + ": STOPPING MELODY");
    }
}
