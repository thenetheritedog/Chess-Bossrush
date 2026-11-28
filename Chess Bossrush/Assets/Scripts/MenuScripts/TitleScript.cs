using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TitleScript : MonoBehaviour
{
    UIDocument uiDoc;
    VisualElement root;
    Button chess;
    Button fightingTest;
    Button quit;

    void OnEnable()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;


        uiDoc = GetComponent<UIDocument>();
        root = uiDoc.rootVisualElement;
        chess = root.Q<Button>("Chess");
        fightingTest = root.Q<Button>("Fight");
        quit = root.Q<Button>("Quit");


        chess.RegisterCallback<ClickEvent>(OnChessClicked);
        fightingTest.RegisterCallback<ClickEvent>(OnFightClicked);
        quit.RegisterCallback<ClickEvent>(OnQuit);
    }

    private void OnChessClicked(ClickEvent evt)
    {
        SceneManager.LoadScene(1);
    }

    private void OnFightClicked(ClickEvent evt)
    {
        SceneManager.LoadScene(2);
    }
    private void OnQuit(ClickEvent evt) 
    {
        Application.Quit();
    }
    // Update is called once per frame
}
