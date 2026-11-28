using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damageDealt;

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
        if (hold)
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
}
