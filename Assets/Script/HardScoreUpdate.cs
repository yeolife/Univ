using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardScoreUpdate : MonoBehaviour
{
    // Enter2D은 진입하자마자 해당됨
    // Stay2D는 충돌하고 있는 시간동안 계속(그 시간을 측정)

    // Trigger는 물리적인 충돌 없이 판단만 함 
    // Exit2D이므로 충돌이 끝날때 판단
    // 충돌 감지를 위해 둘 중 한쪽인 ScoreZone에 isTrigger체크함
    private void OnTriggerExit2D(Collider2D collision) 
    {
        // Player tag와의 충돌 판단
        if (collision.gameObject.tag.CompareTo("Player") == 0)
        {
            AudioSource CoinSound = GetComponent<AudioSource>();
            CoinSound.Play();
            GameManager.score += 2;                           
        }
    }
}
