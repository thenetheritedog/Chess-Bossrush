using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] PieceLogic pieceUsing;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FindPiece(Vector2 mousePos)
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out RaycastHit hit);
        if (hit.collider.GetComponent<PieceLogic>() != null)
        {
            if (pieceUsing != hit.collider.GetComponent<PieceLogic>())
            {
                pieceUsing = hit.collider.GetComponent<PieceLogic>();
                pieceUsing.UsePiece();
            }
            else { pieceUsing = null; }
            
        }
    }
}
