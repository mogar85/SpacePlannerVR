using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointScaler : MonoBehaviour
{
    Transform rightPoint, leftPoint, forwardPoint, backPoint, upPoint, downPoint;

    Vector3 rightStartPos, leftStartPos, forwardStartPos, backStartPos, upStartPos, downStartPos;

    Material mat;

    // Use this for initialization
    void Start()
    {
        SetPointRefs();
        SetStartPos();
        SetPosition();
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardInput();

        // check if the scale points have moved
        if (ChangeInPoints())
        {
            Vector3 newScale = CalcScale();
            Vector3 newPos = CalcPosition();

            transform.localPosition = newPos;
            transform.localScale = newScale;
            RepositionPoints(newPos);
            SetStartPos();

            mat.mainTextureScale = new Vector2((newScale.x + newScale.z) / 2, newScale.y) * 10;


        }
    }

    private void SetPosition()
    {
        rightPoint.transform.localPosition = new Vector3(.5f, 0, 0);
        leftPoint.transform.localPosition = new Vector3(-.5f, 0, 0);
        forwardPoint.transform.localPosition = new Vector3(0, 0, 0.5f);
        backPoint.transform.localPosition = new Vector3(0, 0, -0.5f);
        upPoint.transform.localPosition = new Vector3(0, 0.5f, 0);
        downPoint.transform.localPosition = new Vector3(0, -0.5f, 0);
    }

    private void SetStartPos()
    {
        rightStartPos = rightPoint.transform.localPosition;
        leftStartPos = leftPoint.transform.localPosition;
        forwardStartPos = forwardPoint.transform.localPosition;
        backStartPos = backPoint.transform.localPosition;
        upStartPos = upPoint.transform.localPosition;
        downStartPos = downPoint.transform.localPosition;
    }

    private void SetPointRefs()
    {
        rightPoint = transform.parent.GetChild(1);
        leftPoint = transform.parent.GetChild(2);
        forwardPoint = transform.parent.GetChild(3);
        backPoint = transform.parent.GetChild(4);
        upPoint = transform.parent.GetChild(5);
        downPoint = transform.parent.GetChild(6);
    }

    private Vector3 CalcScale()
    {
        Vector3 newScale;
        newScale.x = (Mathf.Abs(rightPoint.transform.localPosition.x - leftPoint.transform.localPosition.x));
        newScale.y = (Mathf.Abs(upPoint.transform.localPosition.y - downPoint.transform.localPosition.y));
        newScale.z = (Mathf.Abs(forwardPoint.transform.localPosition.z - backPoint.transform.localPosition.z));
        return newScale;
    }

    private Vector3 CalcPosition()
    {
        Vector3 newPos;
        newPos.x = (rightPoint.transform.localPosition.x + leftPoint.transform.localPosition.x) / 2;
        newPos.y = (upPoint.transform.localPosition.y + downPoint.transform.localPosition.y) / 2;
        newPos.z = (forwardPoint.transform.localPosition.z + backPoint.transform.localPosition.z) / 2;
        return newPos;
    }

    private void RepositionPoints(Vector3 newPos)
    {
        rightPoint.transform.localPosition = new Vector3(rightPoint.transform.localPosition.x, newPos.y, newPos.z);
        leftPoint.transform.localPosition = new Vector3(leftPoint.transform.localPosition.x, newPos.y, newPos.z);

        forwardPoint.transform.localPosition = new Vector3(newPos.x, newPos.y, forwardPoint.transform.localPosition.z);
        backPoint.transform.localPosition = new Vector3(newPos.x, newPos.y, backPoint.transform.localPosition.z);

        upPoint.transform.localPosition = new Vector3(newPos.x, upPoint.transform.localPosition.y, newPos.z);
        downPoint.transform.localPosition = new Vector3(newPos.x, downPoint.transform.localPosition.y, newPos.z);
    }

    private bool ChangeInPoints()
    {
        return rightStartPos != rightPoint.transform.localPosition ||
                    leftStartPos != leftPoint.transform.localPosition ||
                    forwardStartPos != forwardPoint.transform.localPosition ||
                    backStartPos != backPoint.transform.localPosition ||
                    upStartPos != upPoint.transform.localPosition ||
                    downStartPos != downPoint.transform.localPosition;
    }

    private void KeyboardInput()
    {
        if (Input.GetKey(KeyCode.A))
            rightPoint.transform.localPosition += -Vector3.right;

        if (Input.GetKey(KeyCode.D))
            rightPoint.transform.localPosition += Vector3.right;

        if (Input.GetKey(KeyCode.W))
            forwardPoint.transform.localPosition += Vector3.forward;

        if (Input.GetKey(KeyCode.S))
            forwardPoint.transform.localPosition += -Vector3.forward;

        if (Input.GetKey(KeyCode.Space))
            upPoint.transform.localPosition += Vector3.up;

        if (Input.GetKey(KeyCode.LeftShift))
            upPoint.transform.localPosition += -Vector3.up;
    }
}
