using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] GameObject gravePrefab;
    bool hasCrashed;

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "Death" || other.tag == "Ground") && !hasCrashed)
        {
            hasCrashed = true;
            ScoreManager.Instance.AddScore(-20);
            CrashCounter.CrashCount++;
            FindObjectOfType<PlayerController>().DisableControls();

            // Lưu mộ tại vị trí chết
            Vector3 gravePosition = transform.position;
            gravePosition.y = other.ClosestPoint(transform.position).y; 
            Instantiate(gravePrefab, gravePosition, Quaternion.identity);

            // Lưu mộ tại màn đó
            string currentScene = SceneManager.GetActiveScene().name;
            GraveRegistry.AddGrave(currentScene, gravePosition);

            crashEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(crashSFX);
            if (GameTimerManager.Instance != null)
            {
                GameTimerManager.Instance.StopTimer();
            }
            Invoke("ReloadScene", loadDelay);
        }
    }
    void ReloadScene()
    {
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.RestartMusic();
        }

        //if (GameTimerManager.Instance != null)
        //{
        //    GameTimerManager.Instance.ResetTimer();
        //    GameTimerManager.Instance.StartTimer();
        //}
        //if (ScoreManager.Instance != null)
        //{
        //    ScoreManager.Instance.ResetScore();
        //}

        SceneManager.LoadScene(1);
    }
}
