using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private HeroBullet _bullet;                // Снаряд, которым игрок будет стрелять.
    [SerializeField] private Transform _shootPoint;             // Точка, в которой будут появляться снаряды игрока.
    public float min, max;                                      // Переменные для ограничения угла поворота.
    public Vector3 _dir = new Vector3(1, 0, 0);                 // Переменная направления.
    public bool IsForward;                               // Логическая переменная направления.
    public bool _cd = false;

    private void Start()
    {
       // IsForward = gameObject.GetComponentInParent<Player_Controller>().IsForward;
    }
    void Update()
    {
        IsForward = gameObject.GetComponentInParent<Player_Controller>().IsForward;
        if (IsForward != true) Flip();
        
        // Считываем положение курсора.
        Vector3 _mousePos = Input.mousePosition;

        // Преобразуем transform.position из мировых координат в координаты экрана.
        Vector3 _myPos = Camera.main.WorldToScreenPoint(transform.position);

        // Высчитываем направление.
        _mousePos = _mousePos - _myPos;
       // if (_mousePos.x > _myPos.x && IsForward) gameObject.GetComponentInParent<Player_Controller>().Flip();
        // Возвращаем угол в радианах тангенса y/x (направления) и переводим их в градусы.
        float angle = Mathf.Atan2(_mousePos.y, _mousePos.x) * Mathf.Rad2Deg;

        // Ограничиваем поворот.
        //angle = Mathf.Clamp(angle, min, max);

        // Поворачиваем.
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


        if (Input.GetButtonDown("Fire1") && !_cd)
        {
            _cd = true;
            Fire();
            // Вызываем перезарядку.
            Invoke("Reload", 0.5f);
        }
        //if (gameObject.GetComponentInParent<Player_Controller>().IsForward != true) Flip();
    }

    void Fire()
    {
        // Создаем снаряд в точке выстрела.
        HeroBullet Bull = Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);

        // Проверяем направление пушки.
        if (!IsForward)

            // Пускаем снаряд в ту сторону.
            Bull._dir.x *= -1;

        // При этом меняем направление скорости.
        Bull._speed *= Mathf.Sign(Bull._dir.x);
    }

    // Разворот.
    void Flip()
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

    void Reload()
    {
        // Выстрел перезарядился, откат прошел - меняем значение логической переменной.
        _cd = false;
    }
}
