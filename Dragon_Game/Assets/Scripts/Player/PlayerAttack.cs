using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Переменные, которые можно настроить через инспектор
    [SerializeField] private float attackCooldown; // Время, необходимое для восстановления атаки
    [SerializeField] private Transform firePoint; // Точка, откуда будут вылетать снаряды
    [SerializeField] private GameObject[] fireballs; // Массив снарядов, которые игрок может использовать
    [SerializeField] private AudioClip fireballSound; // Звук атаки с помощью снаряда
    [SerializeField] private AudioClip meleeAttacklSound; // Звук ближней атаки

    [SerializeField] private float meleeAttackRange; // Дальность атаки ближнего боя
    [SerializeField] private float meleeDamage; // Урон от ближнего боя

    // Новое поле для точки атаки, заданной в инспекторе
    [SerializeField] private Transform attackPoint;

    private Animator anim; // Компонент Animator для анимации игрока
    private PlayerMovement playerMovement; // Ссылка на компонент PlayerMovement
    private float cooldownTimer = Mathf.Infinity; // Таймер для отслеживания времени перезарядки
    private Stamina stamina; // Ссылка на компонент Stamina

    private void Awake()
    {
        // Получаем компоненты Animator и PlayerMovement при инициализации
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        stamina = GetComponent<Stamina>(); // Получаем компонент Stamina
    }

    private void Update()
    {
        // Проверяем, нажата ли клавиша V для атаки снарядом
        // Также проверяем, что таймер перезарядки позволяет атаковать
        if (Input.GetKeyDown(KeyCode.V) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        // Проверяем, нажата ли клавиша B для выполнения ближней атаки
        if (Input.GetKeyDown(KeyCode.B) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            MeleeAttackD();

        // Увеличиваем таймер перезарядки на время, прошедшее с последнего кадра
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        if (stamina.currentStamina >= stamina.attackStaminaCost) // Проверяем, достаточно ли стамины
        {
            SoundManager.instance.PlaySound(fireballSound);
            anim.SetTrigger("attack");
            cooldownTimer = 0;

            // Используем стамину
            stamina.UseStamina(stamina.attackStaminaCost);

            fireballs[FindFireball()].transform.position = firePoint.position;
            fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        }
    }

    private void MeleeAttackD()
    {
        // Проигрываем звук ближней атаки
        SoundManager.instance.PlaySound(meleeAttacklSound);
        // Запускаем анимацию ближней атаки
        anim.SetTrigger("maleAttackD");
        // Сбрасываем таймер перезарядки
        cooldownTimer = 0;

        // Проверяем попадание по врагам в радиусе ближней атаки
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, meleeAttackRange);
        foreach (Collider2D enemy in hitEnemies)
        {
            // Если объект имеет тег "Enemy", наносим ему урон
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Health>().TakeDamage(meleeDamage);
            }
        }
    }

    private int FindFireball()
    {
        // Ищем неактивный снаряд в массиве fireballs
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i; // Возвращаем индекс неактивного снаряда
        }
        return 0; // Если все снаряды активны, возвращаем 0 (можно изменить логику по желанию)
    }

    // Для визуализации области атаки в редакторе
    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null) // Проверяем, задана ли точка атаки
        {
            Gizmos.color = Color.red; // Устанавливаем цвет gizmo
            // Рисуем сферу вокруг точки атаки, чтобы визуализировать радиус
            Gizmos.DrawWireSphere(attackPoint.position, meleeAttackRange);
        }
    }
}
