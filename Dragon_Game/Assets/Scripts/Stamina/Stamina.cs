using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [Header("Stamina Parameters")]
    [SerializeField] public float startingStamina; // Начальное значение стамины
    [SerializeField] public float attackStaminaCost; // Стоимость стамины за атаку
    [SerializeField] public float jumpStaminaCost; // Стоимость стамины за двойной прыжок
    public float currentStamina { get; private set; } // Текущее значение стамины

    private void Awake()
    {
        // Инициализируем текущую стамину
        currentStamina = startingStamina;
        Debug.Log($"Starting Stamina: {currentStamina}"); // Вывод начальной стамины
    }

    private void Update()
    {
        // Проверка на использование стамины при атаке
        if (Input.GetKeyDown(KeyCode.V) && currentStamina >= attackStaminaCost)
        {
            UseStamina(attackStaminaCost);
        }

        // Проверка на использование стамины при двойном прыжке
        if (Input.GetKeyDown(KeyCode.Space) && currentStamina >= jumpStaminaCost)
        {
            UseStamina(jumpStaminaCost);
        }
    }

    // Метод для использования стамины
    public void UseStamina(float amount)
    {
        currentStamina = Mathf.Clamp(currentStamina - amount, 0, startingStamina);
        Debug.Log($"Stamina used: {amount}. Current Stamina: {currentStamina}"); // Вывод остатка стамины после использования
    }

    // Метод для восстановления стамины
    public void RestoreStamina(float amount)
    {
        currentStamina = Mathf.Clamp(currentStamina + amount, 0, startingStamina);
        Debug.Log($"Stamina restored: {amount}. Current Stamina: {currentStamina}"); // Вывод остатка стамины после восстановления
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("StaminaPotion"))
        {
            RestoreStamina(0f); // Восстановление 20 стамины, например
            Destroy(other.gameObject); // Удаляем зелье после взаимодействия
        }
    }
}