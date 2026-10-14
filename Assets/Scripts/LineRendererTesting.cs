using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_Testing : MonoBehaviour {
    [SerializeField] private lr_LineController line;
    [SerializeField] private Transform[] points;

    private void Start() {
        line.SetUpLine(points);
    }
}