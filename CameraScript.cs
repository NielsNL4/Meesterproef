using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private float smoothSpeed = .125f;

    [SerializeField]
    private Vector3 offset;

    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void LateUpdate() {
        Vector3 desPos = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desPos, smoothSpeed);
        transform.position = smoothedPos;


        transform.LookAt(target);
    }
}
