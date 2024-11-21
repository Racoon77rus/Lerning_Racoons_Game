using UnityEngine;
using UnityEngine.UI;

public class Staminabar : MonoBehaviour
{
    [SerializeField] private Stamina playerStamina; // Ссылка на компонент Stamina
    [SerializeField] private Image totalStaminaBar; // Полная шкала стамины
    [SerializeField] private Image currentStaminaBar; // Текущая шкала стамины

    private void Start()
    {
        // Установим полную шкалу стамины
        totalStaminaBar.fillAmount = 1f; // Полная стамина (100%)
        UpdateStaminaBar(); // Обновим текущую шкалу стамины
    }

    private void Update()
    {
        UpdateStaminaBar(); // Обновляем шкалу стамины каждый кадр
    }

    private void UpdateStaminaBar()
    {
        // Обновляем текущую шкалу стамины на основе текущего значения стамины
        currentStaminaBar.fillAmount = playerStamina.currentStamina / playerStamina.startingStamina;
    }
}
