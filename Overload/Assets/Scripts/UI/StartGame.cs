using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public GameObject mainGameUI;
    public Button btnStart;
    void Start()
    {
        btnStart.onClick.AddListener(()=>{
            onClickBtnStart();
        });
    }

    void onClickBtnStart()
    {
        Instantiate(mainGameUI,GameObject.Find("UIRoot").transform);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
