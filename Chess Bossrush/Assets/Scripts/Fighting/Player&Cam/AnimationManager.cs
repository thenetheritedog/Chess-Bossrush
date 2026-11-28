using Unity.VisualScripting;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public bool rootAnimPlaying { get; private set; }

    public Animator animator { get; private set; }

    FighterPlayerManager player;
    
    void Start()
    {
        player = GetComponent<FighterPlayerManager>();
        animator = GetComponent<Animator>();
        rootAnimPlaying = false;
        animator.applyRootMotion = false;
    }

    private void Update()
    {
        animator.SetBool("grounded",player.move.grounded);
    }
    public void AnimationPlay(string animation, bool root = false)
    {
        animator.Play(animation);
        rootAnimPlaying = root;
        animator.applyRootMotion = root;
    }

    public void RemoveRootMotion()
    {
        rootAnimPlaying = false;
        animator.applyRootMotion = false;
    }


}
