using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private Transform _endPosition;
    
    private LineRenderer _lineRenderer;
    private List<RopeSegment> _ropeSegments = new();
    private float _ropeSegLen = 0.25f;
    private int _segmentLength = 10;
    private float _lineWidth = 0.1f;
    
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        Vector3 ropeStartPoint = transform.position;

        for (int i = 0; i < _segmentLength; i++)
        {
            _ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= _ropeSegLen;
        }
    }
    
    void Update()
    {
        DrawRope();
    }

    private void FixedUpdate()
    {
        Simulate();
    }

    private void Simulate()
    {
        Vector2 forceGravity = new Vector2(0f, -1.5f);

        for (int i = 1; i < _segmentLength; i++)
        {
            RopeSegment firstSegment = _ropeSegments[i];
            Vector2 velocity = firstSegment.PositionNow - firstSegment.PositionOld;
            firstSegment.PositionOld = firstSegment.PositionNow;
            firstSegment.PositionNow += velocity;
            firstSegment.PositionNow += forceGravity * Time.fixedDeltaTime;
            _ropeSegments[i] = firstSegment;
        }

        for (int i = 0; i < 50; i++)
        {
            ApplyConstraint();
        }
    }

    private void ApplyConstraint()
    {
        RopeSegment firstSegment = _ropeSegments[0];
        firstSegment.PositionNow = transform.position;
        _ropeSegments[0] = firstSegment;
        
        RopeSegment endSegment = _ropeSegments[^1];
        endSegment.PositionNow = _endPosition.position;
        _ropeSegments[^1] = endSegment;
       // _endPosition.position = _ropeSegments[^1].PositionNow;


        for (int i = 0; i < _segmentLength - 1; i++)
        {
            RopeSegment firstSeg = _ropeSegments[i];
            RopeSegment secondSeg = _ropeSegments[i + 1];

            float dist = (firstSeg.PositionNow - secondSeg.PositionNow).magnitude;
            float error = Mathf.Abs(dist - _ropeSegLen);
            Vector2 changeDir = Vector2.zero;

            if (dist > _ropeSegLen)
            {
                changeDir = (firstSeg.PositionNow - secondSeg.PositionNow).normalized;
            } else if (dist < _ropeSegLen)
            {
                changeDir = (secondSeg.PositionNow - firstSeg.PositionNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (i != 0)
            {
                firstSeg.PositionNow -= changeAmount * 0.5f;
                _ropeSegments[i] = firstSeg;
                secondSeg.PositionNow += changeAmount * 0.5f;
                _ropeSegments[i + 1] = secondSeg;
            }
            else
            {
                secondSeg.PositionNow += changeAmount;
                _ropeSegments[i + 1] = secondSeg;
            }
        }

    }

    private void DrawRope()
    {
        float lineWidth = _lineWidth;
        _lineRenderer.startWidth = lineWidth;
        _lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositions = new Vector3[_segmentLength];
        for (int i = 0; i < _segmentLength; i++)
        {
            ropePositions[i] = _ropeSegments[i].PositionNow;
        }

        _lineRenderer.positionCount = ropePositions.Length;
        _lineRenderer.SetPositions(ropePositions);
    }

    private struct RopeSegment
    {
        public Vector2 PositionNow;
        public Vector2 PositionOld;

        public RopeSegment(Vector2 position)
        {
            PositionNow = position;
            PositionOld = position;
        }
    }
}
