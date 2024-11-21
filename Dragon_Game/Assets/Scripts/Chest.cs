using UnityEngine;

public class Chest : MonoBehaviour
{
    public Item[] itemsToDrop; // ������ ���������, ������� ����� �������
    private bool isOpen = false; // ��������� �������
    private Animator animator; // ������ �� Animator

    void Start()
    {
        // �������� ��������� Animator ��� ������
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && PlayerIsNearby())
        {
            OpenChest();
        }
    }

    private bool PlayerIsNearby()
    {
        return Vector3.Distance(PlayerMovement.instance.transform.position, transform.position) < 2f;
    }

    private void OpenChest()
    {
        if (!isOpen)
        {
            isOpen = true;
            PlayOpenAnimation(); // ������������� �������� ��������
            DropItems();
        }
    }

    private void PlayOpenAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("open"); // ������������� ������� ��� ��������
        }
    }

    private void DropItems()
    {
        foreach (Item item in itemsToDrop)
        {
            // ���������, ������� �� �������
            if (Random.value <= item.dropChance) // ����� ������������ dropChance
            {
                GameObject prefabToDrop = item.GetRandomPrefab(); // �������� ��������� ������
                if (prefabToDrop != null)
                {
                    Instantiate(prefabToDrop, transform.position, Quaternion.identity);
                }
            }
        }
    }
}
