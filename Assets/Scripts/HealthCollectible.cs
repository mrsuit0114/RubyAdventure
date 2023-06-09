using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();
        if (controller != null)
        {
            if (controller.health < controller.maxHealth)
            {
                controller.ChangeHealth(1);
                Destroy(gameObject);

                controller.PlaySound(collectedClip);  // 오브젝트가 삭제되면 사운드가 멈추기 때문에 여기서 사운드 재생을하는데
                // 그럼 코드도 영향받아서 삭제되면 코드실행이 안될수도있는게 아닌가?
            }
        }
    }
}
