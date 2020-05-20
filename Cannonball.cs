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
        }else if(other.collider.name == "Fort"){
            other.collider.gameObject.GetComponent<Fort>().TakeDamage(20);
            Instantiate(explosion.gameObject, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }else if(other.collider.name == "Player"){
            other.collider.gameObject.GetComponent<Player>().TakeDamage(10);
            Instantiate(explosion.gameObject, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }else{
            Instantiate(explosion.gameObject, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        
    }   
}
