using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour
{
    public GameObject objStartGame;
    public Button btnReStart;
    public Text[] txtScore;

    // Start is called before the first frame update
    void Start()
    {
        btnReStart.onClick.AddListener(() =>
        {
            onClickBtnReStart();
        });
        int[] data = {1,2,3,4,5,6,7,8,9,10};
        setScoreList(data);
    }

    void setScoreList(int[] scoreDatas){
        for(int i = 1;i<=scoreDatas.Length; i++){
            txtScore[i].text = scoreDatas[i].ToString();
        }
    }

    void onClickBtnReStart()
    {
        Instantiate(objStartGame, GameObject.Find("UIRoot").transform);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
