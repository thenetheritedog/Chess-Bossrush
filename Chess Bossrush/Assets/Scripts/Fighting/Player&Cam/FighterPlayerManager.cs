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
        anim = transform.GetChild(0).GetComponent<AnimationManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
