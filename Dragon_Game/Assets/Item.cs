using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour
{
    public GameObject[] itemPrefabs; // Массив префабов предмета
    public float dropChance; // Вероятность выпадения предмета

    // Метод для получения случайного префаба
    public GameObject GetRandomPrefab()
    {
        if (itemPrefabs.Length == 0)
            return null;

        int randomIndex = Random.Range(0, itemPrefabs.Length);
        return itemPrefabs[randomIndex];
    }
}
