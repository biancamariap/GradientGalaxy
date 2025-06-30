using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    private CharacterController controller;
    private float speed = 9.0f;
    private Vector3 moveVector;
    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;
    private float animationDuration = 2.0f;

    

    private bool isDead = false;

    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        startTime = Time.time;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;
        if (Time.time - startTime < animationDuration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }
        moveVector = Vector3.zero;
        if (controller.isGrounded)
        {
            verticalVelocity = 0.0f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;


        }

        if (verticalVelocity < -3f)
        {
            Death();
            Debug.Log("Fall");
        }

        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
        moveVector.y = verticalVelocity;

        moveVector.z = speed;
        controller.Move(moveVector * Time.deltaTime);                 


    }




    public void SetSpeed(float modifier)
    {

        speed += modifier;
    }
   
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.gameObject.tag == "Obstacle")
        {
            Death();
            
        }
        
    }


    private void Death()
    {
        isDead = true;
        GetComponent<Score>().OnDeath();
    }
}


