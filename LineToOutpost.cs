using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineToOutpost : MonoBehaviour
{

    private LineRenderer lr;

    private Player player;

    private GameObject[] outposts;

    Vector3 closestOutpost;

    private float closestDistanceSqr = Mathf.Infinity;

    // Start is called before the first frame update
    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        

        player = GameObject.Find("Player").GetComponent<Player>();

        outposts = GameObject.FindGameObjectsWithTag("Outpost");
    }

    // Update is called once per frame
    private void Update()
    {
        for (int i = 0; i < outposts.Length; i++)
        {
            Vector3 dirToTarget = outposts[i].transform.position - transform.position;
            float sqrDistance = dirToTarget.sqrMagnitude;
            if(sqrDistance < closestDistanceSqr){
                closestDistanceSqr = sqrDistance;
                closestOutpost = outposts[i].transform.position;
            }else{
                continue;
            }
    
        }

        Vector3 startLoc = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);

        if(player.points > 0){
            lr.positionCount = 2;
            lr.SetPosition(0, startLoc);
            lr.SetPosition(1, closestOutpost);
        }else{
            lr.positionCount = 0;
        }
    }
}
