using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem explosion;

    private void OnCollisionEnter(Collision other) {
        Debug.Log(other.transform.name);
        if(other.collider.tag == "Ground"){
            Destroy(this.gameObject);
        }else if(other.collider.name == "Fort"){
            other.collider.gameObject.GetComponent<Fort>().TakeDamage(10);
            Instantiate(explosion.gameObject, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }else if(other.transform.name == "Player"){
            other.transform.gameObject.GetComponent<Player>().TakeDamage(10);
            Instantiate(explosion.gameObject, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }else if(other.collider.transform.parent.name == "Boat"){
            other.collider.transform.parent.gameObject.GetComponent<Boat>().TakeDamage(10);
            Instantiate(explosion.gameObject, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }else{
            Instantiate(explosion.gameObject, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        
    }   
}
