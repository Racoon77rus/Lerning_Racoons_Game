using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] buttons; // Массив RectTransform для кнопок
    [SerializeField] private AudioClip changeSound; // Аудиоклип для изменения позиции
    [SerializeField] private AudioClip interactSound; // Аудиоклип для взаимодействия
    private RectTransform arrow; // Преобразование RectTransform для стрелки
    private int currentPosition; // Текущая позиция

    private void Awake()
    {
        arrow = GetComponent<RectTransform>(); // Получение компонента RectTransform для стрелки
    }

    private void OnEnable()
    {
        currentPosition = 0; // Установка начальной позиции
        ChangePosition(0); // Изменение позиции на начальную
    }

    private void Update()
    {
        // Изменение позиции стрелки выбора
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            ChangePosition(-1); // Перемещение вверх
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            ChangePosition(1); // Перемещение вниз

        // Взаимодействие с текущей опцией
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
            Interact(); // Взаимодействие
    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change; // Изменение текущей позиции

        if (_change != 0)
            SoundManager.instance.PlaySound(changeSound); // Воспроизведение звука при изменении позиции

        if (currentPosition < 0)
            currentPosition = buttons.Length - 1; // Ограничение позиции снизу
        else if (currentPosition > buttons.Length - 1)
            currentPosition = 0; // Ограничение позиции сверху

        AssignPosition(); // Назначение новой позиции
    }

    private void AssignPosition()
    {
        // Назначение позиции Y текущей опции для стрелки (перемещение вверх и вниз)
        arrow.position = new Vector3(arrow.position.x, buttons[currentPosition].position.y);
    }

    private void Interact()
    {
        SoundManager.instance.PlaySound(interactSound); // Воспроизведение звука при взаимодействии

        // Доступ к компоненту Button на каждой опции и вызов его функции
        buttons[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}