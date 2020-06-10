using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{

    private int points;

    private Player player;

    private float range = 30;

    private float distToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        points = Random.Range(30,80);
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Update() {
        distToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if(range > distToPlayer){
            if(Input.GetKeyDown(KeyCode.Space)){
                player.addPoints(points);
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmos() {
        if(range > distToPlayer){
            Gizmos.color = Color.red;
        }else{
            Gizmos.color = Color.blue;
        }
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
