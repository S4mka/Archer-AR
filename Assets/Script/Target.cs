using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject particleEffectPrefab; // Префаб системы частиц

    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, попала ли стрела в мишень
        if (collision.gameObject.CompareTag("Arrow"))
        {
            // Запускаем эффект частиц
            
            Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
