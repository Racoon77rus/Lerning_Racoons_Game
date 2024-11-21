using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // ����������, ������� ����� ��������� ����� ���������
    [SerializeField] private float attackCooldown; // �����, ����������� ��� �������������� �����
    [SerializeField] private Transform firePoint; // �����, ������ ����� �������� �������
    [SerializeField] private GameObject[] fireballs; // ������ ��������, ������� ����� ����� ������������
    [SerializeField] private AudioClip fireballSound; // ���� ����� � ������� �������
    [SerializeField] private AudioClip meleeAttacklSound; // ���� ������� �����

    [SerializeField] private float meleeAttackRange; // ��������� ����� �������� ���
    [SerializeField] private float meleeDamage; // ���� �� �������� ���

    // ����� ���� ��� ����� �����, �������� � ����������
    [SerializeField] private Transform attackPoint;

    private Animator anim; // ��������� Animator ��� �������� ������
    private PlayerMovement playerMovement; // ������ �� ��������� PlayerMovement
    private float cooldownTimer = Mathf.Infinity; // ������ ��� ������������ ������� �����������
    private Stamina stamina; // ������ �� ��������� Stamina

    private void Awake()
    {
        // �������� ���������� Animator � PlayerMovement ��� �������������
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        stamina = GetComponent<Stamina>(); // �������� ��������� Stamina
    }

    private void Update()
    {
        // ���������, ������ �� ������� V ��� ����� ��������
        // ����� ���������, ��� ������ ����������� ��������� ���������
        if (Input.GetKeyDown(KeyCode.V) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        // ���������, ������ �� ������� B ��� ���������� ������� �����
        if (Input.GetKeyDown(KeyCode.B) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            MeleeAttackD();

        // ����������� ������ ����������� �� �����, ��������� � ���������� �����
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        if (stamina.currentStamina >= stamina.attackStaminaCost) // ���������, ���������� �� �������
        {
            SoundManager.instance.PlaySound(fireballSound);
            anim.SetTrigger("attack");
            cooldownTimer = 0;

            // ���������� �������
            stamina.UseStamina(stamina.attackStaminaCost);

            fireballs[FindFireball()].transform.position = firePoint.position;
            fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        }
    }

    private void MeleeAttackD()
    {
        // ����������� ���� ������� �����
        SoundManager.instance.PlaySound(meleeAttacklSound);
        // ��������� �������� ������� �����
        anim.SetTrigger("maleAttackD");
        // ���������� ������ �����������
        cooldownTimer = 0;

        // ��������� ��������� �� ������ � ������� ������� �����
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, meleeAttackRange);
        foreach (Collider2D enemy in hitEnemies)
        {
            // ���� ������ ����� ��� "Enemy", ������� ��� ����
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Health>().TakeDamage(meleeDamage);
            }
        }
    }

    private int FindFireball()
    {
        // ���� ���������� ������ � ������� fireballs
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i; // ���������� ������ ����������� �������
        }
        return 0; // ���� ��� ������� �������, ���������� 0 (����� �������� ������ �� �������)
    }

    // ��� ������������ ������� ����� � ���������
    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null) // ���������, ������ �� ����� �����
        {
            Gizmos.color = Color.red; // ������������� ���� gizmo
            // ������ ����� ������ ����� �����, ����� ��������������� ������
            Gizmos.DrawWireSphere(attackPoint.position, meleeAttackRange);
        }
    }
}
