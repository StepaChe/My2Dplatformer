using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrike : Strike
{
    // Метод проверки столкновения.
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Находим здоровье объекта, в который врезались и вызываем получение урона.
            collision.gameObject.GetComponent<Health>().TakeDamage(_damage);
        }
        print(collision.gameObject.tag);
        gameObject.SetActive(false);
    }
}
