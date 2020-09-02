using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{

    [SerializeField] protected float _health;       // Здоровье персонажа.
    [SerializeField] protected float _healthMax;       // Порог Здоровья персонажа.
    [SerializeField] protected Image _hpBar;           // Получаем ссылку на бордер в игровом интерфейсе.
    
    // Каждый кадр отображаем полоску здоровья
    private void Update()
    {
        _hpBar.fillAmount = _health / _healthMax;
    }

    // Метод получения урона.
    public virtual void TakeDamage(int dmg)
    {
        // Вычитаем полученный урон из здоровья.
        _health -= dmg;

        // Подкрашиваем персонажа в красный.
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;

        // Записываем в консоль оставшиеся НР.
        print("Ранили, " +_health);

        // Вызываем откат окраски.
        Invoke("Reload", 0.5f);

        // Если НР кончилось - смерть.
        if (_health <= 0)
            Destroy(gameObject);
    }

    // Метод лечения.
    public void Heal(int hp)
    {
        if (_health < _healthMax)
        {
            // Прибавляем полученное лечение.
            _health += hp;

            // Проверяем превышение Порога здоровья.
            if (_health > _healthMax) _health = _healthMax;

            // Подкрашиваем персонажа в зеленый.
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;

            // Записываем получившееся НР в консоль.
            print("Полечили, " + _health);
            
            // Вызываем откат окраски.
            Invoke("Reload", 0.5f);
        }
        
    }

    // Метод для возвращения исходного цвета персонажа.
    private void Reload()
    {
        //Находим SpriteRenderer и задаем цвет.
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
}
