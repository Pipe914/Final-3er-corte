using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIControllerContadorEnemigos : MonoBehaviour
{
    VisualElement root;
    Label contadorStatic, contadorFlying;

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement.Q("Cont-Enemys");
        contadorStatic = root.Q<Label>("ContadorEnemigosStatic");
        contadorFlying = root.Q<Label>("ContadorEnemigosFlying");
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeContadorStatic(int enemys)
    {
        contadorStatic.text = enemys.ToString();
    }

    public void ChangeContadorFlying(int enemys)
    {
        contadorFlying.text = enemys.ToString();
    }
}
