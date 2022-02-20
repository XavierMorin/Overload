using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameEnd : MonoBehaviour
{
    public Button btnReStart;
    public Text[] txtScore;
    public Text textCurrentScore;
    public int intCurrentScore;
    public string[] scoreNameList = {"First", "Second", "Third"};

    // Start is called before the first frame update
    void Start()
    {
        btnReStart.onClick.AddListener(() =>
        {
            onClickBtnReStart();
        });
        intCurrentScore = PlayerPrefs.GetInt("CurrentScore");
        textCurrentScore.text = "Score: "+intCurrentScore.ToString();
        CompareScore();
        SetScoreList();
    }

    void CompareScore()
    {
        for (int i = 0; i < 3; i++)
        {
            int score = PlayerPrefs.GetInt(scoreNameList[i], 0);
            if (score < intCurrentScore)
            {
                PlayerPrefs.SetInt(scoreNameList[i], intCurrentScore);
                PassDownScore(++i, score);
                break;
            }
        }
    }
    void PassDownScore(int i, int score){
        if (i == 3)return;
        int score1 = PlayerPrefs.GetInt(scoreNameList[i], 0);
        PlayerPrefs.SetInt(scoreNameList[i], score);
        PassDownScore(++i, score1);
    }
    void SetScoreList()
    {
        for (int i = 0; i < 3; i++)
        {

            txtScore[i].text = scoreNameList[i] + ": " + PlayerPrefs.GetInt(scoreNameList[i]).ToString();
        }
    }

    void onClickBtnReStart()
    {
        SceneManager.LoadScene(2);
    }
}
