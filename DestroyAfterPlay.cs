using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterPlay : MonoBehaviour
{

    ParticleSystem ps;

    private void Start() {
        ps = gameObject.GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if(ps.isPlaying == false){
            Destroy(this.gameObject);
        }
    }
}
