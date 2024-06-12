using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melody_Test : MonoBehaviour
{
    private Material myMaterial;
    private Melody_Match_Object melodyMatchObject;
    private Melody_Match_Controller melodyMatchController;

    private void OnEnable()
    {
        myMaterial = GetComponent<Renderer>().material;
        melodyMatchObject = GetComponent<Melody_Match_Object>();
        melodyMatchController = transform.parent.GetComponent<Melody_Match_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (melodyMatchController.GetIsPuzzleComplete())
            myMaterial.color = Color.green;
        else if (melodyMatchObject.GetIsPlayingMelody())
            myMaterial.color = Color.red;
        else
            myMaterial.color = Color.blue;
    }
}
