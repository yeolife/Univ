using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipeMoveY : MonoBehaviour // 챕터 3 움직이는 파이프 구현
{
    public float pipeSpeed = 3.2f;     // x축으로 가는 속도
    public float MoveSpeed = 2.0f;     // y축에서 반복 움직이는 속도
    public int jud = 1;                  // 1,-1로 움직임 판단 변수
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.playerDie)
        {
            
                if (transform.localPosition.y < 1f)          // y축 위로 1을 넘을 시
                { 
                    jud = -1;                                  // 아래로 가기 위해 a를 음수로 설정함
                }
                else if (transform.localPosition.y > 3.5f)   // y축 아래로 -3.5를 넘을 시
                {
                    jud = 1;                                   // 위로 가기 위해 a를 양수로 설정함
                }
        // x축 방향으로 이동속도, y축 방향으로 이동속도
        transform.Translate(-pipeSpeed * Time.deltaTime, -MoveSpeed * Time.deltaTime * jud, 0);
            // 오브젝트 Destory
            if (transform.position.x <= -6f) // x축의 화면 밖으로 넘어갈 시 
            {
                Destroy(gameObject);         // 파이프 삭제
            }
        }
    }
}
