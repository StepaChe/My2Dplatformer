using UnityEngine;

public class HeroHealth : Health
{
    [SerializeField] private GameObject _info;      // Окно информации.


    public override void TakeDamage(int dmg)
    {
        // Вычитаем полученный урон из здоровья.
        _health -= dmg;

        // Подкрашиваем персонажа в красный.
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;

        // Записываем в консоль оставшиеся НР.
        print("Ранили, " + _health);

        // Вызываем откат окраски.
        Invoke("Reload", 0.5f);

        // Если НР кончилось - смерть.
        if (_health <= 0)
        {
            // Активируем информационное окно.
            Invoke("Info", 1.5f);           

            gameObject.SetActive(false);
        }
    }

    private void Info()
    {
        _info.SetActive(true);
    }
}
