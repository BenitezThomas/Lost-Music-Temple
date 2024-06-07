// - Ƹ̵̡Ӝ̵̨̄Ʒ - //
//Author: Emirhan Bulut
//Date 6/6/2024 US


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
        //Get the parent object -> Get the WheelPuzzle Script -> Grab the Cylinders List -> Get the number of items inside and set it as our variable.
        cylinderCount = this.transform.parent.GetComponent<WheelPuzzle>().cylinders.Count;

        //Set the current block as default 1.
        currentBlock = 1; 

        animator = GetComponent<Animator>();     
    }

    void Update()
    {
        //Set the current Block by using the logic inside this function, push the cylinderCount value for proper results.
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
        //This long code block boils down to picking which side is facing the screen depending on the animation being played. 
        if (count == 3)
        {
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
        }

        else if (count == 4)
        {
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

        else if (count == 5)
        {
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
                currentBlock = 5;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("rotation4"))
            {
                currentBlock = 1;
            }
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
