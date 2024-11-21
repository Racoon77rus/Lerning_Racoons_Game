using UnityEngine;
using UnityEngine.UI;

public class Staminabar : MonoBehaviour
{
    [SerializeField] private Stamina playerStamina; // ������ �� ��������� Stamina
    [SerializeField] private Image totalStaminaBar; // ������ ����� �������
    [SerializeField] private Image currentStaminaBar; // ������� ����� �������

    private void Start()
    {
        // ��������� ������ ����� �������
        totalStaminaBar.fillAmount = 1f; // ������ ������� (100%)
        UpdateStaminaBar(); // ������� ������� ����� �������
    }

    private void Update()
    {
        UpdateStaminaBar(); // ��������� ����� ������� ������ ����
    }

    private void UpdateStaminaBar()
    {
        // ��������� ������� ����� ������� �� ������ �������� �������� �������
        currentStaminaBar.fillAmount = playerStamina.currentStamina / playerStamina.startingStamina;
    }
}
