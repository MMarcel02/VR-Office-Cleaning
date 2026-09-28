using UnityEngine;

public class CleaningTask : MonoBehaviour
{
    public Collider[] targets;
    public Rigidbody spongeRigidbody;

    public bool cleaningTask;
    public bool IsComplete { get { return cleaningTask; } }

    private bool[] touched;
    private int touchedCount;
    private Renderer spongeRenderer;

    void Start()
    {
        int n = (targets != null) ? targets.Length : 0;
        touched = new bool[n];
        touchedCount = 0;
        cleaningTask = (n == 0);

        if (spongeRigidbody != null)
            spongeRenderer = spongeRigidbody.GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (cleaningTask || targets == null) return;

        if (spongeRigidbody != null && other.attachedRigidbody != spongeRigidbody)
            return;

        for (int i = 0; i < targets.Length; i++)
        {
            if (!touched[i] && other == targets[i])
            {
                touched[i] = true;
                touchedCount++;

                if (touchedCount == targets.Length)
                {
                    cleaningTask = true;

                    if (spongeRenderer != null)
                        spongeRenderer.material.color = Color.green;
                }

                break;
            }
        }
    }
}
