
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float acceleration;
    [SerializeField] float friction;
    [SerializeField] LayerMask defaultMask;


    FighterPlayerManager playerMan;
    CameraManager cam;
    Rigidbody rb;

    GameObject playerModel;

    float dodgeTimer;

    bool grounded;
    bool isDodging;

    Vector2 moveInput;

    RaycastHit groundHit;



    void Start()
    {
        playerMan = GetComponent<FighterPlayerManager>();
        rb = GetComponent<Rigidbody>();
        playerModel = transform.GetChild(0).gameObject;
        //cam = playerMan.cam;

    }

    private void Update()
    {
        GroundCheck();
    }
    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        Vector3 dirOfPlayerInput = moveInput.y * playerMan.cam.transform.forward;
        dirOfPlayerInput += moveInput.x * playerMan.cam.transform.right;
        Vector3 vel = rb.linearVelocity;
        if (GroundCheck())
        {
            if (playerMan.anim.rootAnimPlaying)
            {
                vel = playerMan.anim.animator.deltaPosition/Time.deltaTime;
                transform.forward = transform.rotation * vel.normalized;
                vel = Vector3.ProjectOnPlane(vel, groundHit.normal).normalized * vel.magnitude;
                rb.linearVelocity = vel;
                //playerMan.anim.transform.localPosition = Vector3.zero;
                return;
            }
            if (dirOfPlayerInput.magnitude > 0)
                transform.forward = Vector3.ProjectOnPlane(dirOfPlayerInput,Vector3.up);
            dirOfPlayerInput = Vector3.ProjectOnPlane(dirOfPlayerInput, groundHit.normal);
            vel +=  acceleration * Time.deltaTime * dirOfPlayerInput.normalized;
            vel -= friction * Time.deltaTime * vel;
            vel = Vector3.ProjectOnPlane(vel,groundHit.normal);
            
            if (vel.magnitude < 1 && dirOfPlayerInput.magnitude == 0)
                vel *= 0;
            transform.forward = vel.normalized;
            playerMan.anim.animator.SetFloat("speed", vel.magnitude); 
        }
        else
        {
            vel += Physics.gravity * Time.deltaTime;
            transform.forward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
        }
        rb.linearVelocity = vel;
        
    }

    bool GroundCheck()
    {
        if (rb.linearVelocity.y > 0)
            return false;
        if (Physics.Raycast(transform.position + transform.up/2, -transform.up,out groundHit, grounded ? 0.6f : 0.5f, defaultMask))
        {
            transform.position += transform.up * (0.5f - groundHit.distance);
            grounded = true;
            return true;
        }
        grounded = false;
        return false;
    }
    public void UpdateMovementVal(Vector2 moveInputUpd)
    {
        moveInput = moveInputUpd;
    }

    public void Jump()
    {
        if (GroundCheck() && !playerMan.anim.rootAnimPlaying)
        {
            rb.linearVelocity += Vector3.up * 5f;
            grounded = false;
        }
    }

}
