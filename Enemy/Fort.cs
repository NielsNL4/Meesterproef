using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Fort : MonoBehaviour
{
    [HideInInspector]
    public int health = 300;
    private Transform target;
    private float range = 200;
    private float distToPlayer;
    private float coolDown = 5f;
    private bool playerIsInRange;
    private float shootDuration = 1;
    private float currentDuration;
    [SerializeField]
    private Transform emittor;
    private Vector3 cp;
    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private Transform loot;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        distToPlayer = Vector3.Distance(transform.position, target.position);
        if(range > distToPlayer){
            playerIsInRange = true;
            Attack();
        }else{
            playerIsInRange = false;
        }

        currentDuration += Time.deltaTime;

        cp = new Vector3(((target.position.x + emittor.position.x) / 2),emittor.position.y,((target.position.z + emittor.position.z / 2)));
        
    }

    private void OnDrawGizmos() {
        if(range > distToPlayer){
            Gizmos.color = Color.red;
        }else{
            Gizmos.color = Color.blue;
        }
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void Attack(){
        if(coolDown > 0){
            coolDown -= Time.deltaTime;
        }
        else if(coolDown <= 0){
            GameObject cannonBall = Instantiate(projectile, emittor.position, Quaternion.identity);
            cannonBall.GetComponent<Rigidbody>().velocity = CalculateVelocty(target.position, emittor.position, 1f);
            coolDown = 5;
        }
    }

    public void TakeDamage(int amount){
        health -= amount;
        if(health <= 0){
            DropLoot();
            Destroy(this.gameObject);
        }
    }

    private void DropLoot(){
        Vector3 dropLoc = new Vector3(transform.position.x + 70, transform.position.y - 5, transform.position.z);
        Instantiate(loot, dropLoc, Quaternion.identity);
    }

    private Vector3 CalculateVelocty(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXz = distance;
        distanceXz.y = 0f;

        float sY = distance.y;
        float sXz = distanceXz.magnitude;

        float Vxz = sXz * time;
        float Vy = (sY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);

        Vector3 result = distanceXz.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }
}
