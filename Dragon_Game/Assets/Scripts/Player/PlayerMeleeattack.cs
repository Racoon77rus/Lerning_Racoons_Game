using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeattack : MonoBehaviour
{
    [SerializeField] protected float damage;

    private void Awake()
    {
        // Деактивируем объект при инициализации
        gameObject.SetActive(false);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

    // Метод для активации атаки
    public void ActivateAttack()
    {
        gameObject.SetActive(true); // Активируем объект
        Invoke("DeactivateAttack", 0.25f); // Вызываем метод деактивации через 0.25 секунды
    }

    // Метод для деактивации атаки (если необходимо)
    public void DeactivateAttack()
    {
        gameObject.SetActive(false); // Деактивируем объект
    }

    // Обработчик нажатия клавиши B
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ActivateAttack();
        }
    }
}