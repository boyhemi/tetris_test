using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class BlockController : MonoBehaviour
{
    public int blockId;

    public Color initalColor;

    public Vector2 blockSize;

    public Vector2Int[] blockStructure;

    [HideInInspector]
    public bool isMovable = true;
    [HideInInspector]
    public int posId;
    [HideInInspector]
    public Vector3 basePos;
    [HideInInspector]
    public Vector3 baseScaleValue;
    [HideInInspector]
    public Vector3 finalScale;

    private void OnEnable() {}

    public void SetPosition(int i, bool cp = true)
    {
        Vector2 blockScale = transform.localScale;
        Vector2 colliderSize = GetComponent<BoxCollider>().size * blockScale;

        Vector3 position = new Vector3(colliderSize.x / 2 - 0.5f + colliderSize.x * i, ResolutionManager.GetBlockY(), 0);

        basePos = position;

        if (cp)
            transform.position = basePos;

        posId = i;
    }
    public void MoveBlock(float time, Vector3 dir)
    {
        GetComponent<BlockMovementAnimController>().enabled = true;
        GetComponent<BlockMovementAnimController>().SetAnim(time, dir);
    }

    public bool IsBlockMoving()
    {
        return GetComponent<BlockMovementAnimController>().enabled;
    }

    public Color GetBlockColor()
    {
        if (transform.GetChild(0).name == "Tile")
            return transform.GetChild(0).GetComponent<SpriteRenderer>().color;

        return transform.GetChild(1).GetComponent<SpriteRenderer>().color;
    }

    public Vector2Int GetInitialBlocks()
    {
        Vector3 pr;
        pr = transform.GetChild(0).transform.position;
        return new Vector2Int((int)(pr.x + 0.5f), (int)(pr.y + 0.5f));

    }

    public void ChangeBlockColor(Color color)
    {
        foreach (Transform t in transform)
            if (t.name == "Tile")
                t.GetComponent<SpriteRenderer>().color = color;
    }

    public void ScaleBlockTiles(Vector3 sc)
    {
        foreach (Transform t in transform)
            t.localScale = sc;
    }
    
    public void BlockScaler(bool s, float t)
    {
        GetComponent<ScaleBlockAnimController>().enabled = true;
        GetComponent<ScaleBlockAnimController>().SetAnim(s, t);
    }


    private void Awake()
    {
        baseScaleValue = BoardController.init.boardScaleTile;
        finalScale = BoardController.init.finalScaleBlockTileScale;

        ScaleBlockTiles(finalScale);
    }
}
