using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempOutLine : MonoBehaviour
{
    Material outLine;
    Renderer renderer;
    List<Material> materialList = new List<Material>();
    void Start()
    {
        outLine = new Material(Shader.Find("Draw/OutlineShader"));
        renderer = this.GetComponent<Renderer>();

        materialList.Clear();
        materialList.AddRange(renderer.sharedMaterials);
        materialList.Add(outLine);

        renderer.materials = materialList.ToArray();
    }

   
}
