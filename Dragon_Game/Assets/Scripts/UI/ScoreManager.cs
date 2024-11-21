using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // �������� ��� ������� �� ������ �������

    public int score { get; private set; } // ������� ���������� �����
    [SerializeField] private Text scoreText; // UI ������� ��� ����������� �����

    private void Awake()
    {
        // ������� ��������
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // ������� ��������
        }
    }

    private void Start()
    {
        UpdateScoreText(); // ��������� ����� ����� �� ������
    }

    public void AddScore(int amount)
    {
        score += amount; // ����������� ����
        UpdateScoreText(); // ��������� ����� �����
    }

    private void UpdateScoreText()
    {
        scoreText.text = "������: " + score; // ��������� ����� �� UI
    }

    public void PrintScore()
    {
        Debug.Log("Current Score: " + score);
    }
}

