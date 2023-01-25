using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DataScript : MonoBehaviour
{
    public List<DrawingData> drawings;
    public GameObject linePrefab; 

    public void Start()
    {
        drawings = LoadAllDrawings();
        if (drawings == null)
        {
            drawings = new List<DrawingData>();
        }
    }

    [Serializable]
    public class DrawingData
    {
        public int id;
        public string time;

        public bool hasParent;

        public float parentPosX;
        public float parentPosY;
        public float parentPosZ;

        public float parentRotX;
        public float parentRotY;
        public float parentRotZ;
        public float parentRotW;

        public LineData[] lines;
    }

    [Serializable]
    public class LineData
    {
        public float PosX;
        public float PosY;
        public float PosZ;

        public float RotX;
        public float RotY;
        public float RotZ;
        public float RotW;

        public PointData[] points;

        public string material;
        public Color color;
        public float width;
    }

    [Serializable]
    public class PointData
    {
        public float PosX;
        public float PosY;
        public float PosZ;
    }

    public DrawingData SerializeData(List<GameObject> selected) 
    {
        DrawingData dd = new DrawingData();
        dd.lines = new LineData[selected.Count];

        for (int i = 0; i < selected.Count; i++)
        {
            Line line = selected[i].GetComponent<Line>();
            LineData ld = new LineData();

            // set up data
            dd.id = 0;
            dd.time = Time.time.ToString();

            if(line.mergeParent != null)
            {
                dd.hasParent = true;

                // set up positions
                dd.parentPosX = line.mergeParent.position.x;
                dd.parentPosY = line.mergeParent.position.y;
                dd.parentPosZ = line.mergeParent.position.z;

                dd.parentRotX = line.mergeParent.rotation.x;
                dd.parentRotY = line.mergeParent.rotation.y;
                dd.parentRotZ = line.mergeParent.rotation.z;
                dd.parentRotW = line.mergeParent.rotation.w;
            }
            else
            {
                dd.hasParent = false;
            }

            // set up points
            ld.points = new PointData[line.points.Count];

            for (int j = 0; j < line.points.Count; j++)
            {
                PointData pd = new PointData();
                pd.PosX = line.points[j].x;
                pd.PosY = line.points[j].y;
                pd.PosZ = line.points[j].z;

                ld.points[j] = pd;
            }

            // set up material, color and width
            ld.material = line.mat.name;
            ld.color = line.col;
            ld.width = line.lr.startWidth;

            // set up line positions and rotations
            ld.PosX = line.transform.position.x;
            ld.PosY = line.transform.position.y;

            // assign all to parent object
            dd.lines[i] = ld;
        }

        return dd;
    }

    public void CreateObjectFromJSON(string json)
    {
        Debug.Log(json);
        DrawingData data = JsonUtility.FromJson<DrawingData>(json);

        // create parent object
        GameObject parent = new GameObject("Drawing_" + data.id);
        if (data.hasParent)
        {
            parent.transform.position = new Vector3(data.parentPosX, data.parentPosY, data.parentPosZ);
            parent.transform.rotation = new Quaternion(data.parentRotX, data.parentRotY, data.parentRotZ, data.parentRotW);
        }

        // create lines
        for (int i = 0; i < data.lines.Length; i++)
        {
            LineData lineData = data.lines[i];

            GameObject line = Instantiate(linePrefab); // fixx
            line.name = "Loaded_Line_" + i;

            line.transform.parent = parent.transform;
            line.transform.position = new Vector3(lineData.PosX, lineData.PosY, lineData.PosZ);
            line.transform.rotation = new Quaternion(lineData.RotX, lineData.RotY, lineData.RotZ, lineData.RotW);

            // add Line component
            Line lc = line.GetComponent<Line>();
            LineRenderer lr = line.GetComponent<LineRenderer>();

            lr.positionCount = lineData.points.Length;

            // set up points
            for (int j = 0; j < lineData.points.Length; j++)
            {
                Vector3 point = new Vector3(lineData.points[j].PosX, lineData.points[j].PosY, lineData.points[j].PosZ);
                lc.points.Add(point);
                lr.SetPosition(j, point);
            }

            // set up material, color and width
            lc.mat = Resources.Load<Material>(lineData.material);
            lc.col = lineData.color;

            lr.startWidth = lineData.width;
            lr.endWidth = lineData.width;
            lr.material = Resources.Load<Material>(lineData.material);
            lr.material = (Material)Resources.Load(lineData.material, typeof(Material));
            lr.startColor = lineData.color;
            lr.endColor = lineData.color;
        }
    }

    public string LoadJson(string fileName)
    {
        if (File.Exists(Application.dataPath + "/Drawings/" + fileName + ".json"))
        {
            return File.ReadAllText(Application.dataPath + "/Drawings/" + fileName + ".json");
        }
        return null;
    }

    // Saving method
    public void SaveDrawing(DrawingData data)
    {
        string json = JsonUtility.ToJson(data);
        string fileName = (drawings.Count + 1).ToString();

        drawings = LoadAllDrawings();

        File.WriteAllText(Application.dataPath + "/Drawings/" + fileName + ".json", json);
    }

    public DrawingData LoadDrawing(string fileName)
    {
        if (File.Exists(Application.dataPath + "/Drawings/" + fileName + ".json"))
        {
            string json = File.ReadAllText(Application.dataPath + "/" + fileName + ".json");
            return JsonUtility.FromJson<DrawingData>(json);
        }
        return null;
    }

    public List<DrawingData> LoadAllDrawings()
    {
        List<DrawingData> drawingList = new List<DrawingData>();
        string[] files = Directory.GetFiles(Application.dataPath + "/Drawings/", "*.json");

        foreach (string file in files)
        {
            string json = File.ReadAllText(file);
            DrawingData drawingData = JsonUtility.FromJson<DrawingData>(json);
            drawingList.Add(drawingData);
        }

        return drawingList;
    }
}
