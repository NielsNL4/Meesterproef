﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour
{
    [SerializeField]
    float power = 3;
    [SerializeField]
    float scale = 1;
    [SerializeField]
    float timeScale = 1;

    float offsetX;
    float offsetY;
    MeshFilter mf;
    // Start is called before the first frame update
    void Start()
    {
        mf = GetComponent<MeshFilter>();
        MakeNoise();
    }

    // Update is called once per frame
    void Update()
    {
        MakeNoise();
        offsetX += Time.deltaTime * timeScale;
        offsetY += Time.deltaTime * timeScale;
    }

    void MakeNoise(){
        Vector3[] vertices = mf.mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = CalculateHeight(vertices[i].x, vertices[i].z) * power;
        }

        mf.mesh.vertices = vertices;
    }

    float CalculateHeight(float x, float y){
        float xCord = x * scale + offsetX;
        float yCord = y * scale + offsetY;

        return Mathf.PerlinNoise(xCord, yCord);
    }
}
