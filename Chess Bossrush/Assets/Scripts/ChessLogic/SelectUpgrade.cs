using UnityEngine;

public class SelectUpgrade : MonoBehaviour
{
    [SerializeField] PieceLogic prefab;
    BoardLogic board;


    private void Start()
    {
        board = FindAnyObjectByType<BoardLogic>();
    }

    public void ClickOnUpgrade()
    {
        board.SetupNewPiece(prefab);
    }
}
