
using UnityEngine;
using UnityEngine.UI;

public class BoardLogic : MonoBehaviour
{
    public PieceLogic[,] boardState = new PieceLogic[8, 8];
    [SerializeField] GameObject litSquareInst;

    private void Start()
    {
        for (int i = 0; i < 64; i++)
        {
            Instantiate(litSquareInst, transform);
        }

    }
    public bool CheckIfValidMove(int xCord, int yCord)
    {


        if (xCord > 7 || yCord > 7 || xCord < 0 || yCord < 0)
            return false;
        GameObject lightUp = transform.GetChild(xCord + yCord * 8).gameObject;
        lightUp.GetComponent<Image>().color = Color.white;
        
        return true;
        
        
    }

    public void RemoveAllLitUpSquares()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.clear;

        }
    }
}
