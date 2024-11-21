using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Синглтон для доступа из других классов

    public int score { get; private set; } // Текущее количество очков
    [SerializeField] private Text scoreText; // UI элемент для отображения очков

    private void Awake()
    {
        // Создаем синглтон
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Удаляем дубликат
        }
    }

    private void Start()
    {
        UpdateScoreText(); // Обновляем текст очков на старте
    }

    public void AddScore(int amount)
    {
        score += amount; // Увеличиваем счет
        UpdateScoreText(); // Обновляем текст очков
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Золото: " + score; // Обновляем текст на UI
    }

    public void PrintScore()
    {
        Debug.Log("Current Score: " + score);
    }
}

