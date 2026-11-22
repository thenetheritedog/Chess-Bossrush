using UnityEngine;
using UnityEngine.InputSystem;

public class FightingInput : MonoBehaviour
{
    CameraManager cameraManager;
    FighterPlayerManager playerManager;
    PlayerMovement playerMovement;
    void Start()
    {
        playerManager = GetComponent<FighterPlayerManager>();
        playerMovement = playerManager.move;
        cameraManager = playerManager.cam;

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnLook(InputValue inputVal)
    {
        playerManager.cam.MoveCamera(inputVal.Get<Vector2>());
    }

    public void OnMove(InputValue inputVal) 
    {
        playerManager.move.UpdateMovementVal(inputVal.Get<Vector2>());
    }

    public void OnJump(InputValue inputVal)
    {
        if (inputVal.Get<float>() > 0.5f)
            playerManager.move.Jump();
    }

    public void OnAttack(InputValue inputVal)
    {
        playerManager.anim.AnimationPlay("attack1",true);
    }

    public void OnSprint(InputValue inputVal)
    {
        playerManager.anim.AnimationPlay("dodge", true);
    }
}
