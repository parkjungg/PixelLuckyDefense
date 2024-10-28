using System;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    private TowerSpawner towerSpawner;    
    [SerializeField]
    private TowerInfoViewer towerInfoViewer;
    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;

    private void Awake() {
        mainCamera = Camera.main;        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            if(!EventSystem.current.IsPointerOverGameObject()) {
                ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                
                if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                    if(hit.transform.CompareTag("Tile")) {
                        towerSpawner.SpawnTower(hit.transform);
                    }
                    if(hit.transform.CompareTag("Tower")) {
                        towerInfoViewer.OnTowerInfo(hit.transform);
                    }         
                }  
            }
        }
    }
}
