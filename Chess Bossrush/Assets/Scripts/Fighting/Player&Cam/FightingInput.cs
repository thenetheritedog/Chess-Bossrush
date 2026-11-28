using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class FightingInput : MonoBehaviour
{

    FighterPlayerManager playerManager;
    void Start()
    {
        playerManager = GetComponent<FighterPlayerManager>();


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
        if (playerManager.move.grounded && !playerManager.anim.rootAnimPlaying)
            playerManager.anim.AnimationPlay("attack1",true);
    }

    public void OnSprint(InputValue inputVal)
    {
        playerManager.move.Sprint(inputVal.Get<float>() > 0.5f);
    }

    public void OnMenu(InputValue inputVal)
    {
        SceneManager.LoadScene(0);
    }

    public void OnLockOn(InputValue inputVal)
    {
        playerManager.cam.SetUpLockOn();
    }

    public void OnBlock(InputValue inputVal)
    {
        playerManager.weapon.Parry(inputVal.Get<float>() > 0.5f);
    }
}
