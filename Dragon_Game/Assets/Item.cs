using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour
{
    public GameObject[] itemPrefabs; // ������ �������� ��������
    public float dropChance; // ����������� ��������� ��������

    // ����� ��� ��������� ���������� �������
    public GameObject GetRandomPrefab()
    {
        if (itemPrefabs.Length == 0)
            return null;

        int randomIndex = Random.Range(0, itemPrefabs.Length);
        return itemPrefabs[randomIndex];
    }
}
