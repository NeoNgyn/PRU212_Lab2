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
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 3)
        {
            SceneManager.LoadScene("WinnerSence");
        }
        else
        {
            int nextSceneIndex = currentSceneIndex + 1;
            if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0; 
            }
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}