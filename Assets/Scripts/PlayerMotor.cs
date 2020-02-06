using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerMotor : MonoBehaviour
{

    public Transform playerBody;
    public CharacterController controller;
    const float locomotionAnimationSmoothTime = .1f;

    public float RotateSpeed;
    public float speed;
    public float walkSpeed;
    public float runSpeed;

    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Animation jumpAnim;
    public Animator animator;
    public float jumpHeight = 3f;

    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
      animator = GetComponentInChildren<Animator>();//looks to child object for animatable
      speed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {

      if(Input.GetKey("w")){
        Vector3 move = playerBody.transform.right * speed; //direction of movement
        velocity.x = move.x;
        velocity.z = move.z;
        controller.Move(move * Time.deltaTime);
      }

      if(Input.GetKeyDown("left shift")){
        speed = (speed == walkSpeed) ? runSpeed : walkSpeed;
      }

      if(Input.GetKeyUp("w")){
        //slowingToHalt = true;
        velocity.x = 0f;
        velocity.z = 0f;
        controller.Move(velocity * Time.deltaTime);
      }

      if(Input.GetKey("a")){
        playerBody.transform.Rotate(-Vector3.up * RotateSpeed * Time.deltaTime);
      }

      if(Input.GetKey("d")){
        playerBody.transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);
      }

      if(Input.GetButtonDown("Jump") && isGrounded){
        animator.SetTrigger("Jump");
      }

      if(Input.GetButtonUp("Jump")){
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        animator.SetTrigger("Float");
      }

      isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

      if(isGrounded && velocity.y < 0){
        velocity.y = -2f; //could be zero, but exprimentally this works better
        animator.SetTrigger("beginLand");
        animator.SetTrigger("completeLand");
      } else{
        velocity.y += gravity * Time.deltaTime;
      }

      controller.Move(velocity * Time.deltaTime); //because gravity function of deltaG = .5*g*t^2
    }
}
