using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{

    PlayerScript player;
    InputAction mouseAction;
    void Start()
    {
        player = GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnPoint(InputValue inputVal)
    {
        player.mousePos = inputVal.Get<Vector2>();

    }

    public void OnClick(InputValue inputVal)
    {
        if (inputVal.Get<float>() > 0.5f)
            player.FindPiece();

    }
}
