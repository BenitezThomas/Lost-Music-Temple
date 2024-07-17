// - Ƹ̵̡Ӝ̵̨̄Ʒ - //
// Author: Han
// Last Update 7/11/2024 US

using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public GameObject player; // The collider that will be used as a collider
    public PianoPuzzleManager pianoPuzzleManager; // Reference to the PianoPuzzleManager
    [SerializeField] AK.Wwise.Event PlayKeynote;
    [SerializeField] AK.Wwise.Event StopKeynote;

    void Start()
    {
        // Get the PianoPuzzleManager component from the grandparent object
        pianoPuzzleManager = this.transform.parent.parent.GetComponent<PianoPuzzleManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("Key pressed: " + gameObject.name);
            PlayKeynote.Post(gameObject);
            
            // Register the key press with the PianoPuzzleManager
            pianoPuzzleManager.RegisterKeyPress(gameObject.transform.parent.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            StopKeynote.Post(gameObject);
        }


    }

}

