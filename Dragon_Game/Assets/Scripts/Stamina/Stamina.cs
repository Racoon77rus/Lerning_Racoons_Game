using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [Header("Stamina Parameters")]
    [SerializeField] public float startingStamina; // ��������� �������� �������
    [SerializeField] public float attackStaminaCost; // ��������� ������� �� �����
    [SerializeField] public float jumpStaminaCost; // ��������� ������� �� ������� ������
    public float currentStamina { get; private set; } // ������� �������� �������

    private void Awake()
    {
        // �������������� ������� �������
        currentStamina = startingStamina;
        Debug.Log($"Starting Stamina: {currentStamina}"); // ����� ��������� �������
    }

    private void Update()
    {
        // �������� �� ������������� ������� ��� �����
        if (Input.GetKeyDown(KeyCode.V) && currentStamina >= attackStaminaCost)
        {
            UseStamina(attackStaminaCost);
        }

        // �������� �� ������������� ������� ��� ������� ������
        if (Input.GetKeyDown(KeyCode.Space) && currentStamina >= jumpStaminaCost)
        {
            UseStamina(jumpStaminaCost);
        }
    }

    // ����� ��� ������������� �������
    public void UseStamina(float amount)
    {
        currentStamina = Mathf.Clamp(currentStamina - amount, 0, startingStamina);
        Debug.Log($"Stamina used: {amount}. Current Stamina: {currentStamina}"); // ����� ������� ������� ����� �������������
    }

    // ����� ��� �������������� �������
    public void RestoreStamina(float amount)
    {
        currentStamina = Mathf.Clamp(currentStamina + amount, 0, startingStamina);
        Debug.Log($"Stamina restored: {amount}. Current Stamina: {currentStamina}"); // ����� ������� ������� ����� ��������������
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("StaminaPotion"))
        {
            RestoreStamina(0f); // �������������� 20 �������, ��������
            Destroy(other.gameObject); // ������� ����� ����� ��������������
        }
    }
}