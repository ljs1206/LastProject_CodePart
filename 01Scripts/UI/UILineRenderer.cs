using UnityEngine;
using UnityEngine.UI;

namespace LJS.UI
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class UILineRenderer : MaskableGraphic
    {
        public Vector2[] points;
        public float thickness = 1f;
        public bool center = true; //중심점을 가운데로 둘꺼냐?
        public Color lineColor;

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
            if (points.Length < 2) return; //길이가 2보다 작으면 점하나 또는 0이니까 선이 없다.

            for (int i = 0; i < points.Length - 1; i++)
            {
                CreateLineSegment(points[i], points[i + 1], vh);
                //뭔가 할꺼야.
                int index = i * 5;
                
                vh.AddTriangle(index, index + 1, index +3 );  //정점 3개를 넣으면 돼
                vh.AddTriangle( index + 3, index + 2, index );

                if (i != 0)
                {
                    //트라이앵글 2개를 더 추가해서 부드럽게 만들 수 있겠지.
                    vh.AddTriangle(index, index-1, index-3);
                    vh.AddTriangle(index+1, index - 1, index -2);
                }
            }
        }

        private void CreateLineSegment(Vector3 point1, Vector3 point2, VertexHelper vh)
        {
            Vector3 offset = center ? (rectTransform.sizeDelta * 0.5f) : Vector3.zero;
            
            UIVertex vertex = UIVertex.simpleVert;
            vertex.color = lineColor;
            
            Quaternion point1Rot = Quaternion.Euler(0, 0, RotatePointToward(point1, point2) + 90f);
            vertex.position = point1Rot * new Vector3(-thickness * 0.5f, 0); //왼쪽에 있는 점 회전
            vertex.position += point1 - offset;
            vh.AddVert(vertex);
            
            vertex.position = point1Rot * new Vector3(thickness * 0.5f, 0); //오른쪽에 있는 점 회전
            vertex.position += point1 - offset;
            vh.AddVert(vertex);
            
            Quaternion point2Rot = Quaternion.Euler(0, 0, RotatePointToward(point2, point1) - 90f);
            vertex.position = point2Rot * new Vector3(-thickness * 0.5f, 0); //왼쪽에 있는 점 회전
            vertex.position += point2 - offset;
            vh.AddVert(vertex);
            
            vertex.position = point2Rot * new Vector3(thickness * 0.5f, 0); //오른쪽에 있는 점 회전
            vertex.position += point2 - offset;
            vh.AddVert(vertex);

            vertex.position = point2 - offset;
            vh.AddVert(vertex);
        }

        private float RotatePointToward(Vector3 vertex, Vector3 target)
            => Mathf.Atan2(target.y - vertex.y, target.x - vertex.x) * Mathf.Rad2Deg;
    }
}
