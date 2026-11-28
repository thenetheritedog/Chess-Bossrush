using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damageDealt;

    public ParticleSystem parryParticle;

    FighterPlayerManager player;
    Collider attackCollider;

    ParticleSystem hitParticle;
    

    void Start()
    {
        player = FindAnyObjectByType<FighterPlayerManager>();
        attackCollider = GetComponent<Collider>();
        hitParticle = GetComponent<ParticleSystem>();
        player.weapon = this;
    }

    public void Parry(bool hold)
    {

        if (hold && player.move.grounded && !player.anim.rootAnimPlaying)
        {
            StopAllCoroutines();
            player.state = FighterPlayerManager.PlayerState.Parry;
            player.anim.AnimationPlay("parry", true);
            player.anim.animator.SetFloat("parryAmount", player.anim.animator.GetFloat("parryAmount") + 0.2f);
            player.anim.animator.SetBool("blocking", true);
        }
        else
            StartCoroutine(StopBlocking());

    }
    

    private void OnTriggerEnter(Collider other)
    {
        hitParticle.Emit(20);
    }

    IEnumerator StopBlocking()
    {
        player.anim.animator.SetBool("blocking", false);
        yield return new WaitForSeconds(1f);
        player.anim.animator.SetFloat("parryAmount", -0.2f);
    }

    public void AttackBehavior()
    {
        StopAllCoroutines();
        if (player.state == FighterPlayerManager.PlayerState.Running)
            player.anim.AnimationPlay("dashAttack", true);
        else if (player.state != FighterPlayerManager.PlayerState.Attack && !player.anim.rootAnimPlaying)
            player.anim.AnimationPlay("attack1", true);
        else
            StartCoroutine(AttackForNextAnimation());
        player.state = FighterPlayerManager.PlayerState.Attack;
        ;

    }

    IEnumerator AttackForNextAnimation()
    {
        player.anim.animator.SetBool("attacks", true);
        yield return new WaitForSeconds(1f);
        player.anim.animator.SetBool("attacks", false);
    }

}
