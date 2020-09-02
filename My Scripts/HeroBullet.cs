using UnityEngine;

// Класс снаряда игрока.
public class HeroBullet : Projectile
{
    // Переписанный родительский метод проверки столкновения.
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            // Находим здоровье объекта, в который врезались и вызываем получение урона.
            collision.gameObject.GetComponent<Health>().TakeDamage(_damage);
            Destroy(gameObject);
        }
        //print(collision.gameObject.tag);

        // Если столкнулись не с игроком - взрыв.
        //if(!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("CharBullets"))

        //Explode();
    }
}
