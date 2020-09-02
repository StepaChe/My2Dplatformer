using UnityEngine;

// Базовый класс для ловушек.
public class Trap : MonoBehaviour
{

    public int _damage = 1;    // Урон, который наносит ловушка.
   
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Находит здоровье игрока и вызывает его метод получения урона.
            collision.gameObject.GetComponent<Health>().TakeDamage(_damage);
        }

        //print(collision.gameObject.tag);
    }
}
