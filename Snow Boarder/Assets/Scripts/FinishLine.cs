//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class FinishLine : MonoBehaviour
//{
//    [SerializeField] float loadDelay = 1f;
//    [SerializeField] ParticleSystem finishEffect;

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.tag == "Player")
//        {
//            finishEffect.Play();
//            GetComponent<AudioSource>().Play();
//            Invoke("ReloadScene", loadDelay);     
//        }
//    }
//    void ReloadScene()
//    {
//        SceneManager.LoadScene(0);
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem finishEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            finishEffect.Play();
            GetComponent<AudioSource>().Play();
            // G?i h�m m?i v?i t�n r� ngh?a h?n
            Invoke("LoadNextLevel", loadDelay);
        }
    }

    // ?�y l� h�m ???c n�ng c?p
    void LoadNextLevel()
    {
        // L?y ch? s? (index) c?a scene hi?n t?i m� ng??i ch?i ?ang ?
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // T�nh to�n ch? s? c?a scene ti?p theo b?ng c�ch c?ng th�m 1
        int nextSceneIndex = currentSceneIndex + 1;

        // KI?M TRA QUAN TR?NG: N?u scene ti?p theo kh�ng t?n t?i (?� l� m�n cu?i)
        // th� quay tr? v? m�n ch?i ??u ti�n (index = 0)
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        // T?i scene v?i ch? s? ?� ???c t�nh to�n
        SceneManager.LoadScene(nextSceneIndex);
    }
}