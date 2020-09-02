using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

// Базовый контроллер игрока.
public class Player_Controller : MonoBehaviour
{
    [SerializeField] private float _speed;                     // Скорость перемещения.
    [SerializeField] private float _jumpForce;                 // Сила прыжка.
    [SerializeField] private HeroBullet _bullet;               // Снаряд, которым игрок будет стрелять.
    [SerializeField] private Transform _shootPoint;            // Точка, в которой будут появляться снаряды игрока.
    [SerializeField] private GameObject _strike;               // Невидимый объект с коллайдером, для нанесения урона в ближнем бою.
        
    private Rigidbody2D _myRg;                                 // Создаем переменную типa Rigitbody2D для контроля прыжка.
    private Animator _myAnim;                                  // Создаем переменную типа Animator для контроля анимаций.
    private bool _isGrounded;                                  // Переменная для проверки, что игрок на земле.

    public Vector3 _dir;                                       // Создаем экземляр вектора для направления движения.
    public bool IsForward = true;                              // Переменная для проверки, куда смотрит игрок.
    public bool Key = false;                                   // Есть ли у персонажа ключь.
    public bool IsMove = false;                                // Движется ли персонаж.
    public bool Couldown1 = false;                              // Откат стрельбы.
    public bool Couldown2 = false;                              // Откат удара.

    private void Start()
    {
        _myRg = GetComponent<Rigidbody2D>();                   // Получаем доступ к компоненту Rigitbody2D игрока.
        _myAnim = GetComponentInChildren<Animator>();                    // Получаем доступ к аниматору.

    }

    private void Update()
    {
        // Меняем направление, если нажимается кнопка контроллера горизонтали.
        _dir.x = Input.GetAxis("Horizontal");
        //_dir.x = CrossPlatformInputManager.GetAxis("Horizontal");

        //_myAnim.SetBool("Move", IsMove);

        //_myAnim.SetFloat("Jump", Mathf.Abs(_myRg.velocity.y));

        if (_dir.x < 0 && IsForward)
            Flip();
        else if (_dir.x > 0 && !IsForward)
            Flip();

        if (_dir.x != 0)
        {
            // Придаем игроку ускорение.
            //_myRg.AddForce(Vector3.right * _speed * _dir.x, ForceMode2D.Impulse);
            transform.position += _dir * _speed * Time.deltaTime;
            IsMove = true;
        }
        else
        {
            IsMove = false;
        }

        // Проверяем модуль скорости по х.
        if (Mathf.Abs(_myRg.velocity.x) > _speed)
        {
            // Запоминаем _myRg.velocity, - для этого создаем переменную "velocity" т.к. нельзя изменить velocity напрямую.
            Vector2 velocity = _myRg.velocity;

            // Домножаем на знак текущей скорости - то есть созданная velocity хранит вектор движения по х с максимальной скоростью.
            velocity.x = _speed * Mathf.Sign(_myRg.velocity.x);

            // Сохраняем изменения - наша новая скорость не превышает максимальную скорость.
            _myRg.velocity = velocity;
        }

        ////Дальняя атака.
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    Fire1();
        //}

        //Ближняя атака.
        if (Input.GetButtonDown("Fire2") && !Couldown2)
        {
            Couldown2 = true;
            Invoke("Fire2", 0.3f);
            // Вызываем перезарядку.
            Invoke("Reload2", 0.9f);
        }

        //Прыжок.
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    //public void Sword()
    //{
    //    _myAnim.SetTrigger("Attack2");

    //    Invoke("Fire2", 0.3f);
    //}

    // Метод дальней атаки.
    public void Fire1()
    {
        //_myAnim.SetTrigger("Attack1");

        // Создаем снаряд в точке выстрела.
        HeroBullet Bull = Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);

        // Проверяем направление персонажа.
        if (!IsForward)

            // Пускаем снаряд в ту сторону.
            //Bull._dir.x *= -1;

        // При этом меняем направление скорости.
        Bull._speed *= Mathf.Sign(Bull._dir.x);
    }

    // Метод ближней атаки.
    public void Fire2()
    {        
            // Утверждаем откат.
            //Couldown2 = true;
            
            // Включаем невидимый объект, наносящий урон.
            _strike.SetActive(true);

            // Вызываем кго отложенное выключение, чтобы успеть попасть по врагу.
            Invoke("Off", 0.4f);        
    }

    // Метод отключения удара для Invoke.
    private void Off()
    {
        _strike.SetActive(false);
    }

    // Два метода проверки пола.

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            _isGrounded = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            _isGrounded = false;
    }

    // Метод прыжка.
    public void Jump()
    {
        if (_isGrounded == true)
            _myRg.AddForce(Vector3.up * _jumpForce * _myRg.mass, ForceMode2D.Impulse);
    }

    // Разворот.
    public void Flip()
    {
        // Запоминаем, что мы больше не смотрим вправо.
        IsForward = !IsForward;

        // Берём новую структуру Vector3 и запоминаем в неё rotation объекта.
        Vector3 V = transform.rotation.eulerAngles;

        // Поворачиваем объект вокруг оси Y.
        V.y += 180;

        // Присваиваем свою структуру, так как напрямую к localScale.x нам не обратиться.
        gameObject.transform.rotation = Quaternion.Euler(V);
    }

    void Reload1()
    {
        // Выстрел перезарядился, откат прошел - меняем значение логической переменной.
        Couldown1 = false;
    }

    void Reload2()
    {
        // Удар перезарядился, откат прошел - меняем значение логической переменной.
        Couldown2 = false;
    }
}

