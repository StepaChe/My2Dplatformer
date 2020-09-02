using UnityEngine;
using UnityEngine.SceneManagement;

public class Clip_Controller : MonoBehaviour
{
    [SerializeField] private GameObject _hero;
    [SerializeField] private GameObject _princess;
    [SerializeField] private GameObject _princess1;
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _info1;
    [SerializeField] private GameObject _info2;
    [SerializeField] private bool _key;                             // Переменная для проверки ключа у героя.
    [SerializeField] private GameObject _mobile;
    [SerializeField] private GameObject _mainAudio;
    [SerializeField] private GameObject[] _background;            // Массив с элементами задника.
    [SerializeField] private GameObject[] _foreground;           // Массив с элементами передника.

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _key = collision.gameObject.GetComponent<Player_Controller>().Key;

            // Если ключ есть.
            if (_key == true)
            {   
                // Выключаем игрока.
                collision.gameObject.SetActive(false);

                // Выключаем тему уровня.
                _mainAudio.SetActive(false);

                // Запускаем аудио заставки.
                _audioSource.Play();

                // Если есть, выключаем интерфейс смартфона.
                _mobile.SetActive(false);

                // Запускаем анимацию героя.
                _hero.SetActive(true);

                // Запускаем анимацию принцессы.
                _princess1.SetActive(true);

                // Запускаем анимацию принцессы на балконе.
                _princess.GetComponent<Animator>().SetTrigger("Final");

                //_camera.SetActive(true);

                // Запускаем аниматор камеры.
                _camera.GetComponent<Animator>().enabled = true;

                // Вызываем диалоговые окна.
                Invoke("Info1", 23.5f);
                Invoke("Info2", 25);

                // Переключаемся на сцену титров.
                Invoke("LoadScene", 33.5f);

                // Отключаем объекты задника.
                foreach (GameObject b in _background) b.SetActive(false);

                // Включаем объекты передника, чтобы герой мог пройти сквозь дверь и оказаться за передником.
                foreach (GameObject f in _foreground) f.SetActive(true);
            }
        }
    }

    private void Info1()
    {
        GameObject Text = Instantiate(_info1, transform.position, transform.rotation);
    }

    private void Info2()
    {
        GameObject Text = Instantiate(_info2, transform.position, transform.rotation);
    }
    public void LoadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(4);
    }
}
