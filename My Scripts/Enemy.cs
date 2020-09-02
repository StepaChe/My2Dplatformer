using UnityEngine;

// Базовый класс врага.
public class Enemy : MonoBehaviour
{
    [SerializeField] protected float _speed, _atakDistance, _reloadTime;        // Скорость перемещения, дистанция, с которой атакует, время перезарядки.
    [SerializeField] protected Animator _myAnim;                                // Задаем компонент аниматора.
    [SerializeField] protected GameObject _target;                                // Цель - в нее будет помещаться игрок, когда подойдет в зону атаки.

    [SerializeField] private EnemyBullet _bullet;                               // Снаряд, которым враг стреляет.
    [SerializeField] private Transform _shootPoint;                           // Точка появления снарядов врага.
    [SerializeField] private Rigidbody2D _myBody;                           // Задаем компонент тела.
    [SerializeField] private Transform _myTrans;                          // Задаем компонент положения.

    protected bool _angry = false;                                      // Проверка нападает ли враг.

    public bool IsMove = false;                                       // Движется ли персонаж.
    public bool IsAngry = false;                                    // Показатель агрессии для аниматора
    public bool IsForward = true, Couldown = false;               // Проверка смотрит ли вперед (направо) и проверка идёт ли сейчас перезарядка.

    Vector3 _dir = new Vector3(-1, 0, 0);                       // вектор направления врага - куда он смотрит.

   // Use this for initialization
    private void Start ()
    {
        _myBody = GetComponent<Rigidbody2D>();                  // Получаем доступ к компоненту тела.
        _myTrans = GetComponent<Transform>();                   // Получаем доступ к компоненту положения.
        _myAnim = GetComponentInChildren<Animator>();           // Получаем доступ на Аниматор в Body врага.
    }
	
	// Update is called once per frame
	protected virtual void Update()
    {
        // Если игрок в поле атаки - враг нападает.
        if (_angry)
        {
            // Вывод в консоль, что игрок замечен.
            print("Детектед");

            // Сравниваем, где находится игрок x < 0 игрок слева, x > 0 справа.
            float x = _target.transform.position.x - transform.position.x;

            // Враг разворачивается в сторону игрока, для этого сравнивается положение врага относительно игрока (смотрит ли на игрока).
            if (x > 0 && IsForward)
                Flip();
            else if (x < 0 && !IsForward)
                Flip();

            // Если игрок в зоне поражение - враг атакует.
            if (Vector3.Distance(transform.position, _target.transform.position) <= _atakDistance)
            {
                IsMove = false;
                _myAnim.SetBool("Move", IsMove);
                print("Атакую!");
                // Метод атаки.
                Engage();
            }

            else if (_angry)
            {
                // Если игрок дальше зоны атаки - враг приблизится к нему.
                print("Приближаюсь!");
                IsMove = true;
                _myAnim.SetBool("Move", IsMove);
                //_myBody.velocity = (transform.right * _dir.x * _speed);
                transform.position += _dir * _speed * Time.deltaTime;
                //_speed = Mathf.Abs(_myBody.velocity.x) * _speed;
            }
            //MyAnim.SetFloat("Speed", Mathf.Abs(MyBody.velocity.x));
             //transform.position += _dir * _speed * Time.fixedDeltaTime;            
        }
        // Если враг не заметил игрока - завершаем работу кадра.
        else
        {
            IsMove = false;
            _myAnim.SetBool("Move", IsMove);
            return;
        }
    }

    // Разворот.
    private void Flip()
    {
        // Запоминаем, что мы больше не смотрим вправо.
        IsForward = !IsForward;

        // Меняем направление движения.
        _dir.x *= -1;

        // Берём новую структуру Vector3 и копируем её с нашего scale.
        Vector3 V = transform.localScale;

        // И меняем её ось x в другую сторону.
        V.x *= -1;

        // Присваиваем свою структуру, так как напрямую к localScale.x нам не обратиться.
        transform.localScale = V;
    }

    // Проверка атаки.
    protected virtual void Engage()
    {
        //Если враг не на перезарядке, то он стреляет.
        if (!Couldown)
        {
                IsAngry = true;
            // Теперь должна пойти перезарядка.
            Couldown = true;

            // Стреляем.
            Invoke("Attack", 2.11f);
            _myAnim.SetTrigger("Attack");

            // Запуск таймера перезарядки.
            Invoke("Reload", _reloadTime);
        }

    }

    // Выстрел врага - сама атака.
    protected virtual void Attack()
    {
        // Создаём пулю в точке выстрела.
        EnemyBullet Bull = Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);
        
        // Проверяем направление персонажа.
        if (!IsForward)

            // Пускаем снаряд в ту сторону.
            Bull._dir.x *= -1;

        // При этом меняем направление скорости.
        Bull._speed *= Mathf.Sign(Bull._dir.x);

    }


    // Перезарядка. 
    void Reload()
    {
        // Враг перезарядился, откат прошел - меняем значение логической переменной.
        Couldown = false;
            IsAngry = false;
    }

    // Касание триггера врага.
    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        // Проверяем тэг объекта.
        if (collision.gameObject.CompareTag("Player"))
        {
            // Делаем ссылку на игрока.
            _target = collision.gameObject;

            // Становимся агрессивными.
            _angry = true;
        }
    }

    // Потеря таргета.
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        // Проверяем тэг объекта столкновения.
        if (collision.gameObject.CompareTag("Player"))
        {           
            // Выключаем агрессию.
            _angry = false;
        }
    }
}
