using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour
{
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public float dodge;
    public float smoothing;
    public float tilt;

    private float targetManuever;
    private float currentSpeed;
    private Rigidbody rb;
    private Boundary boundary;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boundary = FindObjectOfType<Boundary>();
    }

    private void Start()
    {
        currentSpeed = rb.velocity.z;
        StartCoroutine(Evade());
    }

    private void FixedUpdate()
    {
        float newManuever = Mathf.MoveTowards(rb.velocity.x, targetManuever, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManuever, 0f, currentSpeed);

        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, -boundary.screenBounds.x + 0.1f, boundary.screenBounds.x - 0.1f),
            0f,
            Mathf.Clamp(rb.position.z, -boundary.screenBounds.y, boundary.screenBounds.y)
            );
        rb.rotation = Quaternion.Euler(0f, 0f, rb.velocity.x * -tilt);
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while(true)
        {
            targetManuever = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManuever = 0;
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
        }
    }
}