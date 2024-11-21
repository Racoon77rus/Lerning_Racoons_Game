using UnityEngine;

public class Chest : MonoBehaviour
{
    public Item[] itemsToDrop; // Массив предметов, которые могут выпасть
    private bool isOpen = false; // Состояние сундука
    private Animator animator; // Ссылка на Animator

    void Start()
    {
        // Получаем компонент Animator при старте
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
            PlayOpenAnimation(); // Воспроизводим анимацию открытия
            DropItems();
        }
    }

    private void PlayOpenAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("open"); // Устанавливаем триггер для анимации
        }
    }

    private void DropItems()
    {
        foreach (Item item in itemsToDrop)
        {
            // Проверяем, выпадет ли предмет
            if (Random.value <= item.dropChance) // Здесь используется dropChance
            {
                GameObject prefabToDrop = item.GetRandomPrefab(); // Получаем случайный префаб
                if (prefabToDrop != null)
                {
                    Instantiate(prefabToDrop, transform.position, Quaternion.identity);
                }
            }
        }
    }
}
