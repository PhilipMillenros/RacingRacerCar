using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericObjectPool<T> : MonoBehaviour where T : Component
{
    [SerializeField] private T prefab;
    
    
    private Queue<T> objects = new Queue<T>();
    public static GenericObjectPool<T> Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public T Get()
    {
        if(objects.Count == 0) 
            AddObjects(1);
        return objects.Dequeue();
    }
    
    public void ReturnToPool(T objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        objects.Enqueue(objectToReturn);
    }
    private void AddObjects(int count)
    {
        T newObject = Instantiate(prefab);
        newObject.gameObject.SetActive(false);
        objects.Enqueue(newObject);
    }
}