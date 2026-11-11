using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    PlayerScript player;
    void Start()
    {
        player = GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(InputAction inputVal)
    {
        player.FindPiece(inputVal.ReadValue<Vector2>());   
    }
}
