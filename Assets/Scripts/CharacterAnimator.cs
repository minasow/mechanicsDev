using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //for NavMeshAgent

public class CharacterAnimator : MonoBehaviour
{
    public Animator animator;
    public CharacterController controller;
    public PlayerMotor motor;

    const float locomotionAnimationSmoothTime = .1f;
    public float maxSpeed = 10f;
    public float maxJumpHeight = 1.5f;
    float speedPercent;
    float heightPercent;
    Vector3 velocity;
    Vector3 height;

    // Start is called before the first frame update
    void Start()
    {
      motor = gameObject.GetComponent<PlayerMotor>();
      controller = GetComponent<CharacterController>();
      animator = GetComponentInChildren<Animator>();//looks to child object for animatable
    }

    // Update is called once per frame
    void Update()
    {
      Debug.Log(motor.animationVerb);
      if(motor.animationVerb.Length > 0){
        animator.SetTrigger(motor.animationVerb);
        motor.animationVerb = "";
      }
      //should distinguish between grounded & air state, and use appropriate animation
        velocity = controller.velocity;
        velocity.y = 0;
        speedPercent = controller.velocity.magnitude / maxSpeed;//currnent speed / max speed
        animator.SetFloat("speedPercent",speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);//0.1f is a smoothing parameter


    }


}
