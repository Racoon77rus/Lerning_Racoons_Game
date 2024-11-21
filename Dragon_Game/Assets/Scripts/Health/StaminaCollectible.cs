using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaCollectible : MonoBehaviour
{
    [SerializeField] private float staminaValue; // Значение стамины, которое будет добавлено
    [Header("Stamina Sound")]
    [SerializeField] private AudioClip pickupSound; // Звук при сборе

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Проверяем, что столкновение произошло с игроком
        {
            SoundManager.instance.PlaySound(pickupSound); // Проигрываем звук
            collision.GetComponent<Stamina>().RestoreStamina(staminaValue); // Восстанавливаем стамину игрока
            Destroy(gameObject); // Удаляем объект зелья
        }
    }
}
