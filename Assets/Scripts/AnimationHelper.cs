using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{
  public PlayerMotor motor;

    // Start is called before the first frame update
    void Start()
    {
      motor = gameObject.GetComponentInParent<PlayerMotor>();
    }

    void JumpComplete(){
      motor.Jump();
      motor.animationVerb = "Float";
    }

    void floatComplete(){
    }

    //not sure this is necessary
    void landComplete(){
      motor.animationVerb = "";
    }


    // Update is called once per frame
    void Update()
    {

    }
}
