using UnityEngine;

public class Weapon : MonoBehaviour
{
    FighterPlayerManager player;
    Collider attackCollider;
    void Start()
    {
        player = FindAnyObjectByType<FighterPlayerManager>();
        attackCollider = GetComponent<Collider>();
        TurnOffCollider();
    }

    public void TurnOnCollider() 
    {
        attackCollider.enabled = true;
    }

    public void TurnOffCollider()
    {
        attackCollider.enabled = false;
    }
}
