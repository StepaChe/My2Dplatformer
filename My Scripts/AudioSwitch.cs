using UnityEngine;

public class AudioSwitch : MonoBehaviour
{
    [SerializeField] private GameObject _mainAudio;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _mainAudio.SetActive(false);
            _audioSource.Play();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
