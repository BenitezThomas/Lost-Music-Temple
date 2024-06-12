// - Ƹ̵̡Ӝ̵̨̄Ʒ - //
//Author: Emirhan Bulut
//Date 6/10/2024 US

using UnityEngine;

public class HansCameraMovement : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse1))
        {
            if(Input.GetKey(KeyCode.W))
            {
                this.transform.Rotate(-32 * Time.deltaTime,0,0, Space.Self);
            }
            if(Input.GetKey(KeyCode.S))
            {
                this.transform.Rotate(32 * Time.deltaTime,0,0, Space.Self);
            }
            if(Input.GetKey(KeyCode.D))
            {
                this.transform.Rotate(0, 32 * Time.deltaTime ,0, Space.World);
            }
            if(Input.GetKey(KeyCode.A))
            {
                this.transform.Rotate(0,  32 * -Time.deltaTime,0, Space.World);
            }
            //this.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
        }
        else 
        {
            if(Input.GetKey(KeyCode.W))
            {
                this.transform.position = new Vector3(transform.position.x ,transform.position.y, transform.position.z + 17 * (Time.deltaTime));
            }
            if(Input.GetKey(KeyCode.S))
            {
                this.transform.position = new Vector3(transform.position.x ,transform.position.y, transform.position.z - 17 * (Time.deltaTime));
            }
            if(Input.GetKey(KeyCode.D))
            {
                this.transform.position = new Vector3(transform.position.x + 17 * (Time.deltaTime), transform.position.y,transform.position.z);
            }
            if(Input.GetKey(KeyCode.A))
            {
                this.transform.position = new Vector3(transform.position.x - 17 * (Time.deltaTime),transform.position.y,transform.position.z);
            }
        }
       
    }
}
