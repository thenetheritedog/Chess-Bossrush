using UnityEngine;

public class FighterPlayerManager : MonoBehaviour
{
    public CameraManager cam { get; private set; }
    public PlayerMovement move { get; private set; }

    public AnimationManager anim { get; private set; }

    public Weapon weapon;


    [SerializeField] float healthMax;

    float health;

    public enum PlayerState
    {
        Idle,
        SlowWalkUp,
        AuraFarmWalkHorizontal,
        Running,
        Attack,
        Dodge,
        Parry,
        Block
    }
    

    public PlayerState state;
    void Start()
    {
        Time.fixedDeltaTime = 1 / 60f;
        Application.targetFrameRate = 60;
        cam = FindAnyObjectByType<CameraManager>();
        move = GetComponent<PlayerMovement>();
        anim = GetComponent<AnimationManager>();
        health = healthMax;
    }


    public void TurnOnCollider()
    {
        weapon.GetComponent<Collider>().enabled = true;
    }

    public void TurnOffCollider()
    {
        weapon.GetComponent<Collider>().enabled = false;
    }

    public void BlockingNow()
    {
        state = PlayerState.Block;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.GetComponent<Enemy>() != null && state != PlayerState.Dodge)
        {
            if (state == PlayerState.Parry)
            {
                anim.animator.SetFloat("parryAmount", 0);
                Debug.Log("Parry");
            }
            else if (state == PlayerState.Block)
            {
                Debug.Log("Block");
                health -= other.transform.root.gameObject.GetComponent<Enemy>().damageDealt / 2;
            }
            else
                health -= other.transform.root.gameObject.GetComponent<Enemy>().damageDealt;

            Debug.Log(health);
        }
    }
}
