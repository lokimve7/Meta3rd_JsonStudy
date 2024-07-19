using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

// 랜덤하게 만들어지는 오브젝트의 정보
[System.Serializable]
public struct ObjectInfo
{
    public int type;
    public Transform tr;
}

// 저장할 데이터 정보
[System.Serializable]
public struct SaveLoadInfo
{
    public int type;
    public Vector3 pos;
    public Vector3 scale;
    public Quaternion rot;
}

[System.Serializable]
public struct SLList
{
    public List<SaveLoadInfo> data;
}

public class CreateManager : MonoBehaviour
{
    // 모든 오브젝트의 정보
    public List<ObjectInfo> allObject = new List<ObjectInfo>();
    public List<SaveLoadInfo> allSaveLoad = new List<SaveLoadInfo>();

    void Start()
    {
        
    }

    void Update()
    {
        // 랜덤한 모양 (큐브, 스피어, 캡슐, 실린더) 을 랜덤한 위치, 크기, 회전 값으로 만들자.
        // 1번키 누르면
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // 랜덤한 모양을 선택
            int type = Random.Range(0, 4); //0, 1, 2, 3
            // 그 모양을 생성
            GameObject go = GameObject.CreatePrimitive((PrimitiveType)type);
            // 생성된 모양을 랜덤한 위치
            go.transform.position = Random.insideUnitSphere * Random.Range(1.0f, 20.0f);
            // 생성된 모양을 랜덤한 크기
            go.transform.localScale = Vector3.one * Random.Range(0.5f, 2.0f);
            // 생성된 모양을 랜덤한 회전
            go.transform.rotation = Random.rotation;

            // 생성된 모양의 정보를 allObject 에 추가
            ObjectInfo info = new ObjectInfo();
            info.type = type;
            info.tr = go.transform;

            allObject.Add(info);
        }

        //2 번키 누르면 allObject 에 있는 내용을 Json 로 변경한 후 저장
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            // allObject 에서 SaveLoadInfo 로 변경
            for(int i = 0; i < allObject.Count; i++)
            {
                SaveLoadInfo saveLoadInfo = new SaveLoadInfo();
                saveLoadInfo.type = allObject[i].type;
                saveLoadInfo.pos = allObject[i].tr.position;
                saveLoadInfo.scale = allObject[i].tr.localScale;
                saveLoadInfo.rot = allObject[i].tr.rotation;

                allSaveLoad.Add(saveLoadInfo);
            }

            // allSaveLoad 를 키 값이 있게 한다?
            SLList slList = new SLList();
            slList.data = allSaveLoad;

            string jsonData = JsonUtility.ToJson(slList, true);
            print(jsonData);

            // jsonData 를 파일로 저장
            FileStream file = new FileStream(Application.dataPath + "/objectInfo.txt", FileMode.Create);
            // jsonData 를 byte 배열로 바꾼다.
            byte[] byteData = Encoding.UTF8.GetBytes(jsonData);
            // byteData 를 file 쓰자.
            file.Write(byteData, 0, byteData.Length);
            // file 닫자.
            file.Close();
            //키보드 쫀득쫀득
            //키보드 너무 가벼움
            //동해물과 백두산이 마르고 닳도록 하느님이 보우하사 우리나라만세

        }

        // 3 번키 누르면 파일 읽어와서 오브젝트 생성
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            // objectInfo.txt 을 읽어오자.
            FileStream file = new FileStream(Application.dataPath + "/objectInfo.txt", FileMode.Open);
            // file 의 크기만큼 byte 배열을 만들자.
            byte[] byteData = new byte[file.Length];
            // byteData 에 file 의 내용을 읽어오자.
            file.Read(byteData, 0, byteData.Length);
            // file 닫자.
            file.Close();

            // byteData 를 문자열로 바꾸자.
            string jsonData = Encoding.UTF8.GetString(byteData);

            // jsonData 를 SLList 변수로 Parsing 하자.
            SLList slList = JsonUtility.FromJson<SLList>(jsonData);
            
            // slList.data 의 갯수만큼 정보를 이용해서 오브젝트를 만들자.
            for(int i = 0; i < slList.data.Count; i++)
            {
                // type 으로 게임오브젝트 만들자.
                GameObject go = GameObject.CreatePrimitive((PrimitiveType)slList.data[i].type);
                // 만들어진 오브젝트의 위치를 셋팅
                go.transform.position = slList.data[i].pos;
                // 만들어진 오브젝트의 회전를 셋팅
                go.transform.rotation = slList.data[i].rot;
                // 만들어진 오브젝트의 크기를 셋팅
                go.transform.localScale = slList.data[i].scale;
            }
        }
    }
}
