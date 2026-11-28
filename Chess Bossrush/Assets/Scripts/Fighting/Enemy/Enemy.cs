using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float damageDealt;

    [SerializeField] float healthMax;
    [SerializeField] float speedWalk;
    [SerializeField] float speedRun;
    [SerializeField] float friction;
    [SerializeField] float distanceUntilHit;
    [SerializeField] Collider hitBox;
    [SerializeField] LayerMask defaultMask;

    FighterPlayerManager player;
    Animator animator;
    NavMeshAgent agent;
    Rigidbody rb;

    float health;
    float speed;

    bool rootAnimPlaying;
    bool grounded;
    bool inRangeOfAttack;

    Vector3 whatIsTheNextPoint;

    RaycastHit groundHit;


    public enum State
    {
        Idle,
        MatchTheAura,
        Attacking,
        DashAttacking,
        SlowlyWalkingUp,
        Run,
        Block
    }

    public State currentState;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = FindAnyObjectByType<FighterPlayerManager>();
        health = healthMax;
        agent.updatePosition = true;
        agent.updateRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = GroundCheck();
        
        agent.nextPosition = transform.position;

        if (!agent.pathPending)
        {
            agent.SetDestination(whatIsTheNextPoint);
        }
        if (!grounded)
        {
            rb.linearVelocity += Physics.gravity * Time.deltaTime;
        }
        else if ((transform.position - player.transform.position).magnitude < distanceUntilHit * 
            (currentState != State.Run ? 1f : 2f) && !inRangeOfAttack)
        {
            inRangeOfAttack = true;
            StartCoroutine(inRangeOfPlayer());
        }
        else if (!rootAnimPlaying)
        {
            SwitchStates();
            if (currentState != State.Block)
                animator.SetBool("blocking", false);
        }

        UsingStates();

    }

    void AttackPlayer()
    {
        if (!rootAnimPlaying)
        {
            animator.Play("attack1", 0);
            animator.applyRootMotion = true;
            rootAnimPlaying = true;
        }
        transform.forward = player.transform.position - transform.position;
        rb.linearVelocity = animator.deltaPosition / Time.deltaTime;
        rb.linearVelocity = rb.linearVelocity.magnitude * Vector3.ProjectOnPlane(rb.linearVelocity, groundHit.normal).normalized;
    }

    void MoveToPoint()
    {
        if (grounded && animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1f)
        {
            rb.linearVelocity += agent.desiredVelocity.normalized * Time.deltaTime * speed;
            rb.linearVelocity -= rb.linearVelocity * Time.deltaTime * friction;
            transform.forward = Vector3.MoveTowards(transform.forward, (player.transform.position - transform.position).normalized, 5f * Time.deltaTime);
            animator.SetFloat("speedZ", Vector3.Dot(rb.linearVelocity, transform.forward));
            animator.SetFloat("speedX", Vector3.Dot(rb.linearVelocity, transform.right));
        }
    }

    public void TurnOnCollider()
    {
        hitBox.enabled = true;
    }

    public void TurnOffCollider()
    {
        hitBox.enabled = false;
    }

    public void RemoveRootMotion()
    {
        rootAnimPlaying = false;
        animator.applyRootMotion = false;
    }
    bool GroundCheck()
    {

        if (Physics.Raycast(transform.position + Vector3.up / 2, -Vector3.up, out groundHit, grounded ? 0.65f : 0.55f, defaultMask))
        {
            Vector3 groundHitPoint = transform.position;
            groundHitPoint.y = groundHit.point.y;
            rb.MovePosition(groundHitPoint);
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Weapon>() != null)
        {
            health -= currentState != State.Block ? other.GetComponent<Weapon>().damageDealt: other.GetComponent<Weapon>().damageDealt/4;
            player.gameUI.SetEnemyHealth(health/healthMax);
            if (health < 0)
            {
                animator.Play("death");
                rootAnimPlaying=true;
                animator.applyRootMotion=true;
                StartCoroutine(player.GoBackToMenu());
            }
        }
    }


    protected virtual void SwitchStates()
    {
        switch (player.state)
        {
            case FighterPlayerManager.PlayerState.Idle:
            case FighterPlayerManager.PlayerState.SlowWalkUp:
                if (currentState != State.Run)
                    currentState = State.SlowlyWalkingUp;
                break;
            case FighterPlayerManager.PlayerState.AuraFarmWalkHorizontal:
                if (currentState != State.Run)
                    currentState = State.MatchTheAura;
                break;
            case FighterPlayerManager.PlayerState.Attack:
                currentState = State.Block;
                break;
            default:
                currentState = State.Run;
                break;
        }
    }
    protected virtual void UsingStates()
    {
        switch (currentState)
        {
            case State.Idle:
                break;
            case State.MatchTheAura:
                whatIsTheNextPoint = transform.position;
                whatIsTheNextPoint -= player.GetComponent<Rigidbody>().linearVelocity;
                speed = speedWalk;
                MoveToPoint();
                break;
            case State.SlowlyWalkingUp:
                whatIsTheNextPoint = player.transform.position;
                speed = speedWalk;
                MoveToPoint();
                break;
            case State.Attacking:
                whatIsTheNextPoint = player.transform.position;
                AttackPlayer();
                break;
            case State.Run:
                whatIsTheNextPoint = player.transform.position;
                speed = speedRun;
                MoveToPoint();
                break;
            case State.Block:
                animator.SetBool("blocking", true);
                if (!rootAnimPlaying)
                    animator.Play("block");
                whatIsTheNextPoint = transform.position;
                MoveToPoint();
                break;
        }
    }

    IEnumerator inRangeOfPlayer()
    {
        float ranTime = Random.Range(0.1f,1f);
        if (currentState == State.Run)
            ranTime = 0f;
        yield return new WaitForSeconds(ranTime);
        if (inRangeOfAttack)
            currentState = State.Attacking;
        if (ranTime == 0f)
        {
            animator.Play("dashAttack");
            animator.applyRootMotion = true;
            rootAnimPlaying = true;
        }
        inRangeOfAttack = false;
    }
    
}
