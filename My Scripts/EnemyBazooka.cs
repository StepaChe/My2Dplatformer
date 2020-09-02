using UnityEngine;

public class EnemyBazooka : MonoBehaviour
{
    [SerializeField] protected float _atakDistance, _reloadTime;        // Дистанция, с которой атакует, время перезарядки.
    [SerializeField] protected GameObject _target;                     // Цель - в нее будет помещаться игрок, когда подойдет в зону атаки.
    [SerializeField] private DragonBullet _bullet;                     // Снаряд, которым враг будет стрелять.
    [SerializeField] private Transform _shootPoint;                  // Точка, в которой будут появляться снаряды врага.

    protected bool _angry = false;                                 // Проверка нападает ли враг.

    public bool IsAngry = false;                                    // Показатель агрессии для аниматора
    public bool Couldown = false;                               // Проверка смотрит ли вперед (направо) и проверка идёт ли сейчас перезарядка.
    public float min, max;                                      // Переменные для ограничения угла поворота.
    public Vector3 _dir = new Vector3(1, 0, 0);                 // Переменная направления.

    void Update()
    {
        // Считываем положение курсора.
        Vector3 _targetPos = _target.transform.position;

        // Преобразуем transform.position из мировых координат в координаты экрана.
        Vector3 _myPos = gameObject.transform.position;

        // Высчитываем направление.
        _targetPos = _targetPos - _myPos;
       
        // Возвращаем угол в радианах тангенса y/x (направления) и переводим их в градусы.
        float angle = Mathf.Atan2(_targetPos.y, _targetPos.x) * Mathf.Rad2Deg;

        // Ограничиваем поворот.
        //angle = Mathf.Clamp(angle, min, max);

        // Поворачиваем.
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


        // Если игрок в поле атаки - враг нападает.
        if (_angry)
        {
            // Вывод в консоль, что игрок замечен.
            print("Детектед");

            // Сравниваем, где находится игрок x < 0 игрок слева, x > 0 справа.
            float x = _target.transform.position.x - transform.position.x;

            // Если игрок в зоне поражение - враг атакует.
            if (Vector3.Distance(transform.position, _target.transform.position) <= _atakDistance)
            {
                print("Атакую!");
                // Метод атаки.
                Engage();
            }
        }              
    }

    protected virtual void Engage()
    {
        //Если враг не на перезарядке, то он стреляет.
        if (!Couldown)
        {
            IsAngry = true;
            // Теперь должна пойти перезарядка.
            Couldown = true;

            // Стреляем.
            Invoke("Attack", 1f);
            
            // Запуск таймера перезарядки.
            Invoke("Reload", _reloadTime);
        }

    }

    // Выстрел врага - сама атака.
    protected virtual void Attack()
    {
        // Создаём пулю в точке выстрела.
        DragonBullet Bull = Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);
        
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
