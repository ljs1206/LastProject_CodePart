using System;
using UnityEngine;

namespace LJS.UI
{
    public class StatRadarUI : MonoBehaviour
    {
        public int[] stats;
        
        [SerializeField] private CanvasRenderer _radarMesh;
        [SerializeField] private Material _radarMat;
        [SerializeField] private Texture2D _radarTexture;
        [SerializeField] private float _maxChartSize = 240f;
        [SerializeField] private int _maxStatValue = 30;
        private void Start()
        {
            UpdateStatVisual();
        }

        private float GetNormalizedValue(int statValue) => statValue / (float)_maxStatValue;
        

        [ContextMenu("Update Visual")]
        private void UpdateStatVisual()
        {
            Mesh mesh = new Mesh();
            
            float angleInc = 360f / stats.Length;
            
            Vector3[] vertices = new Vector3[stats.Length + 1];
            Vector2[] uvs = new Vector2[stats.Length + 1];
            int[] triangles = new int[3 * stats.Length];

            vertices[0] = Vector3.zero;
            uvs[0] = Vector2.zero;
            
            for (int i = 0; i < stats.Length; i++)
            {
                Vector3 vertex = Quaternion.Euler(0, 0, -angleInc*i) * Vector3.up 
                                                                     * _maxChartSize * GetNormalizedValue(stats[i]);
                vertices[i + 1] = vertex;
                uvs[i + 1] = Vector2.one;
                int tIndex = i * 3;
                triangles[tIndex] = 0;
                triangles[tIndex + 1] = i + 1;
                triangles[tIndex + 2] = stats.Length - 1 == i ? 1 : i + 2;
            }

            mesh.vertices = vertices;
            mesh.uv = uvs;
            mesh.triangles = triangles;
            
            _radarMesh.SetMesh(mesh);
            _radarMesh.SetMaterial(_radarMat, _radarTexture);
        }
    }
}
