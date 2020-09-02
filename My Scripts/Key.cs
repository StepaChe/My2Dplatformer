using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{   
    [SerializeField] private GameObject _target;        // Переменная для передачи ключа герою.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {   
            // Берём героя в таргет.
            _target = collision.gameObject;

            // Получаем ссылку на логическую переменную героя и отмечаем, что ключ у него есть.
            _target.GetComponent<Player_Controller>().Key = true;
            Destroy(gameObject);
        }
    }
}
