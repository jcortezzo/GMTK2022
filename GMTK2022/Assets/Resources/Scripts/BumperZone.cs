using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BumperZone : MonoBehaviour
{
    [SerializeField]
    private GameObject BUMPER_PREFAB;

    [field:SerializeField]
    public float Size { get; private set; }

    public List<GameObject> Bumpers { get; private set; }

    private Dictionary<DiePositions, Vector2> positionMap = new Dictionary<DiePositions, Vector2>()
    {
        {DiePositions.TOP_LEFT, new Vector2(0, 0)},
        {DiePositions.MIDDLE_LEFT, new Vector2(0, 0.5f)},
        {DiePositions.BOTTOM_LEFT, new Vector2(0, 1)},

        {DiePositions.TOP_MIDDLE, new Vector2(0.5f, 0)},
        {DiePositions.MIDDLE_MIDDLE, new Vector2(0.5f, 0.5f)},
        {DiePositions.BOTTOM_MIDDLE, new Vector2(0.5f, 1)},

        {DiePositions.TOP_RIGHT, new Vector2(1, 0)},
        {DiePositions.MIDDLE_RIGHT, new Vector2(1, 0.5f)},
        {DiePositions.BOTTOM_RIGHT, new Vector2(1, 1)},
    };

    void Awake()
    {
        Bumpers = new List<GameObject>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ClearPreviousBumpers()
    {
        foreach (GameObject bumper in Bumpers)
        {
            if (bumper != null)
            {
                Destroy(bumper.gameObject);
            }
        }
    }

    public void GenerateBumperFormation(int dieRoll)
    {
        ClearPreviousBumpers();
        List<DiePositions> positions = new List<DiePositions>();
        switch (dieRoll)
        {
            case 1:
                positions = new List<DiePositions>()
                {
                    DiePositions.MIDDLE_MIDDLE,
                };
                break;
            case 2:
                positions = new List<DiePositions>()
                {
                    DiePositions.TOP_LEFT,
                    DiePositions.BOTTOM_RIGHT,
                };
                break;
            case 3:
                positions = new List<DiePositions>()
                {
                    DiePositions.TOP_LEFT,
                    DiePositions.MIDDLE_MIDDLE,
                    DiePositions.BOTTOM_RIGHT,
                };
                break;
            case 4:
                positions = new List<DiePositions>()
                {
                    DiePositions.TOP_LEFT,
                    DiePositions.BOTTOM_LEFT,
                    DiePositions.TOP_RIGHT,
                    DiePositions.BOTTOM_RIGHT,
                };
                break;
            case 5:
                positions = new List<DiePositions>()
                {
                    DiePositions.TOP_LEFT,
                    DiePositions.BOTTOM_LEFT,
                    DiePositions.MIDDLE_MIDDLE,
                    DiePositions.TOP_RIGHT,
                    DiePositions.BOTTOM_RIGHT,
                };
                break;
            case 6:
                positions = new List<DiePositions>()
                {
                    DiePositions.TOP_LEFT,
                    DiePositions.MIDDLE_LEFT,
                    DiePositions.BOTTOM_LEFT,
                    DiePositions.TOP_RIGHT,
                    DiePositions.MIDDLE_RIGHT,
                    DiePositions.BOTTOM_RIGHT,
                };
                break;
            default:
                break;
        }
        foreach (DiePositions position in positions)
        {
            Vector2 spawnPos = GetCoordsFromPosition(position);
            spawnPos = transform.TransformPoint(spawnPos);
            GameObject bumper = Instantiate(BUMPER_PREFAB, spawnPos, Quaternion.identity, this.transform);
            Bumpers.Add(bumper);
        }
    }

    private Vector2 GetCoordsFromPosition(DiePositions position)
    {
        return (positionMap[position] * Size) - new Vector2(Size / 2, Size / 2);
    }

    public void SetSize(float size)
    {
        this.Size = size;
    }

    private enum DiePositions
    {
        TOP_LEFT,
        MIDDLE_LEFT,
        BOTTOM_LEFT,

        TOP_MIDDLE,
        MIDDLE_MIDDLE,
        BOTTOM_MIDDLE,

        TOP_RIGHT,
        MIDDLE_RIGHT,
        BOTTOM_RIGHT,
    }
}
