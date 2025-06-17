using UnityEngine;
using System.Collections;
public abstract class ActionBase : MonoBehaviour
{
    [Header("Action Execution Settings")]
    [SerializeField] protected bool executeOnStart = false;
    [SerializeField] protected bool executeOnce = false;
    [SerializeField] protected bool executeOnCollision = false;

    protected bool hasExecuted = false;
    
    protected virtual void Start()
    {
        if (executeOnStart && (!executeOnce || !hasExecuted))
        {
            Execute();
        }
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (executeOnCollision && (!executeOnce || !hasExecuted))
        {
            Execute();
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (executeOnCollision && (!executeOnce || !hasExecuted))
        {
            Execute();
        }
    }
    
    protected void Execute()
    {
        if (executeOnce && hasExecuted)
        {
            return;
        }
        
        ExecuteAction();
        if (executeOnce)
        {
            hasExecuted = true;
        }
    }
    
    protected abstract void ExecuteAction();
}