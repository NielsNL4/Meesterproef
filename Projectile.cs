using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody projectile;
    // public GameObject cursor;
    // public Transform shootPoint;
    // public LayerMask layer;
    // public LineRenderer lineVisual;
    // public int lineSegment = 10;

    public GameObject rightAim;
    public GameObject leftAim;

    private Camera cam;
    public Camera camRight;
    public Camera CamLeft;

    public float shootPower = 12;

    // public ParticleSystem smoke;

    bool isLeft;
    bool armed;

    Transform leftCannons;
    Transform rightCannons;

    List<Transform> leftEmittors = new List<Transform>();
    List<Transform> rightEmittors = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        // lineVisual.positionCount = lineSegment;

        leftCannons = GameObject.FindGameObjectWithTag("Left Cannons").transform;
        rightCannons = GameObject.FindGameObjectWithTag("Right Cannons").transform;

        Debug.Log(leftCannons);

        for (int i = 0; i < leftCannons.childCount; i++)
        {
            leftEmittors.Add(leftCannons.GetChild(i).transform);
            rightEmittors.Add(rightCannons.GetChild(i).transform);
            Debug.Log(leftEmittors);
        }

        Debug.Log(leftEmittors[1].position);
    }

    // Update is called once per frame
    void Update()
    {
        LaunchProjectile();
    }

    void LaunchProjectile()
    {
        

        if(Input.GetKeyDown(KeyCode.C)){
            armed = !armed;
        }

        // if(Input.GetMouseButton(1) && armed){
        //     Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit;
        //     Debug.DrawRay(camRay.origin, camRay.direction * 100, Color.green);
        //     if (Physics.Raycast(camRay.origin, camRay.direction, out hit, 100))
        //     {
        //         if(hit.collider.name == "RightRange"){
        //             rightAim.SetActive(true);
        //             leftAim.SetActive(false);
        //         }
        //         else if(hit.collider.name == "LeftRange"){
        //             leftAim.SetActive(true);
        //             rightAim.SetActive(false);
        //         }else{
        //             leftAim.SetActive(false);
        //             rightAim.SetActive(false);
        //         }
        //         lineVisual.enabled = true;
        //         cursor.SetActive(true);
        //         cursor.transform.position = hit.point + Vector3.up * 0.1f;

        //         Vector3 vo = CalculateVelocty(hit.point, shootPoint.position, 1f);

        //         Visualize(vo);

        //         // transform.rotation = Quaternion.LookRotation(vo);

        //         if (Input.GetMouseButtonDown(0))
        //         {
        //             smoke.Play();
        //             Rigidbody obj = Instantiate(projectile, shootPoint.position, Quaternion.identity);
        //             obj.velocity = vo;
        //         }
        //     }else{
        //         lineVisual.enabled = false;
        //         cursor.SetActive(false);
        //         leftAim.SetActive(false);
        //         rightAim.SetActive(false);
        //     }
        // }else{
        //     lineVisual.enabled = false;
        //     cursor.SetActive(false);
            
        // }

        if(armed){
                cam.enabled = false;
                if(isLeft){
                    camRight.enabled = false;
                    CamLeft.enabled = true;
                    leftAim.SetActive(true);
                    rightAim.SetActive(false);
                }else if(!isLeft){
                    camRight.enabled = true;
                    CamLeft.enabled = false;
                    rightAim.SetActive(true);
                    leftAim.SetActive(false);
                }
                
                if(Input.GetKeyDown(KeyCode.X)){
                    isLeft = !isLeft;
                }

                if(Input.GetAxis("Mouse ScrollWheel") > 0){
                    shootPower--;
                    Debug.Log("Shootpower: " + shootPower);
                }
                if(Input.GetAxis("Mouse ScrollWheel") < 0){
                    shootPower++;
                    Debug.Log("Shootpower: " + shootPower);
                }

                if(Input.GetMouseButtonDown(0)){
                    if(isLeft){
                        // smoke.Play();
                        foreach(Transform emittor in leftEmittors){
                            Rigidbody obj = Instantiate(projectile, emittor.position, Quaternion.identity);
                            obj.velocity = shootPower * emittor.forward;
                        }
                    }else if(!isLeft){
                         foreach(Transform emittor in rightEmittors){
                            Rigidbody obj = Instantiate(projectile, emittor.position, Quaternion.identity);
                            obj.velocity = shootPower * emittor.forward;
                        }
                    }
                }
                
        }else{
            cam.enabled = true;
            camRight.enabled = false;
            CamLeft.enabled = false;
            rightAim.SetActive(false);
            leftAim.SetActive(false);
        }
    }

    // void Visualize(Vector3 vo)
    // {
    //     for (int i = 0; i < lineSegment; i++)
    //     {
    //         Vector3 pos = CalculatePosInTime(vo, i / (float)lineSegment);
    //         lineVisual.SetPosition(i, pos);
    //     }
    // }

    // Vector3 CalculateVelocty(Vector3 target, Vector3 origin, float time)
    // {
    //     Vector3 distance = target - origin;
    //     Vector3 distanceXz = distance;
    //     distanceXz.y = 0f;

    //     float sY = distance.y;
    //     float sXz = distanceXz.magnitude;

    //     float Vxz = sXz * time;
    //     float Vy = (sY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);

    //     Vector3 result = distanceXz.normalized;
    //     result *= Vxz;
    //     result.y = Vy;

    //     return result;
    // }

    // Vector3 CalculatePosInTime(Vector3 vo, float time)
    // {
    //     Vector3 Vxz = vo;
    //     Vxz.y = 0f;

    //     Vector3 result = shootPoint.position + vo * time;
    //     float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + shootPoint.position.y;

    //     result.y = sY;

    //     return result;
    // }
}
