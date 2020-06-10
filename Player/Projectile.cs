using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Rigidbody projectile;

    private Camera cam;
    private float shootPower = 45;

    private bool isLeft;
    private bool armed;

    private float cannonRotation;

    private float cooldown = 3f;

    private float radianAngle;
    private float g;
    private float angle;
    private float velocity;

    private Transform leftCannons;
    private Transform rightCannons;

    private List<Transform> leftEmittors = new List<Transform>();
    private List<Transform> rightEmittors = new List<Transform>();

    private GameObject[] rightLines;
    private GameObject[] leftLines;

    private void Start()
    {
        cam = Camera.main;
        cannonRotation = 0f;

        leftLines = GameObject.FindGameObjectsWithTag("LeftLine");
        rightLines = GameObject.FindGameObjectsWithTag("RightLine");

        leftCannons = GameObject.FindGameObjectWithTag("Left Cannons").transform;
        rightCannons = GameObject.FindGameObjectWithTag("Right Cannons").transform;

        g = Mathf.Abs(Physics.gravity.y);

        for (int i = 0; i < leftCannons.childCount; i++)
        {
            leftEmittors.Add(leftCannons.GetChild(i).transform);
            rightEmittors.Add(rightCannons.GetChild(i).transform);
        }        
    }

    // Update is called once per framec
    private void Update()
    {
        LaunchProjectile();
    }

    private void LaunchProjectile()
    {
        

        if(Input.GetKeyDown(KeyCode.C)){
            armed = !armed;
        }

        cooldown -= Time.deltaTime;

        if(armed){
            float lastRotation = cannonRotation;
            if(isLeft){
                rightCannons.GetComponent<LineRenderer>().enabled = false;
                for (int i = 0; i < rightLines.Length; i++)
                {
                    rightLines[i].SetActive(false);
                    leftLines[i].SetActive(true);
                }

            }else if(!isLeft){
                leftCannons.GetComponent<LineRenderer>().enabled = false;
                for (int i = 0; i < rightLines.Length; i++)
                {
                    rightLines[i].SetActive(true);
                    leftLines[i].SetActive(false);
                }
            }
            
            if(Input.GetKeyDown(KeyCode.X)){
                isLeft = !isLeft;
            }

            if(Input.GetAxis("Mouse ScrollWheel") > 0 && cannonRotation > -35){
                cannonRotation--;
            }
            if(Input.GetAxis("Mouse ScrollWheel") < 0 && cannonRotation < 0){
                cannonRotation++;
            }
            if(cooldown <= 0){
                if(Input.GetMouseButtonDown(0)){
                    if(isLeft){
                        foreach(Transform emittor in leftEmittors){
                            Rigidbody obj = Instantiate(projectile, emittor.position, Quaternion.identity);
                            obj.velocity = shootPower * emittor.forward;
                            emittor.GetComponent<ParticleSystem>().Play();
                        }
                    }else if(!isLeft){
                            foreach(Transform emittor in rightEmittors){
                            Rigidbody obj = Instantiate(projectile, emittor.position, Quaternion.identity);
                            obj.velocity = shootPower * emittor.forward;
                            emittor.GetComponent<ParticleSystem>().Play();
                        }
                    }
                    cooldown = 3;
                }
            }
            
            if(cannonRotation != lastRotation){
                foreach(Transform emittor in leftEmittors){
                        emittor.localEulerAngles = new Vector3(cannonRotation, 0, 0);
                    }
                foreach(Transform emittor in rightEmittors){
                        emittor.localEulerAngles = new Vector3(cannonRotation, 0, 0);
                    }
                lastRotation = cannonRotation;
            }
        }else{
                for (int i = 0; i < rightLines.Length; i++)
                {
                    rightLines[i].SetActive(false);
                    leftLines[i].SetActive(false);
                }
            }
    }
}
