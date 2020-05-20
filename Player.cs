﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    [SerializeField]
    Vector3 COM;
    [Space (15)]
    float speed = 1.0f;
    [SerializeField]
    float steerSpeed = 1.0f;
    [SerializeField]
    float movementThresold = 10.0f;

    Transform m_COM;
    float verticalInput;
    float movementFactor;
    float horizontalInput;
    float steerFactor;

    int health = 100;

    void Update() {
        Balance();
        Movement();
        Steer();        
    }

    void Balance() {
        if (!m_COM) {
            m_COM = new GameObject ("COM").transform;
            m_COM.SetParent (transform);
        }

        m_COM.position = COM;
        GetComponent<Rigidbody> ().centerOfMass = m_COM.position;
 
    }

    void Movement() {
        verticalInput = Input.GetAxis ("Vertical");
        movementFactor = Mathf.Lerp (movementFactor, verticalInput, Time.deltaTime / movementThresold);
        transform.Translate (0.0f, 0.0f, movementFactor * speed);
    }

    void Steer() {
        horizontalInput = Input.GetAxis ("Horizontal");
        steerFactor = Mathf.Lerp (steerFactor, horizontalInput * verticalInput, Time.deltaTime / movementThresold);
        transform.Rotate (0.0f, steerFactor * steerSpeed, 0.0f);
    }

    public void TakeDamage(int amount){
        health -= amount;
        Debug.Log("Player HP: " + health);
        if(health <= 0)
            Destroy(this.gameObject);
    }
}
