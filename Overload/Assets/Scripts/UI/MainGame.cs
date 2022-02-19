using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    public Button btnGoOn;
    public Button btnPause;
    public Text txtScore;
    public GameObject pausePanel;
    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
        refreshScore(0);
        btnGoOn.onClick.AddListener(()=>{
            onClickBtnGoOn();
        });
        btnPause.onClick.AddListener(()=>{
            onClickBtnPause();
        });
    }

    void refreshScore(int score)
    {
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
