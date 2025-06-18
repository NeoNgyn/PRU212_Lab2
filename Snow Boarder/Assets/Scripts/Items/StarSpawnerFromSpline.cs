using UnityEngine;
using UnityEngine.U2D;

public class StarSpawnerFromSpline : MonoBehaviour
{
    public GameObject starPrefab;
    public float heightAboveGround = 1.5f;
    public int spawnEveryNPoints = 1;
    public int starsPerPoint = 3;

    void Start()
    {
        SpriteShapeController spriteShape = GetComponent<SpriteShapeController>();

        if (spriteShape == null || starPrefab == null)
        {
            return;
        }

        var spline = spriteShape.spline;
        int pointCount = spline.GetPointCount();
        int skipFirstNPoints = 3;
        for (int i = skipFirstNPoints; i < pointCount - 2; i += spawnEveryNPoints)
        {
            Vector3 localPos = spline.GetPosition(i);
            Vector3 worldPos = transform.TransformPoint(localPos);

            Vector3 spawnPos = new Vector3(worldPos.x, worldPos.y + heightAboveGround, 0f);

            float spacing = 1.5f;
            int starsThisPoint = Random.Range(2, 3);

            for (int j = 0; j < starsThisPoint; j++)
            {
                Vector3 offset = new Vector3(j * spacing - spacing, 0f, 0f);
                Instantiate(starPrefab, spawnPos + offset, Quaternion.identity);
            }  
        }
    }
}
