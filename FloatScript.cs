using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatScript : MonoBehaviour
{
    [SerializeField]
    private float waterLevel = 0.0f;
    [SerializeField]
    private float floatTreshold = 2.0f;
    [SerializeField]
    private float waterDensity = 0.125f;
    [SerializeField]
    private float downForce = 4.0f;

    private float forceFactor;
    private Vector3 floatForce;

    // Update is called once per frame
    void FixedUpdate()
    {
       forceFactor = 1f - ((transform.position.y - waterLevel) / floatTreshold);

       if(forceFactor > 0.0f){
           floatForce = -Physics.gravity * GetComponent<Rigidbody>().mass * (forceFactor - GetComponent<Rigidbody>().velocity.y * waterDensity);
           floatForce += new Vector3(0, -downForce * GetComponent<Rigidbody>().mass, 0);
           GetComponent<Rigidbody>().AddForceAtPosition(floatForce, transform.position);
       } 
    }
}
