using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] protected Animator _myAnim;                   // Задаем компонент аниматора.

    [SerializeField] private GameObject[] _background;            // Массив с элементами задника.
    [SerializeField] private GameObject[] _foreground;           // Массив с элементами передника.


    private bool _closed = true;

    private void Start()
    {
        _myAnim = GetComponentInChildren<Animator>();                  // Получаем доступ к Аниматору.
        _myAnim.SetBool("Closed", _closed);
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Отключаем объекты задника.
            foreach (GameObject b in _background) b.SetActive(true);

            // Включаем объекты передника, чтобы герой мог пройти сквозь дверь и оказаться за передником.
            foreach (GameObject f in _foreground) f.SetActive(false);

            // Закрываем дверь.
            Invoke("Close", 2);
        }
    }

    protected void Close()
    {
        _myAnim.SetBool("Closed", _closed);
    }
}
