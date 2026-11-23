
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float acceleration;
    [SerializeField] float friction;
    [SerializeField] LayerMask defaultMask;

    public bool grounded { get; private set; }

    FighterPlayerManager playerMan;
    CameraManager cam;
    Rigidbody rb;

    GameObject playerModel;

    float dodgeTimer;

    
    bool isDodging;

    Vector2 moveInput;

    RaycastHit groundHit;



    void Start()
    {
        grounded = false;
        playerMan = GetComponent<FighterPlayerManager>();
        rb = GetComponent<Rigidbody>();
        playerModel = transform.GetChild(0).gameObject;
        //cam = playerMan.cam;

    }


    void FixedUpdate()
    {

        grounded = GroundCheck();

        Movement();
    }

    void Movement()
    {
        Vector3 dirOfPlayerInput = moveInput.y * playerMan.cam.transform.forward;
        dirOfPlayerInput += moveInput.x * playerMan.cam.transform.right;
        Vector3 vel = rb.linearVelocity;
        if (grounded)
        {
            if (playerMan.anim.rootAnimPlaying)
            {
                vel = playerMan.anim.animator.deltaPosition/Time.deltaTime;
                
                vel = Vector3.ProjectOnPlane(vel, groundHit.normal).normalized * vel.magnitude;
                rb.linearVelocity = vel;
                transform.forward = Vector3.ProjectOnPlane(transform.forward,groundHit.normal).normalized;
                //playerMan.anim.transform.localPosition = Vector3.zero;
                return;
            }

            dirOfPlayerInput = Vector3.ProjectOnPlane(dirOfPlayerInput, groundHit.normal);
            vel +=  acceleration * Time.deltaTime * dirOfPlayerInput.normalized;
            vel -= friction * Time.deltaTime * vel;
            vel = Vector3.ProjectOnPlane(vel,groundHit.normal);
            
            if (vel.magnitude < 1 && dirOfPlayerInput.magnitude == 0)
            {
                transform.forward = Vector3.ProjectOnPlane(transform.forward,groundHit.normal).normalized;
                vel *= 0;
            }
            else
            {
                transform.forward = vel.normalized;
            }
            playerMan.anim.animator.SetFloat("speed", vel.magnitude); 
        }
        else
        {
            vel += Physics.gravity * Time.deltaTime;
            transform.forward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
            playerMan.anim.animator.SetFloat("yVel", vel.y);
        }
        rb.linearVelocity = vel;
        
    }

    bool GroundCheck()
    {
        if (rb.linearVelocity.y > 0 && !grounded)
            return false;
        if (Physics.Raycast(transform.position + Vector3.up/2, -Vector3.up,out groundHit, grounded ? 0.65f : 0.5f, defaultMask))
        {
            Vector3 groundHitPoint = transform.position;
            groundHitPoint.y = groundHit.point.y;
            rb.MovePosition(groundHitPoint);
            return true;
        }
        return false;
    }
    public void UpdateMovementVal(Vector2 moveInputUpd)
    {
        moveInput = moveInputUpd;
    }

    public void Jump()
    {
        if (grounded && !playerMan.anim.rootAnimPlaying)
        {
            rb.linearVelocity += Vector3.up * 5f;
            grounded = false;
        }
    }

}
