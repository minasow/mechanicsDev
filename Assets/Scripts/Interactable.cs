using UnityEngine;

public class Interactable : MonoBehaviour
{
  public float radius = .5f; //how close does player need to be to interact
  Transform target;
  
  void Start(){
    target = PlayerManager.instance.player.transform;
  }

  void Update(){
    float distance = Vector3.Distance(target.position, transform.position);
    if(distance <= radius){
      Debug.Log("YOU WIN!");
    }

  }

  void OnDrawGizmosSelected(){
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, radius);
  }
}
