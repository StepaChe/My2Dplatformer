using UnityEngine;

//Базовый класс для летающих снарядов.
public class Projectile : MonoBehaviour
{
    public GameObject _boom;                                     // Игровой объект взрыва.                //                    
    public float _speed, _lifeTime;                              // Скорость и время жизни снаряда.       // Поля публичные, чтобы у наследников был доступ к ним.
    public int _damage = 1;                                      // Наносимый урон.                       //
    public bool IsForward = true;                                // Определение направления движения.
    public Vector3 _dir = new Vector3(1, 0, 0);                  // Создаем экземляр вектора.

    private Rigidbody2D _myBody;                                 // Создаем переменную типа Rigitbody2D.

    void Start ()
    {
        // Проверяем и, если надо, меняем направление снаряда.
        if (_dir.x < 0) Flip();

        // Уничтожаем объект через _lifeTime время.
        Destroy(gameObject, _lifeTime);

        // Получаем ссылку на Rigitbody2D снаряда.
        _myBody = GetComponent<Rigidbody2D>();

        // Перемещает объект с помощью физики.
        _myBody.AddForce(transform.right * _speed * _dir.x * _myBody.mass, ForceMode2D.Impulse);
    }
	
    // Виртуальный метод проверки столкновения с объектом.
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, в кого врезались, по тэгу.
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Находим здоровье объекта, в который врезались и вызываем получение урона.
            collision.gameObject.GetComponent<Health>().TakeDamage(_damage);

            // Вызываем метод взрыва.
            Explode();
        }

        else Explode();     
    }

    // Метод взрыва снаряда.
    protected virtual void Explode()
    {
        // Создаем объект _boom в координатах снаряда.
         Instantiate(_boom, transform.position, transform.rotation);

        // Уничтожаем снаряд.
        Destroy(gameObject);
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
}
