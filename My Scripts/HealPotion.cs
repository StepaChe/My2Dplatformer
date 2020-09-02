using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : MonoBehaviour
{
    [SerializeField] private int _heal;       // Сколько здоровья восстановит зелье.

    // Через триггер проверяем столкновение с игровым объектом.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, что столкнулись с игроком.
        if (collision.tag == "Player")
        {
            // Вызываем его метод лечения и прибавляем свои очки здоровья.
            collision.gameObject.GetComponent<Health>().Heal(_heal);

            // Зелье исчезает.
            Destroy(gameObject);
        }
    }
}
