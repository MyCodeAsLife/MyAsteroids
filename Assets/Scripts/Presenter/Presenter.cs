using UnityEngine;

public class Presenter : MonoBehaviour
{
    public int OverlapLayer { get; private set; }

    public void SetOverlapLayer(int layer)
    {
        OverlapLayer = layer;
    }

    public void CollisionCheck()
    {
        var hit = Physics2D.OverlapCircle(transform.position, 3f, 1 << OverlapLayer);

        if (hit != null)
            Debug.Log(hit.gameObject.name);
    }
}
