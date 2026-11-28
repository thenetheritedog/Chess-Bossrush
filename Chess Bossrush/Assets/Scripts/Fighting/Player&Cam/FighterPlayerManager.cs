using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FighterPlayerManager : MonoBehaviour
{
    public CameraManager cam { get; private set; }
    public PlayerMovement move { get; private set; }

    public AnimationManager anim { get; private set; }

    public Weapon weapon;

    public InGameUI gameUI;


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
    void Awake()
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
        if (other.GetComponent<Enemy>() != null)
        {
            return;
        }
        if (other.transform.root.gameObject.GetComponent<Enemy>() != null && state != PlayerState.Dodge)
        {
            if (state == PlayerState.Parry)
            {
                anim.animator.SetFloat("parryAmount", 0);
                weapon.parryParticle.Emit(25);
                Debug.Log("Parry");
            }
            else if (state == PlayerState.Block)
            {
                Debug.Log("Block");
                anim.AnimationPlay("blockHit", true);
                health -= other.transform.root.gameObject.GetComponent<Enemy>().damageDealt / 2;
            }
            else
                health -= other.transform.root.gameObject.GetComponent<Enemy>().damageDealt;

            Debug.Log(health);
            gameUI.SetPlayerHealth(health/healthMax);
            if (health < 0)
            {
                anim.AnimationPlay("death", true);
                StartCoroutine(GoBackToMenu());
            }
        }
    }


    public IEnumerator GoBackToMenu()
    {
        yield return new WaitForSeconds(14f);
        SceneManager.LoadScene(0);
    }
}
