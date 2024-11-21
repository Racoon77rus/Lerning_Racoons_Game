using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    [SerializeField] private int scoreValue = 1; // Количество очков, которое дает объект
    [Header("Score Sound")]
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Проверяем, что взаимодействует игрок
        {
            SoundManager.instance.PlaySound(pickupSound);
            ScoreManager.Instance.AddScore(scoreValue); // Увеличиваем счет
            Destroy(gameObject); // Удаляем объект после взаимодействия
        }
    }
}
