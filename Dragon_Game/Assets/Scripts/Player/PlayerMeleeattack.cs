using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeattack : MonoBehaviour
{
    [SerializeField] protected float damage;

    private void Awake()
    {
        // ������������ ������ ��� �������������
        gameObject.SetActive(false);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

    // ����� ��� ��������� �����
    public void ActivateAttack()
    {
        gameObject.SetActive(true); // ���������� ������
        Invoke("DeactivateAttack", 0.25f); // �������� ����� ����������� ����� 0.25 �������
    }

    // ����� ��� ����������� ����� (���� ����������)
    public void DeactivateAttack()
    {
        gameObject.SetActive(false); // ������������ ������
    }

    // ���������� ������� ������� B
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ActivateAttack();
        }
    }
}