using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public void ChangeFirstScene()
    {
        SceneManager.LoadScene("Chapter1"); // 챕터 1 변환
    }
    public void ChangeSecondScene()
    {
        SceneManager.LoadScene("Chapter2"); // 챕터 2 변환
    }
    public void ChangeThirdScene()
    {
        SceneManager.LoadScene("Chapter3"); // 챕터 3 변환
    }
}
