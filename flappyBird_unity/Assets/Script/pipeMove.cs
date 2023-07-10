using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipeMove : MonoBehaviour
{
    public float pipeSpeed = 4f;
    
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.playerDie)
        {
            // x축 방향으로 이동
            transform.Translate(-pipeSpeed * Time.deltaTime, 0, 0);
            // 오브젝트 Destory
            if (transform.position.x <= -6f)
            {
                Destroy(gameObject);
            }
        }
    }
}
