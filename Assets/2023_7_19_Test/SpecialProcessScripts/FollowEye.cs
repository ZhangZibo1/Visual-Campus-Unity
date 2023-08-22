using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FollowEye : MonoBehaviour
{
    public GazeController gazeContro;
    public static Vector2 target;
    public float errorLen;//当注视时由于机器测量导致的误差范围，暂定为100
    public int sensitivity = 10;
    public static bool eyeOnNPC = false;
    public static bool eyeOnBuil = false;
    public static AutoNPCController closestNpc;

    public float followBuildingMinDistace = -100;
    public static float FixedChooseTime = 2;
    public static float EmptyLookTime = 2;
    public static float blinkTime = 0.2f;
    
    bool susblink = false;

    public static float fixedCountTime;
    public float uncFixCountTime;
    public static bool chooseNPC = false;
    public static bool chooseBuild = false;
    public static bool FixedSomething;
    public static BoxCollider tarBuilding = null;
    bool extented = false;
    Vector3 perverseSize;
    float extentSize = 1.0f;

    float eyeOnBuildTime = 0; 
    float eyeOnNPCTime = 0;
    float eyeBuildingForbiddenTimeCount = 0;
    float eyeBuildingForbiddenTime = 0f;
    public static Vector2 position;


    public static float notOnBuildTime = 0; 

    bool exeForbidd = false;
    public static bool closeToTarBuilding = false;
    public static Outline AttractNPC;

    Vector3 originalScal;
    // Start is called before the first frame update
    void Start()
    {
        errorLen = 100;
        originalScal = transform.localScale;
        transform.localScale = originalScal * errorLen / 50;

        gazeContro = GameObject.Find("GazeController").GetComponent<GazeController>();
        followBuildingMinDistace = -100;
        tarBuilding = null;
        blinkTime = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        Attraction();
        Choose();

        BuildingFollow();
        //ChangeBlind(blinKTime);

    }

    public  void changeErrorR(float errL) 
    {
        errorLen = errL;
        transform.localScale = originalScal * errorLen / 50;
    }
    public static void ChangeBlind(float blinKTime)
    {
        
        blinkTime = blinKTime;
    }
    void Attraction()
    {
        if(eyeBuildingForbiddenTimeCount > 0)
        {
            eyeBuildingForbiddenTimeCount -= Time.deltaTime;//选中建筑后不能立马撤回
        }
        position = transform.position;
        //Debug.Log(eyeOnBuil+"eyeOnbuild");
        //Debug.Log("dayu40" + Vector2.Distance(transform.position, gazeContro.gazeScreenPosition));
        //Debug.Log(Description.eyeOnPanel);
        //Debug.Log(eyeBuildingForbiddenTimeCount);
        if (Vector2.Distance(transform.position, gazeContro.gazeScreenPosition) > errorLen && !susblink && !eyeOnNPC && !eyeOnBuil 
            && ! Description.eyeOnPanel&& eyeBuildingForbiddenTimeCount <= 0 )
        {
            //target 在非人物体非建筑上变更
            //Debug.Log(eyeOnBuil + "eyeonBuild");
            
            target = gazeContro.gazeScreenPosition;

            // Debug.Log(true);
        }
        if (Vector2.Distance(transform.position, target) > 0.1)
        {
            transform.position = Vector2.Lerp(transform.position, target, Time.deltaTime * sensitivity);
        }

        //transform.position = gazeContro.gazeScreenPosition;//注意这个transform.position实际上是屏幕坐标
        //Debug.Log(gazeContro.gazeScreenPosition);
        //Debug.Log(transform.position);
        //Debug.Log("eyehitpoint" + ToPersonCamera.eyeHitPoint);
        //npc 吸附
        closestNpc = ToPersonCamera.NPCs[0];
        float minDistance = Vector2.Distance(gazeContro.gazeScreenPosition, ToPersonCamera.activeCamera.WorldToScreenPoint(closestNpc.transform.position));
        foreach (AutoNPCController npcs in ToPersonCamera.NPCs)
        {
            Vector2 temp = ToPersonCamera.activeCamera.WorldToScreenPoint(npcs.transform.position);
            float d = Vector2.Distance(new Vector2(temp.x, temp.y), gazeContro.gazeScreenPosition);
            if (minDistance > d)
            {
                minDistance = d;
                closestNpc = npcs;
            }
        }

        //Debug.Log(minDistance + " mindistance");
        //Debug.Log(ToPersonCamera.MainCamera.WorldToScreenPoint(closestNpc.transform.position) + " " + transform.position);
        if (minDistance < errorLen)
        {
            Ray ray = ToPersonCamera.activeCamera.ScreenPointToRay(transform.position);//click to enter the personal camera
                                                                                                  //Debug.Log(ray.direction);
            RaycastHit hit;//eyeOnBuilding
            
                if(Physics.Raycast(ray, out hit, 1000) && Vector3.Distance(hit.point,closestNpc.transform.position) < 4f) 
                {
                    eyeOnNPC = true;
                    AttractNPC = closestNpc.transform.GetComponentInChildren<Outline>();
                    target = ToPersonCamera.activeCamera.WorldToScreenPoint(closestNpc.transform.GetChild(1).position);
                }
                else
                {
                    eyeOnNPC = false;
                }
           
            //吸附
        }
        else
        {
            eyeOnNPC = false;
        }

        Ray ray2 = ToPersonCamera.activeCamera.ScreenPointToRay(transform.position);//click to enter the personal camera
                                                                                   //Debug.Log(ray.direction);
        RaycastHit hit2;//eyeOnBuilding

        if (Physics.Raycast(ray2, out hit2, 1000, 1<< 12))
        {
            eyeOnNPC = true;
            AttractNPC = hit2.transform.GetComponentInChildren<Outline>();
            target = ToPersonCamera.activeCamera.WorldToScreenPoint(closestNpc.transform.GetChild(1).position);
        }
    }
    void Choose()
    {

        //Debug.Log(chooseBuild);
        
        if ((Vector3.Distance(target, transform.position) <= 0.1 && Vector3.Distance(gazeContro.gazeScreenPosition, transform.position) <= errorLen)
            || eyeOnNPC || eyeOnBuil || Description.eyeOnPanel)
        {
            susblink = false;
            fixedCountTime += Time.deltaTime;
            uncFixCountTime = 0;
            if (eyeOnNPC)
            {
                eyeOnNPCTime += Time.deltaTime;
            }
            else
            {
                //什么时候归零很重要
                //eyeOnNPCTime = 0;
            }

            if (eyeOnBuil)
            {
                eyeOnBuildTime += Time.deltaTime;
            }
            else
            {
                //eyeOnBuildTime = 0;
            }
        }
        else
        {
            uncFixCountTime += Time.deltaTime;
        }
        if(uncFixCountTime < blinkTime)//任何一个非注视在一开始都会被怀疑为眨眼
        {
            susblink = true;
        }

        if (uncFixCountTime > blinkTime)
        {
            //没有眨眼
            susblink = false;
            fixedCountTime = 0;
            FixedSomething = false;
            eyeOnBuildTime = 0;
            eyeOnNPCTime = 0;
        }
        if (eyeOnNPC || eyeOnBuil || Description.eyeOnPanel)
        {
            if (fixedCountTime > FixedChooseTime)
            {
                FixedSomething = true;
                //Debug.Log(chooseSomething);
            }
        }
        else 
        {
            if (fixedCountTime > EmptyLookTime)
            {
                FixedSomething = true;
                //Debug.Log(chooseSomething);
            }
        }
        if ( FixedSomething && eyeOnNPCTime / FixedChooseTime > 0.8)//起码花1.6秒看npc
        {
            chooseNPC = true;
            //Debug.Log(eyeOnNPCTime / FixedChooseTime);
        }
        else
        {
            chooseNPC = false;
        }
        if (FixedSomething && eyeOnBuildTime / FixedChooseTime > 0.8)//起码花1.6秒看building
        {
            chooseBuild = true;
            
            if (!exeForbidd)
            {
                eyeBuildingForbiddenTimeCount = eyeBuildingForbiddenTime;
                exeForbidd = true;
            }
        }
        else
        {
            chooseBuild = false;
            exeForbidd = false;
        }
        if(!eyeOnBuil)
        {
            notOnBuildTime += Time.deltaTime;
        }
        else
        {
            notOnBuildTime = 0;
        }
        if(notOnBuildTime > fixedCountTime * 0.2 && !Description.eyeOnPanel)
        {
            chooseBuild = false;
        }

        

        if(FixedSomething == false) 
        {
            Description.eyeOnPanel = false;
            chooseBuild = false;
            chooseNPC = false;
            
        }

        

    }
    void BuildingFollow()
    {
        
        //Debug.Log("chooseBuild" + chooseBuild);
        Ray ray = ToPersonCamera.activeCamera.ScreenPointToRay(gazeContro.gazeScreenPosition);//click to enter the personal camera
                                                                                            //Debug.Log(ray.direction);
        RaycastHit hit;//eyeOnBuilding
        if (Physics.Raycast(ray, out hit, 1000, 1 << 11))
        {
            //排除非跟随模式详细信息
            
            if (ToPersonCamera.inNPCView && chooseESCUI.eyeOnEsc) return;
            float minFollDis = 100f;
            if (ToPersonCamera.inNPCView)
            {
                minFollDis = 400f;
            }
            else 
            {
                minFollDis = 100f;
            }
            eyeOnBuil = true;
            Vector3 temp = hit.point;
            if(Vector3.Distance(ToPersonCamera.activeCamera.transform.position,temp) > minFollDis)
            {
                Vector3 bias = hit.transform.GetComponent<BoxCollider>().center;
                target = ToPersonCamera.activeCamera.WorldToScreenPoint(hit.transform.position + bias);
                closeToTarBuilding = false;
                //Debug.Log("building center"+ hit.transform.position + bias);
                //Debug.Log(hit.point);
            }
            else 
            {
                target = ToPersonCamera.activeCamera.WorldToScreenPoint(hit.point);
                closeToTarBuilding = true;
            }
            eyeOnNPC = false;
            //Debug.Log("hit Building"+hit.transform.name);
        }
        else
        {
            eyeOnBuil = false;
        }

        if (chooseBuild)
        {
            if (eyeOnBuil) { 
                tarBuilding = hit.transform.GetComponent<BoxCollider>();
            }
            
            

            if (extented == false)
            {

                //不能撤回只执行一次

                perverseSize = tarBuilding.size;
                tarBuilding.size = extentSize * tarBuilding.size;
                extented = true;

                //Debug.Log("big time");
            }

        }
        else
        {
            if (tarBuilding != null)
            {
                //Debug.Log("small times");
                tarBuilding.size = perverseSize;
            }
            extented = false;
            tarBuilding = null;//离开建筑
            //chooseBuild = false;
        }

    }


}
