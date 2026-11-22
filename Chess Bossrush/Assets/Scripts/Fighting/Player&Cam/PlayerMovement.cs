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

    bool grounded;

    Vector2 moveInput;

    RaycastHit groundHit;
    
    void Start()
    {
        playerMan = GetComponent<FighterPlayerManager>();
        rb = GetComponent<Rigidbody>();
        playerModel = transform.GetChild(0).gameObject;
        //cam = playerMan.cam;
    }

    // Update is called once per frame
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
                vel = playerMan.anim.transform.localPosition /Time.deltaTime;
                vel = transform.rotation * vel;
                vel = Vector3.ProjectOnPlane(vel, groundHit.normal).normalized * vel.magnitude;
                rb.linearVelocity = vel;
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

            playerMan.anim.animator.SetFloat("speed", vel.magnitude); 
        }
        else
        {
            vel += Physics.gravity * Time.deltaTime;
        }
        rb.linearVelocity = vel;
        
    }

    bool GroundCheck()
    {
        if (rb.linearVelocity.y > 0)
            return false;
        if (Physics.Raycast(transform.position + Vector3.up/2, Vector3.down,out groundHit, grounded ? 0.6f : 0.5f, defaultMask))
        {
            rb.MovePosition(groundHit.point);
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
