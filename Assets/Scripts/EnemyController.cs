using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
  public float lookRadius = 10f;
  public float caughtRadius = 1f;
  public float RotateSpeed = 100f;

  Transform target;
  NavMeshAgent agent;
  bool isEngaged = false;
  bool caught = false;
  bool forwardPacing = true;
  public float speed;
  public string animationVerb;
  Vector3 startPosition;
  public float pacingDistance = 10f;
  public CharacterController controller;
  Vector3 velocity;
  Vector3 direction;
  float distance;

    // Start is called before the first frame update
    void Start()
    {
      startPosition = transform.position;
      target = PlayerManager.instance.player.transform;
      //agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
      if(!isEngaged){
        Pace();
      }

      distance = Vector3.Distance(target.position, transform.position);

      isEngaged = isEngaged ? true : distance <= lookRadius; //should only be able to engage, not disengage
      caught = distance <= caughtRadius;
      if(caught){
        Debug.Log("YOU LOSE");
      } else if(isEngaged){
        isEngaged = true;
        FaceTarget();
        RushTarget();
        /*
        isEngaged = true;
        //then start interacting with PlayerManager
        //agent.SetDestination(target.position);
        if(distance <= stoppingDistance){
          //interact with target
          //face target
          FaceTarget();
        }*/
      }

    }

    void RushTarget(){
      //TODO: how to chase the player
    }

    void FaceTarget(){
      Debug.Log("DETECTED");
      direction = (target.position - transform.position).normalized;

      Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
      transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation, Time.deltaTime);

      velocity.x = 0f;
      velocity.z = 0f;
      controller.Move(velocity * Time.deltaTime);

    }

    void Pace(){
      if(forwardPacing){
        Vector3 move = transform.right*speed;
        velocity = move;
        controller.Move(move * Time.deltaTime);
      }

      float relativeDistance = Vector3.Distance(startPosition, transform.position);
      if((!isEngaged) && (relativeDistance >= pacingDistance)){ //then turn around
        transform.rotation = Quaternion.LookRotation(-transform.forward, Vector3.up);
      }


    }


    void OnDrawGizmosSelected(){
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
