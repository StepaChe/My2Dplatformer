using UnityEngine;

// Класс снаряда для потолочной ловушки.
public class UpTrap : MonoBehaviour
{
    public float _lifeTime;                // Время существования снаряда.
    public int _damage = 1;                // Урон.

    private void Start()
    {
        // Этот снаряд исчезнет, когда время существования истечет.
        Destroy(gameObject, _lifeTime);        
    }

    // Переписанный метод ловушки.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Наносит урон как любой снаряд или ловушка.
            collision.gameObject.GetComponent<Health>().TakeDamage(_damage);
        }
    }
}
