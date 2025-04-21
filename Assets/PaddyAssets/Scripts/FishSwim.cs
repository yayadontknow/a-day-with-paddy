// using UnityEngine;

// public class FishSwim : MonoBehaviour
// {
//     public Transform body;
//     public Transform back;
//     public Transform tail;

//     public float frequency = 2f;
//     public float amplitude = 20f;

//     private float timer = 0f;

//     void Update()
//     {
//         timer += Time.deltaTime;

//         float bodyAngle = Mathf.Sin(timer * frequency) * amplitude;
//         float backAngle = Mathf.Sin(timer * frequency + 0.5f) * amplitude * 1.2f;
//         float tailAngle = Mathf.Sin(timer * frequency + 1f) * amplitude * 1.5f;

//         body.localRotation = Quaternion.Euler(0f, 0f, bodyAngle);
//         back.localRotation = Quaternion.Euler(0f, 0f, backAngle);
//         tail.localRotation = Quaternion.Euler(0f, 0f, tailAngle);
//     }
// }

using UnityEngine;

public class FishSwim : MonoBehaviour
{
    public Transform body;
    public Transform back;
    public Transform tail;

    public float frequency = 2f;
    public float amplitude = 20f;
    public float swimSpeed = 0.2f;
    public float turnAngle = 180f;

    private float timer = 0f;
    private Vector3 moveDirection;

    private Transform target;
    private bool chasingFood = false;

    void Start()
    {
        moveDirection = transform.forward;
    }

    void Update()
    {
        // Animate body parts
        timer += Time.deltaTime;

        float bodyAngle = Mathf.Sin(timer * frequency) * amplitude;
        float backAngle = Mathf.Sin(timer * frequency + 0.5f) * amplitude * 1.2f;
        float tailAngle = Mathf.Sin(timer * frequency + 1f) * amplitude * 1.5f;

        body.localRotation = Quaternion.Euler(0f, 0f, bodyAngle);
        back.localRotation = Quaternion.Euler(0f, 0f, backAngle);
        tail.localRotation = Quaternion.Euler(0f, 0f, tailAngle);

        // If target was destroyed, stop chasing
        if (chasingFood && target == null)
        {
            chasingFood = false;
            moveDirection = transform.forward;
            transform.position -= moveDirection * swimSpeed * Time.deltaTime;

            // Lock Y position
            Vector3 pos = transform.position;
            pos.y = 0.49f;
            transform.position = pos;
            swimSpeed = 0.2f;
            frequency = 2f;
        }

        // If chasing food and target is valid, move toward it
        if (chasingFood && target != null)
        {
            Vector3 directionToTarget = -(target.position - transform.position).normalized;
            moveDirection = directionToTarget;
            transform.rotation = Quaternion.LookRotation(moveDirection);
            transform.position -= moveDirection * swimSpeed * Time.deltaTime;
            swimSpeed = 1f;
            frequency = 5f;
        }
        else
        {
            Vector3 pos = transform.position;

            // Smoothly move Y position towards 0.49f
            float targetY = 0.358f;
            float smoothSpeed = 0.2f; // Adjust this to control how fast it moves
            pos.y = Mathf.MoveTowards(pos.y, targetY, smoothSpeed * Time.deltaTime);

            transform.position = pos;

            // Apply horizontal movement
            transform.position -= moveDirection * swimSpeed * Time.deltaTime;
            swimSpeed = 0.2f;
            frequency = 2f;
        }

        // Move fish
        
    }

    public void SetTarget(Transform food)
    {
        target = food;
        chasingFood = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WaterBoundary"))
        {
            moveDirection = Vector3.Reflect(moveDirection, other.transform.forward);
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
        else if (chasingFood && other.CompareTag("FishFood"))
        {
            Destroy(other.gameObject);
            chasingFood = false;
            target = null;

            moveDirection = transform.forward;
        }
    }
}
