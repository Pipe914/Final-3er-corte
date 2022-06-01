using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIControllerWinScreen : MonoBehaviour
{
    VisualElement root;
    Button restart;

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement.Q("Cont-WinScreen");
        restart = root.Q<Button>("button-restart");

        restart.clicked += RestartGame;
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void RestartGame()
    {
        root.style.display = DisplayStyle.None;
        StartCoroutine(FindObjectOfType<GameManager>().RestartGame());
        Time.timeScale = 1;
    }

}
