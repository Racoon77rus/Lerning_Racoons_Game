using UnityEngine;

public class PlatformPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform[] patrolPoints; // Массив точек патрулирования

    [Header("Platform")]
    [SerializeField] private Transform platform; // Платформа, которую нужно перемещать

    [Header("Movement parameters")]
    [SerializeField] private float speed; // Скорость движения платформы
    private int currentPointIndex; // Индекс текущей точки патрулирования

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration; // Время ожидания на точке
    private float idleTimer; // Таймер для отслеживания времени ожидания

    private void Start()
    {
        currentPointIndex = 0; // Начинаем с первой точки
    }

    private void Update()
    {
        // Двигаем платформу к текущей точке
        MoveTowardsCurrentPoint();
    }

    private void MoveTowardsCurrentPoint()
    {
        // Проверяем, есть ли точки для патрулирования
        if (patrolPoints.Length == 0) return;

        // Получаем текущую точку патрулирования
        Transform targetPoint = patrolPoints[currentPointIndex];

        // Двигаем платформу в сторону текущей точки
        platform.position = Vector3.MoveTowards(platform.position, targetPoint.position, speed * Time.deltaTime);

        // Проверяем, достигли ли мы текущей точки
        if (Vector3.Distance(platform.position, targetPoint.position) < 0.1f)
        {
            // Увеличиваем таймер ожидания
            idleTimer += Time.deltaTime;

            // Если прошло достаточно времени, переходим к следующей точке
            if (idleTimer > idleDuration)
            {
                // Сбрасываем таймер
                idleTimer = 0;

                // Переходим к следующей точке
                currentPointIndex++;
                if (currentPointIndex >= patrolPoints.Length)
                {
                    currentPointIndex = 0; // Возвращаемся к первой точке
                }
            }
        }
        else
        {
            // Если мы еще не достигли точки, сбрасываем таймер ожидания
            idleTimer = 0;
        }
    }
}
