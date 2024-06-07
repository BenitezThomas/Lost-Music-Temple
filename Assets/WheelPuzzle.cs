using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelPuzzle : MonoBehaviour
{
    public WheelInteraction cylinder1;
    public WheelInteraction cylinder2;
    public WheelInteraction cylinder3;
    public WheelInteraction cylinder4;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(cylinder1.transform.localEulerAngles.x);
            Debug.Log(cylinder2.transform.localEulerAngles.x);
            Debug.Log(cylinder3.transform.localEulerAngles.x);
            Debug.Log(cylinder4.transform.localEulerAngles.x);
        }

        if(cylinder1.isCorrect && cylinder2.isCorrect && cylinder3.isCorrect && cylinder4.isCorrect)
        {
            Debug.Log("All Correct");
        }
    }
}
