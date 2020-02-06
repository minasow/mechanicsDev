using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
  public float lookRadius = 10f;
  Transform target;
  NavMeshAgent agent;
  bool isEngaged = false;
  bool forwardPacing = true;
  public float speed;
  Vector3 startPosition;
  public float pacingDistance = 10f;
  public CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
      startPosition = transform.position;
      target = PlayerManager.instance.player.transform;
      agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
      if(!isEngaged){
        Pace();
      }

      float distance = Vector3.Distance(target.position, transform.position);

      if(distance <= lookRadius){
        isEngaged = true;
        //then start interacting with PlayerManager
        agent.SetDestination(target.position);
        if(distance <= agent.stoppingDistance){
          //interact with target
          //face target
          FaceTarget();
        }
      }

    }

    void Pace(){
      if(forwardPacing){
        Vector3 move = transform.right;
        controller.Move(move* speed * Time.deltaTime);
      }

      float relativeDistance = Vector3.Distance(startPosition, transform.position);
      if(relativeDistance >= pacingDistance){ //then turn around
        transform.rotation = Quaternion.LookRotation(-transform.forward, Vector3.up);
      }


    }

    void FaceTarget(){
      Vector3 direction = (target.position - transform.position).normalized;
      Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
      transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation, Time.deltaTime*5f);
    }

    void OnDrawGizmosSelected(){
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
