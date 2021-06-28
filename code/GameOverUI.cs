using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    private Button retry_button;
    [SerializeField]
    private Button quit_button;

    [SerializeField]
    private Animator gameover_animator;

    [SerializeField]
    private string GameOversound = "GameOver";

    // Start is called before the first frame update
    void Start()
    {
        if(gameover_animator == null)
        {
            Debug.LogError("No gameover_animator referenced: GameOverUI.cs");
        }
        else
        {
            gameover_animator.SetBool("Playgameover", false);
            
        }


        retry_button.onClick.AddListener(Retry);
        quit_button.onClick.AddListener(Quit);
    }

    // Update is called once per frame
    void Update()
    {
        PlayGameOver();
    }

    void Retry()
    {
        // 재시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    void Quit()
    {
        //프로그램 종료, 에디터상에서는 무시됨
        Application.Quit();
    }

    public void PlayGameOver()
    {
        if(GameMaster.gm.playerlives <= 0)
        {
            gameover_animator.SetBool("Playgameover", true);
            AudioManager.instance.PlaySound(GameOversound);
        }
    }

}
