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
            // G?i hàm m?i v?i tên rõ ngh?a h?n
            Invoke("LoadNextLevel", loadDelay);
        }
    }

    // ?ây là hàm ???c nâng c?p
    void LoadNextLevel()
    {
        // L?y ch? s? (index) c?a scene hi?n t?i mà ng??i ch?i ?ang ?
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Tính toán ch? s? c?a scene ti?p theo b?ng cách c?ng thêm 1
        int nextSceneIndex = currentSceneIndex + 1;

        // KI?M TRA QUAN TR?NG: N?u scene ti?p theo không t?n t?i (?ã là màn cu?i)
        // thì quay tr? v? màn ch?i ??u tiên (index = 0)
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        // T?i scene v?i ch? s? ?ã ???c tính toán
        SceneManager.LoadScene(nextSceneIndex);
    }
}