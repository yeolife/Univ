using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Text 사용을 위해 추가
using UnityEngine.UI;
//Scene Load를 위해 추가
using UnityEngine.SceneManagement;
public class EndPanelManager : MonoBehaviour
{
    public Text Score;
    public Text BestScore;

    public GameObject newImage;
    public GameObject medal;
    public Sprite gold_m;
    public Sprite silver_m;
    public Sprite bronze_m;

    public void OnEnable()
    {
        //GameManager의 Score를 받아옴
        Score.text = GameManager.score.ToString();
        // 만약 GameManager의 bestScore가 현재 Score보다 낮다면
        if (GameManager.bestScore < GameManager.score)
        {
            // 점수 갱신
            GameManager.bestScore = GameManager.score;
            // newImage.SetActive(true)는 New(UI Image)를 화면에 띄울 것이라는 말
            // 점수가 갱신 되었기 때문에 New 이미지를 띄움
            newImage.SetActive(true);
        }
        else
        {
            // 점수 갱신이 되지 않은 생태라면 New 이미지를 화면에 띄우지 않음
            newImage.SetActive(false);
        }
        // BestScore 역시 GameManager의 Score를 받아옴
        BestScore.text = GameManager.bestScore.ToString();

        // 메달을 3개 만들어 점수별로 나눌 것이고,
        // 10점 이상이면 금메달!
        if (GameManager.score >= 10)
        {
            medal.GetComponent<Image>().sprite = gold_m;
        }
        // 5점 이상이면 은메달!
        else if (GameManager.score >= 5)
        {
            medal.GetComponent<Image>().sprite = silver_m;
        }
        // 나머지는 동메달
        else
        {
            medal.GetComponent<Image>().sprite = bronze_m;
        }
    }
    
    // OK버튼 누르면 점수 초기화
    public void okBtn()
    {
        GameManager.score = 0;
    }
}
