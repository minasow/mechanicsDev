using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{
  public CharacterAnimator charAnimator;
  public PlayerMotor motor;

    // Start is called before the first frame update
    void Start()
    {
      charAnimator = gameObject.GetComponentInParent<CharacterAnimator>();
      motor = gameObject.GetComponentInParent<PlayerMotor>();

    }

    void JumpComplete(){
      motor.Jump();
      motor.animationVerb = "Float";
    }

    void floatComplete(){
      Debug.Log("float complete");
    }

    void landComplete(){
      motor.animationVerb = "";
    }


    // Update is called once per frame
    void Update()
    {

    }
}
