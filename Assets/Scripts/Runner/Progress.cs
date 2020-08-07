using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progress : MonoBehaviour
{

    public GameObject startPlayerPosition, startSnakePosition;
    public GameObject imageProgress, player, snake;

    public static Progress instance;

    float time; public bool endGame;

    private void Awake()
    {

        instance = this;
        StartCoroutine(Ab());

    }

    private void Update()
    {

        if (!RunnerController.instace.arcade)
        {

            if (RunnerController.instace.currentState == RunnerController.State.beforeRunner || MenuBotoesBehaviour.inPause)
            {

                snake.GetComponent<RectTransform>().transform.position = startSnakePosition.transform.position;
                player.GetComponent<RectTransform>().transform.position = startPlayerPosition.transform.position;

                time = 0;

                imageProgress.GetComponent<Image>().color = new Color(imageProgress.GetComponent<Image>().color.r, imageProgress.GetComponent<Image>().color.g, imageProgress.GetComponent<Image>().color.b, 0);
                player.GetComponent<Image>().color = new Color(player.GetComponent<Image>().color.r, player.GetComponent<Image>().color.g, player.GetComponent<Image>().color.b, 0);
                snake.GetComponent<Image>().color = new Color(snake.GetComponent<Image>().color.r, snake.GetComponent<Image>().color.g, snake.GetComponent<Image>().color.b, 0);

            }

            else if (RunnerController.instace.currentState == RunnerController.State.inRunner || !MenuBotoesBehaviour.inPause &&
            RunnerController.instace.currentState == RunnerController.State.inRunner)
            {

                imageProgress.GetComponent<Image>().color = new Color(imageProgress.GetComponent<Image>().color.r, imageProgress.GetComponent<Image>().color.g, imageProgress.GetComponent<Image>().color.b, 1);
                player.GetComponent<Image>().color = new Color(player.GetComponent<Image>().color.r, player.GetComponent<Image>().color.g, player.GetComponent<Image>().color.b, 1);
                snake.GetComponent<Image>().color = new Color(snake.GetComponent<Image>().color.r, snake.GetComponent<Image>().color.g, snake.GetComponent<Image>().color.b, 1);

                if (!endGame)
                {

                    player.GetComponent<RectTransform>().position += new Vector3(1.2f, 0f, 0f) * Time.deltaTime;
                    snake.GetComponent<RectTransform>().position += new Vector3(1.2f, 0f, 0f) * Time.deltaTime;

                }
                else
                {

                    player.GetComponent<RectTransform>().position += Vector3.zero;
                    snake.GetComponent<RectTransform>().position += Vector3.zero;

                }

            }

            else if (RunnerController.instace.currentState == RunnerController.State.afterRunner)
            {

                imageProgress.GetComponent<Image>().color = new Color(imageProgress.GetComponent<Image>().color.r, imageProgress.GetComponent<Image>().color.g, imageProgress.GetComponent<Image>().color.b, 0);
                player.GetComponent<Image>().color = new Color(player.GetComponent<Image>().color.r, player.GetComponent<Image>().color.g, player.GetComponent<Image>().color.b, 0);
                snake.GetComponent<Image>().color = new Color(snake.GetComponent<Image>().color.r, snake.GetComponent<Image>().color.g, snake.GetComponent<Image>().color.b, 0);

            }

        }
        else
        {

            imageProgress.SetActive(false);
            player.SetActive(false);
            snake.SetActive(false);

        }

    }

    IEnumerator Ab()
    {

        //Debug.LogWarning(time++);
        yield return new WaitForSeconds(1);
        StartCoroutine(Ab());

    }

}
