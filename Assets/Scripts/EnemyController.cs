using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//시간을 기준으로 짠 예시코드
public class EnemyController : MonoBehaviour
{
    bool broken;
    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;

    public AudioClip hitByEnemySound;
    public ParticleSystem smokeEffect;

    Rigidbody2D rigidbody2d;  // 기존 필드를 덮음
    float timer;
    int direction = 1;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        broken = true;
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!broken)
        {
            return;
        }
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }


        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        rigidbody2d.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.PlaySound(hitByEnemySound);
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
        broken = false;
        rigidbody2d.simulated = false;
    }
}

// bool보다 int 형으로 관리하는게 코드가 깔끔하다, 위치를 기준으로 짠 코드
/*public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D enemyRigidbody2d;
    int upDownDirection = 1;  // 1 -> up, -1 -> down
    float moveSpeed = 20f;
    float yRange = 4f;
    void Start()
    {
        enemyRigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = enemyRigidbody2d.position;   // Vector2 position = transform.position; 떨림 개선한 코드로 교체함
        position.y = position.y + moveSpeed* upDownDirection * Time.deltaTime;
        if (position.y > yRange || position.y <-yRange) {
            upDownDirection = -upDownDirection;
        }
        enemyRigidbody2d.MovePosition(position);  // transform.position = position; 떨림 개선한 코드로 교체함
    }
}*/


// bool변수로 썼을 경우
/*public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D enemyRigidbody2d;
    bool upDownDirection;  // true -> up, false -> down
    float moveSpeed = 20f;
    float yRange = 4f;
    void Start()
    {
        enemyRigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = enemyRigidbody2d.position;   // Vector2 position = transform.position; 떨림 개선한 코드로 교체함
        if (upDownDirection)
        {
            position.y = position.y + moveSpeed * Time.deltaTime;
        }
        else
        {
            position.y = position.y - moveSpeed * Time.deltaTime;
        }
        if (position.y > yRange)
        {
            upDownDirection = false;
        }
        else if (position.y < -yRange)
        {
            upDownDirection = true;
        }
        enemyRigidbody2d.MovePosition(position);  // transform.position = position; 떨림 개선한 코드로 교체함
    }
}*/

