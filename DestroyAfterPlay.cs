using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterPlay : MonoBehaviour
{

    private ParticleSystem ps;

    private void Start() {
        ps = gameObject.GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        if(ps.isPlaying == false){
            Destroy(this.gameObject);
        }
    }
}
