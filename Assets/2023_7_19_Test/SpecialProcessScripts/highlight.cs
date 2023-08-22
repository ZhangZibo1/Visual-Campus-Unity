using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highlight : MonoBehaviour
{
    public  GameObject buildingP;
    public GameObject colliderP;
    public Outline[] buildings;
    public List<Transform> colliders;
    public int[] colliderToBuildingList;
    //public List<Transform> l;
    Outline highlightO;
    // Start is called before the first frame update
    void Start()
    {
        buildingP = GameObject.Find("City");//增加building时加上collider(building layer), outline 对应位置，说明内容和tag
        colliderP = GameObject.Find("BuildingColliders");
        colliders = new List<Transform>(colliderP.GetComponentsInChildren<Transform>());
        buildings = buildingP.GetComponentsInChildren<Outline>();
        colliderToBuildingList = new int[colliders.Capacity];
        for(int i = 1;i < colliders.Capacity ; i++) 
        {
            colliderToBuildingList[i] = 0;
            Vector3 temp1 = colliders[i].transform.position + colliders[i].transform.GetComponent<BoxCollider>().center;
            float minD = Vector3.Distance(temp1, buildings[0].transform.position);
            for (int j = 0;j < buildings.Length ; j++) 
            {
                //Debug.Log(minD);
                Outline bui = buildings[j];
                Vector3 temp = colliders[i].transform.position + colliders[i].transform.GetComponent<BoxCollider>().center;
                if (Vector3.Distance(temp,bui.transform.position) < minD)
                {
                    minD = Vector3.Distance(temp, bui.transform.position);
                    colliderToBuildingList[i] = j;
                }

            }
        }
    }
    private void GetAllInstallOutlineEnable(Transform transform)
    {
        if (transform.childCount == 0) { return; }
        foreach (Transform item in transform)
        {
            if (item.GetComponent<Outline>() == null)
            {
                Outline l = item.gameObject.AddComponent<Outline>();
                l.OutlineColor = transform.GetComponent<Outline>().OutlineColor;
                l.OutlineWidth = transform.GetComponent<Outline>().OutlineWidth;
            }
            item.GetComponent<Outline>().enabled = true;
            //Debug.Log(item);
            GetAllInstallOutlineEnable(item);
        }
    }
    void AllDisAbled(Transform transform) 
    {
        if (transform.childCount == 0) { return; }
        foreach (Transform item in transform)
        {
            if (item.GetComponent<Outline>() == null)
            {
                Outline l = item.gameObject.AddComponent<Outline>();
                l.OutlineColor = transform.GetComponent<Outline>().OutlineColor;
                l.OutlineWidth = transform.GetComponent<Outline>().OutlineWidth;
                
            }
            item.GetComponent<Outline>().enabled = false;
            //Debug.Log(item);
            AllDisAbled(item);
        }
    }

    void ActiveOutline(Outline paren) 
    {

        paren.enabled = true;
        GetAllInstallOutlineEnable(paren.transform);


    }
    void UnActive(Outline paren) 
    {
        paren.enabled = false;
        AllDisAbled(paren.transform);
    }
    // Update is called once per frame
    void Update()
    {
        Buildinghl();
        NPChl();
        SmallBuilHL();
        CancelHl();
    }
    void Buildinghl() 
    {

        if (FollowEye.chooseBuild)
        {
            int index = 0;
            for (int i = 0; i < colliders.Capacity; i++)
            {
                if (FollowEye.tarBuilding.transform == colliders[i])
                {
                    index = i;
                }
            }
            Outline h = buildings[colliderToBuildingList[index]];
            Debug.Log(colliderToBuildingList[index]);
            if (highlightO == null)
            {
                highlightO = h;
                ActiveOutline(highlightO);
            }
            if (h != highlightO)
            {

                UnActive(highlightO);
                highlightO = h;
                ActiveOutline(highlightO);

            }
        }
        
    }
    void NPChl() 
    {
        if (FollowEye.eyeOnNPC)
        {

            Outline h = FollowEye.AttractNPC;
            ///Debug.Log(colliderToBuildingList[index]);
            if (highlightO == null || h == highlightO)
            {
                highlightO = h;
                ActiveOutline(highlightO);
            }
            if (h != highlightO)
            {

                UnActive(highlightO);
                highlightO = h;
                ActiveOutline(highlightO);

            }
        }
    }

    void SmallBuilHL() 
    {
        if (InNPCView.eyeOnSamllBuil && ToPersonCamera.inNPCView) 
        {
           
            if( InNPCView.hitParent.transform.GetComponent<Outline>() == null) 
            {
                Outline m = InNPCView.hitParent.transform.gameObject.AddComponent<Outline>();
                m.enabled = false;
                m.OutlineColor = Color.blue;
                m.OutlineWidth = 3;
                Debug.Log(m.name);
                Debug.Log(InNPCView.hitParent.transform.name);
            }
            Outline h = InNPCView.hitParent.transform.GetComponent<Outline>();
            ///Debug.Log(colliderToBuildingList[index]);
            if (highlightO == null || h == highlightO)
            {
                highlightO = h;
                ActiveOutline(highlightO);
            }
            if (h != highlightO)
            {

                UnActive(highlightO);
                highlightO = h;
                ActiveOutline(highlightO);

            }
        }
        
    }

    void CancelHl() 
    {
        if (!FollowEye.chooseBuild && !FollowEye.eyeOnNPC && !InNPCView.eyeOnSamllBuil)
        {
            if (highlightO != null)
            {
                UnActive(highlightO);
                highlightO = null;
            }

        }
    }
}
