using System.Collections;
using UnityEngine;

public class OstrichPoop : MonoBehaviour
{
    public GameObject poopPrefab;
    public float moveSpeed = 2f;
    public float walkDuration = 2f;
    public float poopInterval = 5f;
    public Vector2 areaMin = new Vector2(-9f, 0f);
    public Vector2 areaMax = new Vector2(4.5f, 7f);

    private Vector3 moveDirection;
    private Animator anim;
    private const float fixedZ = 20f;

    void Start()
    {
        anim = GetComponent<Animator>();
        // Set Z on start
        Vector3 pos = transform.position;
        pos.z = fixedZ;
        transform.position = pos;

        StartCoroutine(WalkAndPoopLoop());
    }

    IEnumerator WalkAndPoopLoop()
    {
        while (true)
        {
            anim.SetTrigger("Run");

            moveDirection = GetRandomDirection();

            float elapsed = 0f;

            while (elapsed < walkDuration)
            {
                Vector3 nextPos = transform.position + moveDirection * moveSpeed * Time.deltaTime;

                // Clamp to area
                nextPos.x = Mathf.Clamp(nextPos.x, areaMin.x, areaMax.x);
                nextPos.y = Mathf.Clamp(nextPos.y, areaMin.y, areaMax.y);
                nextPos.z = fixedZ;

                transform.position = nextPos;

                elapsed += Time.deltaTime;
                yield return null;
            }

            anim.SetTrigger("Idle");
            Poop();

            yield return new WaitForSeconds(poopInterval);
        }
    }

    Vector3 GetRandomDirection()
    {
        switch (Random.Range(0, 4))
        {
            case 0: return Vector3.up;
            case 1: return Vector3.down;
            case 2: return Vector3.left;
            case 3: return Vector3.right;
            default: return Vector3.zero;
        }
    }

    void Poop()
    {
        if (poopPrefab != null)
        {
            Vector3 poopPos = transform.position;
            poopPos.z = fixedZ;
            Instantiate(poopPrefab, poopPos, Quaternion.identity);
        }
    }

    void LateUpdate()
    {
        // Final z-enforcer just in case
        if (Mathf.Abs(transform.position.z - fixedZ) > 0.001f)
        {
            Vector3 correctedPos = transform.position;
            correctedPos.z = fixedZ;
            transform.position = correctedPos;
        }
    }
}