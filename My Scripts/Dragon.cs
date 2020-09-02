using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy
{
    protected override void Update()
    {
        // Если игрок в поле атаки - враг нападает.
        if (_angry)
        {
            // Вывод в консоль, что игрок замечен.
            print("Детектед");

            // Сравниваем, где находится игрок x < 0 игрок слева, x > 0 справа.
            float x = _target.transform.position.x - transform.position.x;

            // Если игрок в зоне поражение - враг атакует.
            if (Vector3.Distance(transform.position, _target.transform.position) <= _atakDistance)
            {
                IsMove = false;
                _myAnim.SetBool("Move", IsMove);
                print("Атакую!");
                // Метод атаки.
                Engage();
            }          
        }
    }
}
