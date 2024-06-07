// - Ƹ̵̡Ӝ̵̨̄Ʒ - //
//Author: Emirhan Bulut
//Date 6/7/2024 US


using UnityEngine;

public class WheelInteraction : MonoBehaviour
{
    [SerializeField] [Range(0.99f,6f)] private float correctBlock;
    [SerializeField] private int currentBlock;
    public bool isCorrect;
    private bool isFirstSet = false;
    private float cylinderCount;
    private Animator animator;
    

    void Start()
    {
        //Set the current block as default 1.
        currentBlock = 1; 

        animator = GetComponent<Animator>();     
    }

    void Update()
    {
        SetCurrentBlock(cylinderCount);
        //Simple Code Checking if  current state == correct state.
        CheckStateCorrect();
    }


    private void OnMouseDown()
    {
        Debug.Log("Clicked at " + this.name);   
        //Once Clicked on the Object Activate this Function.
        RotateCylinder();
    }

    private void RotateCylinder()
    {
        //Logic here is, if it is the first time being touched it will play the rotation Animation, if not it will activate the trigger and the animation logic will do the rest.
        if(isFirstSet == false) {animator.Play("rotation");} 
        else { animator.SetTrigger("TriggerExample");}

        isFirstSet = true;     
    }

    private void SetCurrentBlock(float count)
    {
        //This code block decides on what the currentblock is based on the last played animation
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("rotation"))
        {
            currentBlock = 2;            
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("rotation1"))
        {
            currentBlock = 3;
        }      
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("rotation2"))
        {
            currentBlock = 4;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("rotation3"))
        {
            currentBlock = 1;
        }
        
    }

    private void CheckStateCorrect()
    {
        if(currentBlock == correctBlock)
        {
            isCorrect = true;
        }
        else
        {
            isCorrect = false;
        }
    }
}
