using System.Collections;
using UnityEngine;

public class PoopTwo : MonoBehaviour
{
    public GameObject poopPrefab;
    public float moveSpeed = 2f;
    public float poopInterval = 5f;
    public float moveDuration = 2f;

    // Boundaries for movement (with z fixed to 20)
    public Vector3 areaMin = new Vector3(-9f, 0f, 20f);
    public Vector3 areaMax = new Vector3(4.5f, 7f, 20f);

    private const float fixedZ = -5f;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        SetZPosition();

        StartCoroutine(PoopAndMoveCycle());
    }

    void SetZPosition()
    {
        // Ensure that Z is always 20
        Vector3 pos = transform.position;
        pos.z = fixedZ;
        transform.position = pos;
    }

    IEnumerator PoopAndMoveCycle()
    {
        while (true)
        {
            // First Poop
            Poop();

            // Wait before moving
            yield return new WaitForSeconds(poopInterval);

            // Move in a random direction
            yield return StartCoroutine(MoveInRandomDirection());

            // Wait for the move to finish
            yield return new WaitForSeconds(moveDuration);
        }
    }

    void Poop()
    {
        if (poopPrefab != null)
        {
            // Ensure the poop is at the current position with fixed Z
            Vector3 poopPos = transform.position;
            poopPos.z = fixedZ;
            Instantiate(poopPrefab, poopPos, Quaternion.identity);
            anim.SetTrigger("Idle");
        }
    }

    IEnumerator MoveInRandomDirection()
    {
        // Pick a random direction (up, down, left, right)
        Vector3 randomDirection = GetRandomDirection();

        // Move for the specified duration
        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            Vector3 nextPos = transform.position + randomDirection * moveSpeed * Time.deltaTime;
            nextPos.x = Mathf.Clamp(nextPos.x, areaMin.x, areaMax.x);
            nextPos.y = Mathf.Clamp(nextPos.y, areaMin.y, areaMax.y);
            nextPos.z = fixedZ;  // Ensure Z stays fixed at 20

            transform.position = nextPos;
            elapsedTime += Time.deltaTime;
            yield return null;
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

    void LateUpdate()
    {
        // Ensure that Z remains at 20 in case something else modifies it
        if (Mathf.Abs(transform.position.z - fixedZ) > 0.001f)
        {
            Vector3 correctedPos = transform.position;
            correctedPos.z = fixedZ;
            transform.position = correctedPos;
        }
    }
}
