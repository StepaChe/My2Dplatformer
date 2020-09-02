using UnityEngine;

public class EnemyMeele : Enemy
{
    [SerializeField] private GameObject _strike;    // Удар врага.

    // Проверка атаки.
    protected override void Engage()
    {
        //Если враг не на перезарядке, то он стреляет.
        if (!Couldown)
        {
            IsAngry = true;
            // Теперь должна пойти перезарядка.
            Couldown = true;

            // Стреляем.
            Invoke ("Attack", 0.7f);
            _myAnim.SetTrigger("Attack");

            // Запуск таймера перезарядки.
            Invoke("Reload", _reloadTime);
        }
    }

    // Выстрел врага - сама атака.
    protected override void Attack()
    {
        // Включаем невидимый объект, наносящий урон.
        _strike.SetActive(true);

        // Вызываем кго отложенное выключение, чтобы успеть попасть по врагу.
        Invoke("Off", 0.4f);
    }

    // Метод отключения удара для Invoke.
    private void Off()
    {
        _strike.SetActive(false);
    }
}
