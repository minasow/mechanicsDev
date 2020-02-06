using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float pitch = 2f;

    public float threshold = 5f;
    public float rotationSpeed = 100f;
    private float currentZoom = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update(){
      //yawInput -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
      //transform.rotation = target.rotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
    }
}
