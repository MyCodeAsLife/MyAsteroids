using System;
using UnityEngine;

public class Presenter : MonoBehaviour
{
    public class MyBaseClass
    {
    }

    public class MyDerivedClass : MyBaseClass
    {
    }

    public event Action<Presenter> Destroyed;

    public int OverlapLayer { get; private set; }

    private void Start()
    {
        MyBaseClass myBase = new MyBaseClass();
        MyDerivedClass myDerived = new MyDerivedClass();
        object o = myDerived;
        MyBaseClass b = myDerived;

        Debug.Log("mybase: Type is " + myBase.GetType().Name);
        Debug.Log("myDerived: Type is " + myDerived.GetType().Name);
        Debug.Log("object o = myDerived: Type is " + o.GetType().Name);
        Debug.Log("MyBaseClass b = myDerived: Type is " + b.GetType().Name);
    }

    public void SetOverlapLayer(int layer)
    {
        OverlapLayer = layer;
    }

    public void CollisionCheck()
    {
        var hit = Physics2D.OverlapCircle(transform.position, 3f, 1 << OverlapLayer);

        if (hit != null)
            Debug.Log(hit.gameObject.name);

        Destroyed?.Invoke(this);
    }
}
