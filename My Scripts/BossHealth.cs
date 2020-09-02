using UnityEngine;

public class BossHealth : Health
{
    [SerializeField] private GameObject _key;       // Ключ для выхода с уровня.
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
            Invoke("Info", 2);

            // Создаем ключ для выхода с уровня.
            GameObject Key = Instantiate(_key, transform.position, transform.rotation);

            gameObject.SetActive(false);
        }
    }

    private void Info()
    {
        GameObject Text = Instantiate(_info, transform.position, transform.rotation);
    }
}
