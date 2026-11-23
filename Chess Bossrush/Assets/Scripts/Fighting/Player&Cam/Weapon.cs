using UnityEngine;

public class Weapon : MonoBehaviour
{
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

    

    private void OnTriggerEnter(Collider other)
    {
        hitParticle.Emit(20);
    }
}
