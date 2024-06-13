// - Ƹ̵̡Ӝ̵̨̄Ʒ - //
// Author: Emirhan Bulut
// Date 6/11/2024 US

using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    public float pushForce = 1;
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;

        if(rb != null)
        {
            //This Part takes the objects position and find the correct Vector3 to apply force into. 
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            //Applies force to the new Vector3 in between the objects. 
            rb.AddForceAtPosition(forceDirection * pushForce, transform.position, ForceMode.Impulse);
        }
    }

}
