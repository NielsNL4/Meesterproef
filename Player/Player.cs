using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{   
    [SerializeField]
    private Vector3 COM;
    [Space (15)]
    private float speed = 0.5f;
    [SerializeField]
    private float steerSpeed = 1.0f;
    [SerializeField]
    private float movementThresold = 10.0f;

    private Transform m_COM;
    private float verticalInput;
    private float movementFactor;
    private float horizontalInput;
    private float steerFactor;

    public int health = 250;
    public int points;

    private bool docked;

    private int score;

    [SerializeField]
    private Text pointText;

    private GameManager gameManager;

    private void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update() {
        Balance();
        Movement();
        Steer();
        
        pointText.text = "Points: " + points.ToString();      

        if(docked){
            if(Input.GetKeyDown(KeyCode.E) && points > 0){
                SellPoints();
            }
        }


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
        if(health <= 0)
            Destroy(this.gameObject);
    }

    public void addPoints(int amount){
        points += amount;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Dock"){
            docked = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Dock"){
            docked = false;
        }
    }

    void SellPoints(){
        score += points;
        points = 0;
        gameManager.currentScore = score;
    }
}
