using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Action onGameStarted;
    private bool isGameStarted;
    private float currentTimeScale;
    private int score;
    private int money;
    [SerializeField]private GameObject[] animals;
    [SerializeField] private GameObject shit;
    [SerializeField] private Transform shitTransform;
    private GameObject currentAnimal;
    private int animalInt;
    public Transform animalSpawnTransform;
    private void Awake()
    {
        instance = this;
        currentTimeScale = Time.timeScale;
        if (PlayerPrefs.HasKey("Money"))
        {
            money = PlayerPrefs.GetInt("Money");
        }
        else
        {
            PlayerPrefs.SetInt("Money", 0);
            PlayerPrefs.Save();
        }
        
        if (!PlayerPrefs.HasKey("F1"))
        {
            for (int i = 0; i < animals.Length; i++)
            {
                PlayerPrefs.SetInt("F" + i, 0);
            }
            PlayerPrefs.Save();
        }    
    }
    private void Start()
    {
        UIManager.instance.ShowMoney(money.ToString());
    }
    private void Update()
    {
        if (isGameStarted)
        {
            score += 1;
            UIManager.instance.ShowScore(score.ToString());
        }
    }
    public void StartGame()
    {
        isGameStarted = true;
        onGameStarted?.Invoke();
        Time.timeScale = 1f;
        animalInt = UnityEngine.Random.Range(0, animals.Length);
        currentAnimal = Instantiate(animals[animalInt]);
        currentAnimal.transform.position = animalSpawnTransform.position;
        UIManager.instance.MoveCameraToFeed();
    }
    public void PauseGame()
    {
        isGameStarted = false;
        Time.timeScale = 0f;
    }
    public void UnPauseGame()
    {
        isGameStarted = true;
        Time.timeScale = currentTimeScale;
    }
    public void EndGame(bool isWin)
    {
        isGameStarted = false;
        CheckBestScore();
        if (!isWin)
        {
            UIManager.instance.losePanel.SetActive(true);
            currentAnimal.GetComponent<Animator>().SetBool("isDead", true);
        }
        else
        {
            PlayerPrefs.SetInt("F" + animalInt, PlayerPrefs.GetInt("F" + animalInt) + 1);
            PlayerPrefs.Save();
            SpawnShit();
        }
    }
    private void SpawnShit()
    {
        UIManager.instance.questionPanel.SetActive(false);
        for (int i=0; i<15;i++)
        {
            GameObject newShit = Instantiate(shit);
            newShit.transform.position = new Vector3(shitTransform.position.x+(UnityEngine.Random.Range(-0.5f,0.5f)), shitTransform.position.y + (UnityEngine.Random.Range(-0.5f, 0.5f)), shitTransform.position.z + (UnityEngine.Random.Range(-0.2f, 0.2f)));
        }
        StartCoroutine(WaitToShowWinPanel());
    }
    private IEnumerator WaitToFeed()
    {
        yield return new WaitForSeconds(0.5f);
        currentAnimal.GetComponent<Animator>().SetBool("isFeeding", true);
    }
    private IEnumerator WaitToShowWinPanel()
    {
        StartCoroutine(WaitToFeed());
        yield return new WaitForSeconds(4);
        
        UIManager.instance.winPanel.SetActive(true);

    }
    private void CheckBestScore()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            int tempBestScore = PlayerPrefs.GetInt("BestScore");
            if (tempBestScore > score)
            {
                UIManager.instance.ShowBestScore(tempBestScore.ToString());
            }
            else
            {
                UIManager.instance.ShowBestScore(score.ToString());
                PlayerPrefs.SetInt("BestScore", score);
                PlayerPrefs.Save();
            }
        }
        else
        {
            UIManager.instance.ShowBestScore(score.ToString());
            PlayerPrefs.SetInt("BestScore", score);
            PlayerPrefs.Save();
        }
    }
    public bool IsGameStarted()
    {
        return isGameStarted;
    }
}
