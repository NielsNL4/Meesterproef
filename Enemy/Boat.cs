using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Boat : MonoBehaviour
{
    private Transform target;

    [SerializeField]
    private Vector3 COM;
    private Transform m_COM;

    [SerializeField]
    private float range = 100f;
    private float closeRange = 80f;

    public int health = 150;

    private float distToPlayer;

    [SerializeField]
    private float speed;

    private Vector3 dir;

    private float movementThresold = 5f;

    private float movementFactor;
    private float steerFactor;

    private NavMeshAgent agent;

    private bool grounded;

    [SerializeField]
    private float wanderRadius = 200;

    private float wanderTime = 5;

    private Vector3 randomDirection;
    private Vector3 lastDirection;
    
    [SerializeField]
    private Transform loot;

    private Vector3 offsetPlayer;

    private Transform cannons;
    private List<Transform> emittors = new List<Transform>();

    private float cooldown = 5;

    [SerializeField]
    private GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        agent = gameObject.GetComponent<NavMeshAgent>();

        cannons = GameObject.FindGameObjectWithTag("Enemy Cannons").transform;

        for(int i = 0; i < 3; i++){
            emittors.Add(cannons.GetChild(i).transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Balance();
        speed = 30f * Time.deltaTime;
        distToPlayer = Vector3.Distance(transform.position, target.position);
        if(range >= distToPlayer && distToPlayer > closeRange){
            steerFactor = Mathf.Lerp(steerFactor, 1, Time.deltaTime * movementThresold);
            movementFactor = Mathf.Lerp (movementFactor, 1, Time.deltaTime / movementThresold);
            agent.SetDestination(target.position);
            agent.acceleration = movementFactor * 10f;
        }else if(closeRange > distToPlayer){
            offsetPlayer = target.position - transform.position;
            dir = Vector3.Cross(offsetPlayer, Vector3.up);
            agent.SetDestination(transform.position + dir);
            Attack();
        }
        else{
            Wander(Time.deltaTime);
        }
    }

    void Wander(float Time){

        wanderTime -= Time;

        if(wanderTime <= 0){
            randomDirection = Random.insideUnitSphere * wanderRadius;
            wanderTime = 15;
        }


        if(randomDirection != lastDirection){
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1);
            Vector3 finalPosition = hit.position;
            agent.SetDestination(finalPosition);
        }

        lastDirection = randomDirection;

        
    }

     void Balance() {
        if (!m_COM) {
            m_COM = new GameObject ("COM").transform;
            m_COM.SetParent (transform);
        }

        m_COM.position = COM;
        GetComponent<Rigidbody> ().centerOfMass = m_COM.position;
 
    }

    private void OnDrawGizmos() {
        if(range > distToPlayer){
            Gizmos.color = Color.red;
        }else{
            Gizmos.color = Color.blue;
        }
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, closeRange);
    }

    public void OnTriggerEnter(Collider other)
     {
         if ((other.tag == "Ground") && (grounded == false))
         {
             agent.enabled = false;
             agent.enabled = true;
             grounded = true;
         }
     }

    public void TakeDamage(int amount){
        health -= amount;

        if(health <= 0){
            DropLoot();
            Destroy(this.gameObject);
        }
    }

    void DropLoot(){
        Instantiate(loot, transform.position, Quaternion.identity);
    }

    void Attack(){
        if(cooldown > 0){
            cooldown -= Time.deltaTime;
        }else if(cooldown <= 0){
            foreach(Transform emittor in emittors){
                GameObject cannonBall = Instantiate(projectile, emittor.position, Quaternion.identity);
                cannonBall.GetComponent<Rigidbody>().velocity = 45 * emittor.forward;
                emittor.GetComponent<ParticleSystem>().Play();
                cooldown = 5;
            }
        }
    }
}
