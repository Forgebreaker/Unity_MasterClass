using UnityEngine;

public class PerlinNoiseMesh : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] float m_threshold = 0.5f;
    [SerializeField, Range(0.05f, 0.3f)] float m_scale = 0.1f;
    int sz = 100;
    float heightScale = 5f;

    // Start is called before the first frame update
    void Start()
    {
        HeightInfo[,] heightArr = new HeightInfo[sz, sz];
        for (int x = 0; x < sz; ++x)
        {
            for (int z = 0; z < sz; ++z)
            {
                float noise = Mathf.PerlinNoise((float)x * m_scale, (float)z * m_scale);
                if (noise < m_threshold)
                {
                    float thRate = (m_threshold - noise) / (m_threshold);
                    heightArr[x, z].height = thRate * heightScale;
                    heightArr[x, z].vertCol = new Color(1f - thRate * 1.1f, 0.8f + thRate * 0.3f, 0.9f - thRate * 1.2f);
                }
                else
                {
                    heightArr[x, z].height = 0f;
                    heightArr[x, z].vertCol = new Color(0.3f, 0.5f, 0.9f);
                }
            }
        }
        GetComponent<MeshFilter>().mesh = CreateHeightMesh(heightArr);
        MeshCollider mc = GetComponent<MeshCollider>();
        mc.sharedMesh = GetComponent<MeshFilter>().mesh;
    }

    void Update() { }

    [System.Serializable]
    public struct HeightInfo
    {
        public float height;
        public Color vertCol;
    };

    public static Mesh CreateHeightMesh(HeightInfo[,] _heightArr, bool _isUnitPerGrid = true)
    {
        int _divX = _heightArr.GetLength(0) - 1;
        int _divZ = _heightArr.GetLength(1) - 1;

        int vertNum = (_divX + 1) * (_divZ + 1);
        int quadNum = _divX * _divZ;
        int[] triangles = new int[quadNum * 6];
        Vector3[] vertices = new Vector3[vertNum];
        Vector2[] uv = new Vector2[vertNum];
        Color[] colors = new Color[vertNum];
        Vector3[] normals = new Vector3[vertNum];
        Vector4[] tangents = new Vector4[vertNum];

        for (int zz = 0; zz < (_divZ + 1);++zz)
        {
            for (int xx = 0; xx < (_divX + 1); ++xx)
            {
                float height = _heightArr[xx, zz].height;
                Vector2 uvPos = new Vector2((float)xx / (float)_divX, (float)zz / (float)_divZ);
                vertices[zz * (_divX + 1) + xx] = new Vector3(uvPos.x - 0.5f, height, uvPos.y - 0.5f);
                if (_isUnitPerGrid)
                {
                    vertices[zz * (_divX + 1) + xx] = Vector3.Scale(vertices[zz * (_divX + 1) + xx], new Vector3(_divX, 1f, _divZ));
                }
                uv[zz * (_divX + 1) + xx] = uvPos;
                colors[zz * (_divX + 1) + xx] = _heightArr[xx, zz].vertCol * 0.1f;
                normals[zz * (_divX + 1) + xx] = new Vector3(0.0f, 1.0f, 0.0f);
                tangents[zz * (_divX + 1) + xx] = new Vector4(1.0f, 0.0f, 0.0f);
                if ((xx < _divX) && (zz < _divZ))
                {
                    int[] sw = { 0, 0, 1, 1, 1, 0, 1, 1, 0, 0, 0, 1 };
                    for (int ii = 0; ii < 6; ++ii)
                    {
                        triangles[(zz * _divX + xx) * 6 + ii] = (zz + sw[ii * 2 + 1]) * (_divX + 1) + (xx + sw[ii * 2 + 0]);
                    }
                }
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.colors = colors;
        mesh.normals = normals;
        mesh.tangents = tangents;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.SetIndices(mesh.GetIndices(0), MeshTopology.Triangles, 0);
        return mesh;
    }
}