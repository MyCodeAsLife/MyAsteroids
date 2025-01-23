using Asteroids;
using System;
using UnityEngine;

public class PresenterTest : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private Transform _objectView;
    private Transformable _objectModel;
    private MovementTest _objectMovement;

    private Vector2 _startPosition;
    private Vector2 _startRotation;

    private Vector2 _displaySize;
    private Vector2 _offsetPosition;

    public event Action<PresenterTest> Destroyed;
    public event Action<float> Updated;

    public int OverlapLayer { get; private set; }

    private void Start()
    {
        _displaySize = _canvas.renderingDisplaySize / _canvas.scaleFactor;
        _offsetPosition = _displaySize / 2 * Config.ScaleWindowSize;
        _objectMovement.SetDisplaySize(_displaySize);
        _objectView = transform;

        // Эта вся часть взята из AsteroidPresenter, адаптировать
        //_objectMovement.SetDisplaySize(_displaySize);
        //_objectMovement.SetMovementSpeed(0.05f);
        //_objectMovement.SetRotationSpeed(200f);
        //_objectMovement.SetDirectionOfMovement(_objectModel.Forward);
        //_objectMovement.SetDirectionOfRotation(1);

        //MyBaseClass myBase = new MyBaseClass();
        //MyDerivedClass myDerived = new MyDerivedClass();
        //object o = myDerived;
        //MyBaseClass b = myDerived;

        //Debug.Log("mybase: Type is " + myBase.GetType().Name);
        //Debug.Log("myDerived: Type is " + myDerived.GetType().Name);
        //Debug.Log("object o = myDerived: Type is " + o.GetType().Name);
        //Debug.Log("MyBaseClass b = myDerived: Type is " + b.GetType().Name);
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        MoveObjectModel(deltaTime);
        MoveObjectView(deltaTime);
        CollisionCheck();
    }

    public void SetModel(Transformable model) => _objectModel = model;
    public void SetView(Transform view) => _objectView = view;
    public void SetMovement(MovementTest movement) => _objectMovement = movement;
    public void SetOverlapLayer(int layer) => OverlapLayer = layer;
    public void SetStartPosition(Vector2 position) => _startPosition = position;
    public void SetStartRotation(Vector2 rotation) => _startRotation = rotation;

    public void CollisionCheck()
    {
        var hit = Physics2D.OverlapCircle(transform.position, 3f, 1 << OverlapLayer);

        if (hit != null)
            Debug.Log(hit.gameObject.name);

        Destroyed?.Invoke(this);
    }

    private void MoveObjectModel(float deltaTime)
    {
        _objectMovement.Tick(deltaTime);
    }

    protected virtual void MoveObjectView(float deltaTime)
    {
        var correctPosition = (_displaySize * _objectModel.Position) - _offsetPosition;
        _objectView.localPosition = correctPosition;
        _objectView.rotation = Quaternion.Euler(0f, 0f, _objectModel.RotationAngle);
    }
}

//internal class MyDerivedClass : MyBaseClass
//{
//    public MyDerivedClass()
//    {
//    }
//}

//internal class MyBaseClass
//{
//    public MyBaseClass()
//    {
//    }
//}