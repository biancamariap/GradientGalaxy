using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    private Transform lookAt;
    private Vector3 startOffset;
    private Vector3 moveVector;
    private float transition = 0.0f;
    private float animationDuration = 2.0f; //durata animatiei de la inceput
    private Vector3 animationOffset = new Vector3(0, 5, 5);
    // Update is called once per frame
    void Start()
    {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position - lookAt.position;
       
    }

    private void Update()
    {
        moveVector = lookAt.position + startOffset;
        transform.position = lookAt.position + startOffset;


        //x
        moveVector.x = 0.0f;

        //y
        moveVector.y = Mathf.Clamp(moveVector.y, 3, 5);

        if (transition > 1.0f)
        {

            transform.position = moveVector;
        }
        else
        {
           
            transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
            transition += Time.deltaTime * 1 / animationDuration;
            transform.LookAt(lookAt.position + Vector3.up);
        }
     

    }
}
