using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public bool rootAnimPlaying { get; private set; }

    public Animator animator { get; private set; }

    FighterPlayerManager player;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void AnimationPlay(string animation, bool root = false)
    {
        if (rootAnimPlaying)
            return;
        animator.Play(animation);
        rootAnimPlaying = root;
    }

    public void RemoveRootMotion()
    {
        rootAnimPlaying = false;
    }
}
