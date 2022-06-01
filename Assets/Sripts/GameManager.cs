using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] UIDocument ContadorEnemigos;
    [SerializeField] UIDocument WinScreen;
    private int flyingEnemys, staticEnemys;
    [SerializeField] AudioClip Inicio; 
    [SerializeField] AudioClip Escena;
    private AudioSource musiquita; 

    // Start is called before the first frame update
    void Start()
    {
        musiquita = GetComponent<AudioSource>();
        StartCoroutine(StartGame());

    }

    // Update is called once per frame
    void Update()
    {
        if (flyingEnemys <= 0 && staticEnemys <=0)
        {
            StartCoroutine(WinGame()); 
        }
    }

    public IEnumerator startingCounters()
    {
        flyingEnemys = GameObject.FindGameObjectsWithTag("FlyingEnemy").Length;
        staticEnemys = GameObject.FindGameObjectsWithTag("StaticEnemy").Length;
        yield return new WaitForSeconds(.5f);
        FindObjectOfType<UIControllerContadorEnemigos>().ChangeContadorFlying(flyingEnemys);
        FindObjectOfType<UIControllerContadorEnemigos>().ChangeContadorStatic(staticEnemys);
        ContadorEnemigos.GetComponent<UIDocument>().rootVisualElement.Q("Cont-Enemys").style.display = DisplayStyle.Flex;
        musiquita.Stop();
        musiquita.clip = Escena; 
        musiquita.Play();
        yield return null;

    }

    public IEnumerator countFlyingEnemys()
    {

        flyingEnemys--; 
        
        FindObjectOfType<UIControllerContadorEnemigos>().ChangeContadorFlying(flyingEnemys);

        yield return null;
    }

    public IEnumerator countStaticEnemys()
    {
        staticEnemys--;

        FindObjectOfType<UIControllerContadorEnemigos>().ChangeContadorStatic(staticEnemys);

        yield return null;
    }

    IEnumerator StartGame()
    {
        musiquita.clip = Inicio; 
        musiquita.loop = true;
        musiquita.volume = 0.05f;
        musiquita.priority = 256;
        musiquita.Play(); 
        StartCoroutine(startingCounters());
        Time.timeScale = 0;
        yield return null;
    }


    IEnumerator WinGame()
    {
        ContadorEnemigos.GetComponent<UIDocument>().rootVisualElement.Q("Cont-Enemys").style.display = DisplayStyle.None;
        WinScreen.GetComponent<UIDocument>().rootVisualElement.Q("Cont-WinScreen").style.display = DisplayStyle.Flex;
        Time.timeScale = 0;
        yield return null;
    }

    public IEnumerator RestartGame()
    {
        SceneManager.LoadScene("Megaman");
        yield return null;
    }
    public IEnumerator StopMusic()
    {
        musiquita.Stop();
        yield return null;
    }
}
