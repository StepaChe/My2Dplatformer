using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] protected Animator _myAnim;                   // Задаем компонент аниматора.

    [SerializeField] private bool _key;                             // Переменная для проверки ключа у героя.
    [SerializeField] private GameObject[] _background;            // Массив с элементами задника.
    [SerializeField] private GameObject[] _foreground;           // Массив с элементами передника.

    private bool _closed = true;

    private void Start()
    {
        _myAnim = GetComponentInChildren<Animator>();                  // Получаем доступ к Аниматору.
        _myAnim.SetBool("Closed", _closed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Получаем ссылку на ключ героя.
            _key = collision.gameObject.GetComponent<Player_Controller>().Key;

            // Если ключ есть - открываем дверь.
            if (_key == true)
            {
                _closed = false;

                // Отключаем объекты задника.
                foreach (GameObject b in _background) b.SetActive(false);

                // Включаем объекты передника, чтобы герой мог пройти сквозь дверь и оказаться за передником.
                foreach (GameObject f in _foreground) f.SetActive(true);

                // Открываем дверь.
                _myAnim.SetBool("Closed", _closed);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Получаем ссылку на ключ героя.
            _key = collision.gameObject.GetComponent<Player_Controller>().Key;

            // Отключаем объекты задника.
            foreach (GameObject b in _background) b.SetActive(true);

            // Включаем объекты передника, чтобы герой мог пройти сквозь дверь и оказаться за передником.
            foreach (GameObject f in _foreground) f.SetActive(false);

            // Если ключ есть - дверь открыта.
            if (_key == true)
            {
                _closed = true;
                // Закрываем дверь.
                Invoke("Close", 1); 
            }
        }
    }

    protected void Close()
    {
        _myAnim.SetBool("Closed", _closed);
    }
}
