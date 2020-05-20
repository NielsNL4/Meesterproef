using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Fort : MonoBehaviour
{
    [HideInInspector]
    public int health = 300;
    Transform target;
    float range = 200;
    float distToPlayer;
    float coolDown = 5f;
    bool playerIsInRange;
    float shootDuration = 1;
    float currentDuration;
    public Transform emittor;
    Vector3 cp;
    public GameObject projectile;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
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

    void Attack(){
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
        Debug.Log("Fort HP: " + health);
        if(health <= 0)
            Destroy(this.gameObject);
    }

     Vector3 CalculateVelocty(Vector3 target, Vector3 origin, float time)
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
