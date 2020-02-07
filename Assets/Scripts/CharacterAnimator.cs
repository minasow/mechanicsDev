using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //for NavMeshAgent

public class CharacterAnimator : MonoBehaviour
{
    public Animator animator;
    public CharacterController controller;
    public PlayerMotor playerMotor;
    public EnemyController enemyMotor;


    const float locomotionAnimationSmoothTime = .1f;
    public float maxSpeed = 4.5f;
    public float maxJumpHeight = 1.5f;
    float speedPercent;
    Vector3 velocity;
    Vector3 height;
    bool isEnemy;

    // Start is called before the first frame update
    void Start()
    {
      playerMotor = gameObject.GetComponent<PlayerMotor>();
      if(playerMotor == null){
        isEnemy = true;
        enemyMotor = gameObject.GetComponent<EnemyController>();
      }
      controller = GetComponent<CharacterController>();
      animator = GetComponentInChildren<Animator>();//looks to child object for animatable
    }



    void LateUpdate(){
      if((!isEnemy) && (playerMotor.animationVerb.Length > 0)){
          animator.SetTrigger(playerMotor.animationVerb);
          playerMotor.animationVerb = "";
      }
    }

    // Update is called once per frame
    void Update()
    {
        velocity = controller.velocity;
        velocity.y = 0;
        speedPercent = controller.velocity.magnitude / maxSpeed;//currnent speed / max speed
        animator.SetFloat("speedPercent",speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);//0.1f is a smoothing parameter
    }


}
