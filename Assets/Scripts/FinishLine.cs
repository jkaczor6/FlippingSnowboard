using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] float delayBeforeReload = 1.5f;
    [SerializeField] ParticleSystem finishParticles;
    ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Player");
        if(collision.gameObject.layer == layerIndex)
        {
            finishParticles.Play();
            Invoke("ReloadScene", delayBeforeReload);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
