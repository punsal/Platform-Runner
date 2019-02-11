using System.Collections.Generic;
using UnityEngine;

public class SkylineManager : MonoBehaviour
{
    public Transform prefab;
    public int numberOfObjects;
    public float recycleOffset;
    public Vector3 startPosition;

    private Vector3 nextPosition;
    private Queue<Transform> objectQueue;

    private void Start()
    {
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;

        objectQueue = new Queue<Transform>(numberOfObjects);
        for (int i = 0; i < numberOfObjects; i++)
        {
            Transform skyline = Instantiate(
                prefab,
                new Vector3(0f, 0f, -100f),
                Quaternion.identity
                );
            skyline.SetParent(transform);
            objectQueue.Enqueue(skyline);
        }

        enabled = false;
    }

    private void GameStart()
    {
        nextPosition = startPosition;
        for (int i = 0; i < numberOfObjects; i++)
        {
            Recycle();
        }

        enabled = true;
    }

    private void GameOver()
    {
        enabled = false;
    }

    private void Update()
    {
        if (objectQueue.Peek().localPosition.x + recycleOffset < Runner.distanceTraveled)
        {
            Recycle();
        }
    }

    public Vector3 minSize, maxSize;

    private void Recycle()
    {
        Vector3 scale = new Vector3(
            Random.Range(minSize.x, maxSize.x),
            Random.Range(minSize.y, maxSize.y),
            Random.Range(minSize.z, maxSize.z)
            );

        Vector3 position = nextPosition;
        position.x += scale.x * 0.5f;
        position.y += scale.y * 0.5f;

        Transform obj = objectQueue.Dequeue();
        obj.localScale = scale;
        obj.localPosition = position;
        nextPosition.x += scale.x;
        objectQueue.Enqueue(obj);
    }
}
