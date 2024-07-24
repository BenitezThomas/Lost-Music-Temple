// - Ƹ̵̡Ӝ̵̨̄Ʒ - //
// Author: Han
// Last Update 7/11/2024 US

using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public GameObject player; // The collider that will be used as a collider
    public PianoPuzzleManager pianoPuzzleManager; // Reference to the PianoPuzzleManager
    [SerializeField] Animator animator;

    void Start()
    {
        // Get the PianoPuzzleManager component from the grandparent object
        //pianoPuzzleManager = this.transform.parent.parent.GetComponent<PianoPuzzleManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("Key pressed: " + gameObject.name);

            animator.SetTrigger("KeyDown");
            
            // Register the key press with the PianoPuzzleManager
            pianoPuzzleManager.RegisterKeyPress(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            animator.SetTrigger("KeyUp");
        }
    }
}

