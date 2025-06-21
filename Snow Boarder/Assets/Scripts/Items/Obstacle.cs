using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    public int scoreValue = -5;
    [SerializeField] private AudioClip collectSound;
    private AudioSource audioSource;

    bool hasHit = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasHit) return;

        if (collision.collider.CompareTag("Player"))
        {
            hasHit = true;
            ScoreManager.Instance.AddScore(scoreValue);
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }
    }
}
