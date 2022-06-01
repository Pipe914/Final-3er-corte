using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIControllerMainScreen : MonoBehaviour
{
    VisualElement root;
    Button start;

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement.Q("Cont-MainScreen");
        start = root.Q<Button>("button-start");

        start.clicked += StartGame;
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame()
    {
        root.style.display = DisplayStyle.None;
        StartCoroutine(FindObjectOfType<GameManager>().startingCounters());
        Time.timeScale = 1;
    }

}
