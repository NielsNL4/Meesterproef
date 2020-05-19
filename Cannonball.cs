using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    [SerializeField]
    ParticleSystem explosion;

    private void OnCollisionEnter(Collision other) {
        if(other.collider.tag == "Ground"){
            Destroy(this.gameObject);
        }else{
            Instantiate(explosion.gameObject, transform.position, Quaternion.identity);
            float duration = explosion.main.duration;
            Destroy(this.gameObject);
        }
    }   
}
