using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuSelect : MonoBehaviour
{

    [SerializeField]
    private Button StartButton;

    [SerializeField]
    private Button QuitButton;

    [SerializeField]
    private string ButtonPressedMusic = "ButtonPress";

    

    // Start is called before the first frame update
    void Start()
    {
        if (StartButton == null || QuitButton == null)
        {
            Debug.LogError("No reference Button in MenuSelect");

        }

        StartButton.onClick.AddListener(GameStart);
        QuitButton.onClick.AddListener(Quit);

    }

    // Update is called once per frame
    void Update()
    {



    }

    private void Quit()
    {
        AudioManager.instance.PlaySound(ButtonPressedMusic);
        Application.Quit();
    }

    private void GameStart()
    {
        AudioManager.instance.PlaySound(ButtonPressedMusic);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    

}
