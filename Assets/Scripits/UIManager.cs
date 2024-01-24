using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject cameraObj;
    [SerializeField] private Transform startCameraPos;
    [SerializeField] private Transform feedCameraPos;
    [SerializeField] private TMP_Text scoreBar;
    [SerializeField] private TMP_Text bestScoreBar;
    [SerializeField] private TMP_Text moneyBar;
    [SerializeField] public GameObject startPanel;
    [SerializeField] public GameObject inGamePanel;
    [SerializeField] public GameObject losePanel;
    [SerializeField] public GameObject winPanel;
    [SerializeField] public GameObject questionPanel;
    [SerializeField] public GameObject[] elements;
    [SerializeField] private GameObject blackWindow;
    [SerializeField] private AudioSource source;
    public UniWebView uniWebView;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            CloseUI();
        }
    }
    public void StartGame()
    {
        GameManager.instance.StartGame();
    }
    public void MoveCameraToFeed()
    {
        cameraObj.transform.DOMove(feedCameraPos.position, 2f);
        cameraObj.transform.DORotateQuaternion(feedCameraPos.rotation, 2f);
    }
    public void ShowMoney(string money)
    {
        moneyBar.text = money;
    }
    public void PauseGame()
    {
        GameManager.instance.PauseGame();
    }
    public void UnPauseGame()
    {
        GameManager.instance.UnPauseGame();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ShowScore(string score)
    {
        scoreBar.text = score;
    }
    public void ShowBestScore(string bestScore)
    {
        bestScoreBar.text = bestScore;
    }
    public void CloseQuestionPanel()
    {

    }
    public void CloseUI()
    {
        source.Pause();
        foreach (GameObject obj in elements)
        {
            obj.SetActive(false);
        }
        blackWindow.SetActive(true);
        
    }
    public void ShowPrivacy(string url)
    {
        var webviewObject = new GameObject("UniWebview");
        uniWebView = webviewObject.AddComponent<UniWebView>();
        uniWebView.Frame = new Rect(0, 0, Screen.width, Screen.height);
        uniWebView.SetShowToolbar(true, false, true, true);
        uniWebView.Load(url);
        uniWebView.Show();
    }
}
