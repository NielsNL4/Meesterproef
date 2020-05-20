using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    Rigidbody projectile;

    GameObject rightAim;
    GameObject leftAim;

    Camera cam;
    [SerializeField]
    Camera camRight;
    [SerializeField]
    Camera CamLeft;

    float shootPower = 45;

    Renderer sails;

    bool isLeft;
    bool armed;

    float cannonRotation;

    float cooldown = 3f;

    float radianAngle;
    float g;
    float angle;
    float velocity;

    Transform leftCannons;
    Transform rightCannons;

    List<Transform> leftEmittors = new List<Transform>();
    List<Transform> rightEmittors = new List<Transform>();

    void Start()
    {
        cam = Camera.main;
        cannonRotation = 0f;

        leftCannons = GameObject.FindGameObjectWithTag("Left Cannons").transform;
        rightCannons = GameObject.FindGameObjectWithTag("Right Cannons").transform;

        Debug.Log(leftCannons);

        g = Mathf.Abs(Physics.gravity.y);

        for (int i = 0; i < leftCannons.childCount; i++)
        {
            leftEmittors.Add(leftCannons.GetChild(i).transform);
            rightEmittors.Add(rightCannons.GetChild(i).transform);
            Debug.Log(leftEmittors);
        }

        sails = GameObject.Find("Sails").GetComponent<Renderer>();
        
    }

    // Update is called once per framec
    void Update()
    {
        LaunchProjectile();
    }

    void LaunchProjectile()
    {
        

        if(Input.GetKeyDown(KeyCode.C)){
            armed = !armed;
        }

        cooldown -= Time.deltaTime;

        if(armed){
            Debug.Log(sails.material.color);
            float lastRotation = cannonRotation;
            cam.enabled = false;
            if(isLeft){
                camRight.enabled = false;
                CamLeft.enabled = true;
                leftAim.SetActive(true);
                rightAim.SetActive(false);
                // leftCannons.GetComponent<LineRenderer>().enabled = true;
                rightCannons.GetComponent<LineRenderer>().enabled = false;
                // RenderArc(leftCannons.gameObject, shootPower * leftCannons.GetChild(0).forward, leftCannons.GetComponent<LineRenderer>());

            }else if(!isLeft){
                camRight.enabled = true;
                CamLeft.enabled = false;
                rightAim.SetActive(true);
                leftAim.SetActive(false);
                leftCannons.GetComponent<LineRenderer>().enabled = false;
                // rightCannons.GetComponent<LineRenderer>().enabled = true;
                // RenderArc(rightCannons.gameObject, shootPower * rightCannons.GetChild(0).forward, rightCannons.GetComponent<LineRenderer>());
            }
            
            if(Input.GetKeyDown(KeyCode.X)){
                isLeft = !isLeft;
            }

            if(Input.GetAxis("Mouse ScrollWheel") > 0 && cannonRotation > -35){
                cannonRotation--;
                Debug.Log("cannon Rot: " + cannonRotation);
            }
            if(Input.GetAxis("Mouse ScrollWheel") < 0 && cannonRotation < 0){
                cannonRotation++;
                Debug.Log("cannon Rot: " + cannonRotation);
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
                Debug.Log("cannon rot is changed");
                foreach(Transform emittor in leftEmittors){
                        emittor.localEulerAngles = new Vector3(cannonRotation, 0, 0);
                    }
                foreach(Transform emittor in rightEmittors){
                        emittor.localEulerAngles = new Vector3(cannonRotation, 0, 0);
                    }
                lastRotation = cannonRotation;
            }
                
        }else{
            cam.enabled = true;
            camRight.enabled = false;
            CamLeft.enabled = false;
            rightAim.SetActive(false);
            leftAim.SetActive(false);
        }
    }

    void RenderArc(GameObject emittor, Vector3 vo, LineRenderer lr)
    {
        angle = cannonRotation;
        velocity = vo.magnitude + vo.x + (vo.y / 2) - 1;
        lr.positionCount = 15;
        lr.SetPositions(CalculateArcArray());
    }

    Vector3[] CalculateArcArray(){
        Vector3[] arcArray = new Vector3[15 + 1];

        radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;

        for (int i = 0; i < 15; i++)
        {
            float t = (float)i / (float)10;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }

        return arcArray;
        
    }

    Vector3 CalculateArcPoint(float t, float maxDistance){
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle))) - 1;
        return new Vector3(0, y, -x);
    }

    void ChangeSailColor(float Alpha){
        sails.material.SetColor("_BaseColor", new Color(1,1,1,Alpha));
    }
}
