using UnityEngine;

public class PlatformPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform[] patrolPoints; // ������ ����� ��������������

    [Header("Platform")]
    [SerializeField] private Transform platform; // ���������, ������� ����� ����������

    [Header("Movement parameters")]
    [SerializeField] private float speed; // �������� �������� ���������
    private int currentPointIndex; // ������ ������� ����� ��������������

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration; // ����� �������� �� �����
    private float idleTimer; // ������ ��� ������������ ������� ��������

    private void Start()
    {
        currentPointIndex = 0; // �������� � ������ �����
    }

    private void Update()
    {
        // ������� ��������� � ������� �����
        MoveTowardsCurrentPoint();
    }

    private void MoveTowardsCurrentPoint()
    {
        // ���������, ���� �� ����� ��� ��������������
        if (patrolPoints.Length == 0) return;

        // �������� ������� ����� ��������������
        Transform targetPoint = patrolPoints[currentPointIndex];

        // ������� ��������� � ������� ������� �����
        platform.position = Vector3.MoveTowards(platform.position, targetPoint.position, speed * Time.deltaTime);

        // ���������, �������� �� �� ������� �����
        if (Vector3.Distance(platform.position, targetPoint.position) < 0.1f)
        {
            // ����������� ������ ��������
            idleTimer += Time.deltaTime;

            // ���� ������ ���������� �������, ��������� � ��������� �����
            if (idleTimer > idleDuration)
            {
                // ���������� ������
                idleTimer = 0;

                // ��������� � ��������� �����
                currentPointIndex++;
                if (currentPointIndex >= patrolPoints.Length)
                {
                    currentPointIndex = 0; // ������������ � ������ �����
                }
            }
        }
        else
        {
            // ���� �� ��� �� �������� �����, ���������� ������ ��������
            idleTimer = 0;
        }
    }
}
