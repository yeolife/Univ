using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyButton : MonoBehaviour
{
    public float birdJump = 8f;
    public int j = 0;                    // DieSound 1번 출력을 위한 변수
    public GameObject EndPannel;
    public GameObject ScoreText;
    public GameObject DangerText;

    // 게임 play 버튼 누를 시 Update 함수가 대략 1초에 60번 실행함
    private void Update()
    {
        if (!GameManager.playerDie)
        {
            // GetMouseButton으로 하면 클릭하고 있으면 계속 적용
            // 0은 0번 마우스(우클릭)
            if (Input.GetMouseButtonDown(0))
            {
                // 오브젝트에 붙인 컴포넌트 기능 불러옴
                // new Vector3 방향으로 힘을 가함(y축으로 10f만큼 힘을 줌)
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, birdJump, 0);
                // 새의 로테이션에 z값을 30f만큼 올려(움직인 것)
                transform.rotation = Quaternion.Euler(0, 0, 30f);
            }
            // 계속 -1씩 돌림
            transform.Rotate(0, 0, -0.3f);
        }
    }

    // Oncollision은 충돌 발생시 Enter / Exit / Stay 조건에 의해 어떤 함수를 동작시킴
    // Collision은 물리적인 충돌을 하고 감지함
    // Enter2D이므로 부딪히자마자 동작
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.CompareTo("Pipe_Ground") == 0)
        {
            GameManager.playerDie = true; // 플레이어 멈춤
            EndPannel.SetActive(true);    // End패널 활성화
            dieSound(1);                  // die사운드 출력
            ScoreText.SetActive(false);   // 게임 종료시 실시간 점수 가림
            DangerText.SetActive(false);  // 게임 종료시 위험 텍스트 가림
        }
    }
    private void dieSound(int i)
    {
        j += i;
        if (j == 1)
        {
            AudioSource DieSound = GetComponent<AudioSource>();
            DieSound.Play();
        }
    }
}
