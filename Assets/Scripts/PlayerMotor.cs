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
    bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
      animator = GetComponentInChildren<Animator>();//looks to child object for animatable
      isRunning = false;
      speed = walkSpeed;
      //jumpAnim = GetComponentInChildren<Animation>();
    }

    // Update is called once per frame
    void Update()
    {

      //w should increase your speed in the forward direction
      //speeed should slowly compound up to a limit
      //a/d should rotate and shift velocity appropriately

      if(Input.GetKey("w")){
        //slowingToHalt = false;
        Vector3 move = playerBody.transform.right; //direction of movement
        velocity.x = (move*speed).x;
        velocity.z = (move*speed).z;
        controller.Move(move* speed * Time.deltaTime);
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

      /*if(slowingToHalt){
        Vector3 move = playerBody.transform.right; //direction of movement
        //velocity -= (move*speedDecrease);
        velocity = new Vector3(0f,0f,0f);

        Debug.Log(velocity.magnitude);
        if(velocity.magnitude <= 2f){
          slowingToHalt = false;
          velocity = new Vector3(0f,0f,0f);
        }
        controller.Move(velocity * Time.deltaTime);
      }*/

      /*if(Input.GetKey("s")){
        slowingToHalt = false;

        Vector3 move = playerBody.transform.right;
        controller.Move(-move * speedIncrease * Time.deltaTime);
      }*/

      if(Input.GetKey("a")){
        //slowingToHalt = false;
        playerBody.transform.Rotate(-Vector3.up * RotateSpeed * Time.deltaTime);

        /*float magnitude = velocity.magnitude;
        if(magnitude > 2){
          Vector3 move = playerBody.transform.right;
          velocity = move*magnitude;
        }*/

        //controller.Move(velocity*Time.deltaTime);
        //velocity = Vector3.RotateTowards(velocity,-Vector3.up,7f,0f);

        //playerBody.transform.Rotate(-Vector3.up * RotateSpeed * Time.deltaTime);
        //Vector3 move = playerBody.transform.forward;
        //controller.Move(move * speed * Time.deltaTime);
      }

      if(Input.GetKey("d")){
        //slowingToHalt = false;

        playerBody.transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);

        //Vector3 move = playerBody.transform.forward;
        //controller.Move(-move * speed * Time.deltaTime);
      }

      if(Input.GetButtonDown("Jump") && isGrounded){
        //jumpAnim.Play();
        //animator.SetFloat("heightPercent",1, 0, Time.deltaTime);//0.1f is a smoothing parameter
        //Debug.Log(animator.GetFloat("heightPercent"));//0.1f is a smoothing parameter
        //Debug.Log(animator.GetFloat("speedPercent"));//0.1f is a smoothing parameter
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

        //animator.SetFloat("heightPercent",0, locomotionAnimationSmoothTime, Time.deltaTime);//0.1f is a smoothing parameter

      } else{
        velocity.y += gravity * Time.deltaTime;
      }
      controller.Move(velocity * Time.deltaTime); //because gravity function of deltaG = .5*g*t^2
      //controller.velocity = velocity;

    }
}
