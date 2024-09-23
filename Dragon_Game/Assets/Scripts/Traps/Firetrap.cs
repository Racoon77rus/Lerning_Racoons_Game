using UnityEngine;
using System.Collections;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered; //Когда ловушка срабатывает
    private bool active; //Когда ловушка активна и может нанести урон игроку

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) //Если столкновение происходит с объектом с тегом «Player»
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
                StartCoroutine(ActivateFiretrap());

            if (active)
                collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
    private IEnumerator ActivateFiretrap()
    {
        // Покрасить спрайт в красный цвет, чтобы уведомить игрока и активировать ловушку
        triggered = true;
        spriteRend.color = Color.red;

        //Подождать заданное время задержки, активировать ловушку, включить анимацию
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white; //Вернуть спрайту исходный цвет
        active = true;
        anim.SetBool("activated", true);

        //Подождать X секунд, деактивировать ловушку и сбросить все переменные и настройки аниматора
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}