using UnityEngine;

// Cтандартный класс для ближней атаки.
public class Strike : MonoBehaviour
{
    public int _damage = 1;      // Урон персонажа.    

    // Метод проверки столкновения.
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Находим здоровье объекта, в который врезались и вызываем получение урона.
            collision.gameObject.GetComponent<Health>().TakeDamage(_damage);
        }        
        gameObject.SetActive(false);
    }   
}
