using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaCollectible : MonoBehaviour
{
    [SerializeField] private float staminaValue; // �������� �������, ������� ����� ���������
    [Header("Stamina Sound")]
    [SerializeField] private AudioClip pickupSound; // ���� ��� �����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // ���������, ��� ������������ ��������� � �������
        {
            SoundManager.instance.PlaySound(pickupSound); // ����������� ����
            collision.GetComponent<Stamina>().RestoreStamina(staminaValue); // ��������������� ������� ������
            Destroy(gameObject); // ������� ������ �����
        }
    }
}
