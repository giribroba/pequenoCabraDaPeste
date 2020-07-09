using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunnerController : MonoBehaviour
{
    public enum State { beforeRunner, inRunner, afterRunner }
    public State currentState;

    public bool arcade;

    public static RunnerController instace;
    public RunnerRotate runnerRotate;
    public GameObject btnPause, btnJump, btnSlide, winGameFadeOut;

    void Awake()
    {

        instace = this;
        currentState = State.beforeRunner;

        arcade = MenuAlfa.instance != null ? MenuAlfa.instance.isArcade : arcade;
        if (MenuAlfa.instance != null) Destroy(MenuAlfa.instance.gameObject);

    }

    private void Update()
    {

        if (!Progress.instance.endGame)
        {

            if (MenuBotoesBehaviour.inPause || RunnerController.instace.currentState != RunnerController.State.inRunner)
            {

                btnPause.SetActive(false);
                btnJump.SetActive(false);
                btnSlide.SetActive(false);

            }
            else
            {

                btnPause.SetActive(true);
                btnJump.SetActive(true);
                btnSlide.SetActive(true);

            }

        }

        else
        {

            btnPause.SetActive(false);
            btnJump.SetActive(false);
            btnSlide.SetActive(false);

            Progress.instance.imageProgress.SetActive(false);
            Progress.instance.player.SetActive(false);
            Progress.instance.snake.SetActive(false);

        }

        if (Progress.instance.endGame)
        {

            StartCoroutine(WinGame());

        }

        IEnumerator WinGame()
        {

            winGameFadeOut.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            SceneManager.LoadScene("MenuAlfa");

        }

    }

}
