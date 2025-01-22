using Asteroids;
using System;
using UnityEngine;

public class Presenter : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    // Этот блок переметить в Presenter?
    private Transform _objectView;                  // Временно цепляем вручную
    private Transformable _objectModel;
    private Movement _objectMovement;

    private Vector2 _startPosition;
    private Vector2 _displaySize;
    private Vector2 _offsetPosition;

    public event Action<Presenter> Destroyed;

    public int OverlapLayer { get; private set; }

    private void Start()
    {
        _displaySize = _canvas.renderingDisplaySize / _canvas.scaleFactor;
        _offsetPosition = _displaySize / 2 * Config.ScaleWindowSize;
        _objectMovement.SetDisplaySize(_displaySize);
        _objectView = transform;

        // Эта вся часть взята из AsteroidPresenter, адаптировать
        _objectMovement.SetDisplaySize(_displaySize);
        _objectMovement.SetMovementSpeed(Config.AsteroidMaxSpeed);
        _objectMovement.SetRotationSpeed(200f);
        _objectMovement.SetDirectionOfMovement(_objectModel.Forward);
        _objectMovement.SetDirectionOfRotation(1);

        MyBaseClass myBase = new MyBaseClass();
        MyDerivedClass myDerived = new MyDerivedClass();
        object o = myDerived;
        MyBaseClass b = myDerived;

        Debug.Log("mybase: Type is " + myBase.GetType().Name);
        Debug.Log("myDerived: Type is " + myDerived.GetType().Name);
        Debug.Log("object o = myDerived: Type is " + o.GetType().Name);
        Debug.Log("MyBaseClass b = myDerived: Type is " + b.GetType().Name);
    }

    private void Update()        // Переместить в Presenter?
    {
        float deltaTime = Time.deltaTime;

        MoveObjectModel(deltaTime);
        MoveObjectView(deltaTime);
    }

    public void SetModel(EnemyModel model) => _objectModel = model;
    public void SetView(Transform view) => _objectView = view;
    public void SetMovement(Movement movement) => _objectMovement = movement;
    public void SetOverlapLayer(int layer) => OverlapLayer = layer;
    public void SetStartPosition(Vector2 position) => _startPosition = position;

    public void CollisionCheck()
    {
        var hit = Physics2D.OverlapCircle(transform.position, 3f, 1 << OverlapLayer);

        if (hit != null)
            Debug.Log(hit.gameObject.name);

        Destroyed?.Invoke(this);
    }

    protected virtual void MoveObjectView(float deltaTime)        // Переместить в Presenter
    {
        var correctPosition = (_displaySize * _objectModel.Position) - _offsetPosition;
        _objectView.localPosition = correctPosition;
        _objectView.rotation = Quaternion.Euler(0f, 0f, _objectModel.RotationAngle);
    }

    private void MoveObjectModel(float deltaTime)        // Переместить в Presenter?
    {
        _objectMovement.Tick(deltaTime);
    }
}

internal class MyDerivedClass : MyBaseClass
{
    public MyDerivedClass()
    {
    }
}

internal class MyBaseClass
{
    public MyBaseClass()
    {
    }
}