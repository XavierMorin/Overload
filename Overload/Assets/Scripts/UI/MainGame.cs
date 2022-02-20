using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    public Button btnGoOn;
    public Button btnPause;
    public Text txtScore;
    public Text txtNumOfItems;
    public GameObject pausePanel;
    public MainCharacter mainCharacter;
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

    void Update()
    {
        refreshScore();
        UpdateAmountOfLuagagge();
    }
    public void refreshScore()
    {
        txtScore.text = mainCharacter.score.ToString();
    }

    public void UpdateAmountOfLuagagge()
    {
        txtNumOfItems.text = "Num of Items: " + mainCharacter.amountOfLuaggages.ToString();
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
}
