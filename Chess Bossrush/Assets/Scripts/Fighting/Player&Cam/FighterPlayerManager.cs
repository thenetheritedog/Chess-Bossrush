using UnityEngine;

public class FighterPlayerManager : MonoBehaviour
{
    public CameraManager cam { get; private set; }
    public PlayerMovement move { get; private set; }

    public AnimationManager anim { get; private set; }

    public Weapon weapon;
    void Start()
    {
        Time.fixedDeltaTime = 1 / 60f;
        Application.targetFrameRate = 60;
        cam = FindAnyObjectByType<CameraManager>();
        move = GetComponent<PlayerMovement>();
        anim = GetComponent<AnimationManager>();
    }


    public void TurnOnCollider()
    {
        weapon.GetComponent<Collider>().enabled = true;
    }

    public void TurnOffCollider()
    {
        weapon.GetComponent<Collider>().enabled = false;
    }
}
