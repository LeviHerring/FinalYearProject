using System.Collections;
using UnityEngine;

public class OstrichPoop : MonoBehaviour
{
    public GameObject poopPrefab;           // Assign a poop prefab in inspector
    public float moveSpeed = 2f;
    public float walkDuration = 2f;
    public float poopInterval = 5f;
    public Vector2 areaMin = new Vector2(-10, -10);  // Square area bounds
    public Vector2 areaMax = new Vector2(10, 10);

    private Vector2 moveDirection;

    void Start()
    {
        StartCoroutine(WalkAndPoopLoop());
    }

    IEnumerator WalkAndPoopLoop()
    {
        while (true)
        {
            // Pick a random direction (up/down/left/right)
            int dir = Random.Range(0, 4);
            switch (dir)
            {
                case 0: moveDirection = Vector2.up; break;
                case 1: moveDirection = Vector2.down; break;
                case 2: moveDirection = Vector2.left; break;
                case 3: moveDirection = Vector2.right; break;
            }

            float elapsed = 0f;

            while (elapsed < walkDuration)
            {
                Vector2 nextPos = (Vector2)transform.position + moveDirection * moveSpeed * Time.deltaTime;

                // Keep within bounds
                if (nextPos.x >= areaMin.x && nextPos.x <= areaMax.x &&
                    nextPos.y >= areaMin.y && nextPos.y <= areaMax.y)
                {
                    transform.position = nextPos;
                }

                elapsed += Time.deltaTime;
                yield return null;
            }

            // Poop time!
            Poop();

            yield return new WaitForSeconds(poopInterval);
        }
    }

    void Poop()
    {
        if (poopPrefab != null)
        {
            Instantiate(poopPrefab, transform.position, Quaternion.identity);
        }
    }
}
