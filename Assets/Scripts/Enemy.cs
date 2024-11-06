using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 20;
    public UnityEngine.AI.NavMeshAgent agent;
    public enum State
    {
        Idle,
        Patrol,
        Chase,
        Attack
    };
    public State state;
    public float waitAtPoint = 2f;
    public float chaseRange = 5f;
    public float attackRange = 2f;
    public float attackDelay = 2f;
    public float rotationSpeed = 3f;
    private float waitAtPointTimer;
    private float attackDelayTimer; 
    public Animator anim;
    public GameManager GM;
    public float hp = 200;
    public bool isDead = false;
    public float HitTime = 0;
    public float minDistance = 0f;
    public float maxDistance = 20f;
    public Vector3 destinationPoint;
    public Collider AttackCollider;
    void Start()
    {
        anim = GetComponent<Animator>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        if (HitTime - Time.time < -1f)
        {
            anim.SetBool("GetHit", false);
            HitTime = 0;
        }
        float distanceToPlayer = Vector3.Distance(GameObject.Find("Player").transform.position, agent.transform.position);
        if (isDead == false){
        switch (state)
        {
            case State.Idle:
            Idle(distanceToPlayer);
            break;

            case State.Patrol:
            Patrol(distanceToPlayer);
            break;

            case State.Chase:
            Chase(distanceToPlayer);
            break;

            case State.Attack:
            Attack(distanceToPlayer);
            break;
        }
        }
    }
    private void Idle(float distanceToPlayer)
    {
        anim.SetBool("Attack", false);
        anim.SetBool("IsRunning", false);
        if (distanceToPlayer <= chaseRange && GM.PlayerDead == false)
        {
            state = State.Chase;
        }
        else
        {
            if (waitAtPointTimer > 0)
            {
                waitAtPointTimer -= Time.deltaTime;
            }
            else
            {
                state = State.Patrol;
                destinationPoint = GenerateSpawnPosition();
                agent.SetDestination(destinationPoint);
            }
        }
    }
         private void Patrol(float distanceToPlayer)
    {
        anim.SetBool("Attack", false);
        anim.SetBool("IsRunning", true);
        if (distanceToPlayer <= chaseRange && GM.PlayerDead == false)
        {
            state = State.Chase;
        }
        else
        {
            LookAtSlerp(destinationPoint);
            if (agent.remainingDistance <= 0.2f)
            {
                state = State.Idle;
                waitAtPointTimer += waitAtPoint;
            }
        }
    }
    private void Chase(float distanceToPlayer)
    {
        anim.SetBool("Attack", false);
        anim.SetBool("IsRunning", true);
        LookAtSlerp(GameObject.Find("Player").transform.position);

        agent.SetDestination(GameObject.Find("Player").transform.position);
        
        if (distanceToPlayer <= attackRange && GM.PlayerDead == false)
        {
            state = State.Attack;
            agent.velocity = Vector3.zero;
            agent.isStopped = true;

            attackDelayTimer = attackDelay;
        }
        else if (distanceToPlayer > chaseRange)
        {
            state = State.Patrol;
            waitAtPointTimer = waitAtPoint;
            agent.velocity = Vector3.zero;
            agent.SetDestination(agent.transform.position);
        }
    }
    private void Attack(float distanceToPlayer)
    {
        if (GM.PlayerDead == false)
        {
          anim.SetBool("IsRunning", false);
        anim.SetBool("Attack", true);
        LookAtSlerp(GameObject.Find("Player").transform.position);
        attackDelayTimer -= Time.deltaTime;
        if (attackDelayTimer <= 0)
        {
            if (distanceToPlayer <= attackRange)
            {
                attackDelayTimer = attackDelay;
            }
            else
            {
                state = State.Idle;
                agent.isStopped = false;
            }
        }  
        }
        else
        {
            state = State.Patrol;
            destinationPoint = GenerateSpawnPosition();
            agent.isStopped = false;
            agent.SetDestination(destinationPoint);
        }
        
    }
    private void LookAtSlerp(Vector3 target)
    {
        agent.transform.rotation = Quaternion.Slerp(
            agent.transform.rotation,
            Quaternion.LookRotation(target - agent.transform.position),
            Time.deltaTime * rotationSpeed
        );
        agent.transform.rotation = Quaternion.Euler(0f, agent.transform.rotation.eulerAngles.y, 0f);
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
           hp -= col.gameObject.GetComponent<PlayerSword>().Damage;
           anim.SetBool("GetHit", true);
            HitTime = Time.time;
        }
        if (hp <= 0 && !isDead)
        {
            GetComponent<BoxCollider>().isTrigger = false;
            AttackCollider.enabled = false;
            isDead = true;
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
            anim.SetBool("Attack", false);
            anim.SetBool("IsRunning", false);
            anim.SetBool("Death", true);
            anim.SetBool("GetHit", false);
            Destroy(gameObject, 5f);
            GameObject.Find("Hud").GetComponent<UIManager>().EnemiesKilled++;
            enabled = false;
        }
    }
    private Vector3 GenerateSpawnPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * Random.Range(minDistance, maxDistance);
        randomDirection += transform.position;
        UnityEngine.AI.NavMeshHit navMeshHit;
        if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out navMeshHit, maxDistance, UnityEngine.AI.NavMesh.AllAreas))
        {
            return navMeshHit.position;
        }
        return Vector3.zero;
    }
}