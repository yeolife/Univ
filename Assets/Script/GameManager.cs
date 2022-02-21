using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool check = true;
    static public bool playerDie = true; // bird의 상태
    public float pipeTime = 1.5f;        // pipe 생성 텀
    public float addTime = 0.7f;         // pipe 생성 추가 텀(챕터3 전용)
    public float pipeMin = -1f;          // pipe의 위 아래 위치 
    public float pipeMax = 1f;
    static public int score = 0;         // 점수
    static public int bestScore = 0;     // 최고점수
    public Text ScoreText;
    public GameObject player;            // 플레이어(bird)
    public GameObject pipePrefab;        // 파이프 클론
    public GameObject HpipePrefab;       // 파이프 클론2(챕터3 전용)
    public GameObject startPanel;        // 시작패널
    public GameObject readyPanel;        // 준비패널
    public GameObject NextPanel;         // 다음 스테이지 패널
    public GameObject scoreText;         // 화면의 실시간 점수 텍스트
    public GameObject dangerText;        // 화면의 위험 텍스트
    public AudioSource endSo;            // 종료 사운드

    // 시작버튼 사운드
    public void startSound()
    {
        AudioSource StartSound = GetComponent<AudioSource>();
        StartSound.Play();
    }
    // 종료버튼 사운드
    public void endSound()
    {
        AudioSource EndSound = GetComponent<AudioSource>();
        EndSound.Play();
    }

    // 시작패널 Start버튼 
    public void startBtn()
    {
        check = false;
        startPanel.SetActive(false);     // 시작패널 비활성화
        // 중력을 작동 시킴;
        player.SetActive(true);          // 플레이어 활성화
        readyPanel.SetActive(true);      // 준비패널 시작
    }
    // 준비패널 화면클릭
    public void click()
    {
        scoreText.SetActive(true);                            // 점수판 활성화
        readyPanel.SetActive(false);                          // 준비패널 비활성화
        GameManager.playerDie = false;                        // 플레이어 움직임
        if (SceneManager.GetActiveScene().name != "Chapter3")     // 현재 챕터 확인
        {
            StartCoroutine(PipeStart());                          // 파이프 코루틴으로 반복생성
        }
        else
        {
            StartCoroutine(PipeStart2());                         // 챕터3일시 함수2 코루틴
        }
        player.GetComponent<Rigidbody2D>().simulated = true;     // 리지드바디의 simulated 체크
    }

    IEnumerator PipeStart()                                           // 챕터1,2 파이프 생성
    {
        yield return new WaitForSeconds(1.0f);                        // 초반 준비 딜레이
        check = true;
        do
        {
            Instantiate(pipePrefab,                                   // 생성객체
                new Vector3(3.5f, Random.Range(pipeMin, pipeMax), 0), // 생성위치
                Quaternion.Euler(new Vector3(0, 0, 0)));              // 회전 값
            // Wait State
            yield return new WaitForSeconds(pipeTime);
        } while (!GameManager.playerDie);
    }
    IEnumerator PipeStart2()                                             // 챕터3 전용 파이프생성
    {
        yield return new WaitForSeconds(1.0f);                           // 초반 준비 딜레이
        check = true;
        do
        {
            for (int i = 0; i < Random.Range(1,4); i++)                  // 최소 1번 또는 4번 실행
            {
                Instantiate(HpipePrefab, // 생성객체
                    new Vector3(3.5f, Random.Range(pipeMin, pipeMax), 0), // 생성위치
                    Quaternion.Euler(new Vector3(0, 0, 0)));              // 회전 값(default)
                // Wait State
                yield return new WaitForSeconds(pipeTime);                // 다음 파이프 생성 텀
            }
            dangerText.SetActive(true);                                   // 움직이는 파이프 경고메시지 활성화
            yield return new WaitForSeconds(addTime);
            dangerText.SetActive(false);                                  // 움직이는 파이프 경고메시지 비활성화
            Instantiate(pipePrefab, // 생성객체
                    new Vector3(6.5f, Random.Range(5, 3), 0),             // 생성위치
                    Quaternion.Euler(new Vector3(0, 0, 0)));              // 회전 값
            // Wait State
            yield return new WaitForSeconds(pipeTime+addTime);
        } while (!GameManager.playerDie);
    }

    private void Update()
    {
        ScoreText.text = score.ToString(); 
        if(score>=5 && SceneManager.GetActiveScene().name != "Chapter3") // 챕터1,2 점수 10점 달성시 and 씬이 챕터1,2일 시 
        {
            player.SetActive(false);    // 플레이어 숨김
            scoreText.SetActive(false); // 실시간 점수 숨김
            playerDie = true;           // 플레이어 멈춤
            NextPanel.SetActive(true);  // Next패널 활성화
            Time.timeScale = 0;         // 시간 멈춤
        }
    }
    // Next패널 Next버튼
    public void NextBtn()
    {
        NextPanel.SetActive(false); // Next패널 숨김
        GameManager.score = 0;      // 점수 초기화
        player.SetActive(true);     // 플레이어 재시작
        Time.timeScale = 1;         // 시간 재시작
    }
}
