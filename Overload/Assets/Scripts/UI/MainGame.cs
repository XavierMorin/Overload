using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    public Button btnGoOn;
    public Button btnPause;
    public static Text txtScore;
    public GameObject pausePanel;
    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
        btnGoOn.onClick.AddListener(()=>{
            onClickBtnGoOn();
        });
        btnPause.onClick.AddListener(()=>{
            onClickBtnPause();
        });
    }

    public static void refreshScore()
    {
        int score = PlayerPrefs.GetInt("score");
        txtScore.text = score.ToString();
    }

    void onClickBtnGoOn()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    void onClickBtnPause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
