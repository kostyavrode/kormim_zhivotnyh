using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private TMP_Text[] answers;
    [SerializeField] private TMP_Text question;
    private int num1;
    private int num2;
    private string act;
    private int rightAnswer;
    private int stage;
    private void Awake()
    {
        GameManager.onGameStarted += CreateGame;
    }
    private void OnDisable()
    {
        
    }
    private void CreateGame()
    {
        num1 = Random.Range(0, 100);
        num2 = Random.Range(0, 100);
        switch(Random.Range(0,3))
        {
            case 0:
                {
                    act = "+";
                    rightAnswer = num1 + num2;
                    question.text = num1 + "+" + num2;
                    break;
                }
            case 1:
                {
                    act = "-";
                    rightAnswer = num1 - num2;
                    question.text = num1 + "-" + num2;
                    break;
                }
            case 2:
                {
                    rightAnswer = num1 * num2;
                    question.text = num1 + "*" + num2;
                    act = "*";
                    break;
                }
            default:
                {
                    rightAnswer = num1 + num2;
                    question.text = num1 + "+" + num2;
                    act = "+";
                    break;
                }
        }
        CreateAnswers();
        
    }
    private void MakeRightChoce()
    {
        Debug.Log("RIGHT!");
        stage+=1;
        if (stage==4)
        {
            GameManager.instance.EndGame(true);
        }
        CreateGame();
    }
    private void MakeFalseChoice()
    {
        Debug.Log("FALSE");
        GameManager.instance.EndGame(false);
    }
    private void CreateAnswers()
    {
        foreach(TMP_Text texts in answers)
        {
            texts.text = Random.Range(0, rightAnswer + 5).ToString();
        }
        answers[Random.Range(0, 4)].text = rightAnswer.ToString();
    }
    public void MakeChoice(int a)
    {
        string answerType = answers[a].text;
        if (rightAnswer.ToString()==answerType)
        {
            MakeRightChoce();
        }
        else
        {
            MakeFalseChoice();
        }
    }
}
