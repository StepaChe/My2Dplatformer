using UnityEngine;

public class EnemyBullet : Projectile
{
    // Переписанный родительский метод проверки столкновения.
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, чтобы объект столкновения был игроком.
        if (collision.gameObject.CompareTag("Player"))
        {
            // И наносим игроку урон.
            collision.gameObject.GetComponent<Health>().TakeDamage(_damage);
            Destroy(gameObject);
        }        
    }     
}

