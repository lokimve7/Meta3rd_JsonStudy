using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

// �����ϰ� ��������� ������Ʈ�� ����
[System.Serializable]
public struct ObjectInfo
{
    public int type;
    public Transform tr;
}

// ������ ������ ����
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
    // ��� ������Ʈ�� ����
    public List<ObjectInfo> allObject = new List<ObjectInfo>();
    public List<SaveLoadInfo> allSaveLoad = new List<SaveLoadInfo>();

    void Start()
    {
        
    }

    void Update()
    {
        // ������ ��� (ť��, ���Ǿ�, ĸ��, �Ǹ���) �� ������ ��ġ, ũ��, ȸ�� ������ ������.
        // 1��Ű ������
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // ������ ����� ����
            int type = Random.Range(0, 4); //0, 1, 2, 3
            // �� ����� ����
            GameObject go = GameObject.CreatePrimitive((PrimitiveType)type);
            // ������ ����� ������ ��ġ
            go.transform.position = Random.insideUnitSphere * Random.Range(1.0f, 20.0f);
            // ������ ����� ������ ũ��
            go.transform.localScale = Vector3.one * Random.Range(0.5f, 2.0f);
            // ������ ����� ������ ȸ��
            go.transform.rotation = Random.rotation;

            // ������ ����� ������ allObject �� �߰�
            ObjectInfo info = new ObjectInfo();
            info.type = type;
            info.tr = go.transform;

            allObject.Add(info);
        }

        //2 ��Ű ������ allObject �� �ִ� ������ Json �� ������ �� ����
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            // allObject ���� SaveLoadInfo �� ����
            for(int i = 0; i < allObject.Count; i++)
            {
                SaveLoadInfo saveLoadInfo = new SaveLoadInfo();
                saveLoadInfo.type = allObject[i].type;
                saveLoadInfo.pos = allObject[i].tr.position;
                saveLoadInfo.scale = allObject[i].tr.localScale;
                saveLoadInfo.rot = allObject[i].tr.rotation;

                allSaveLoad.Add(saveLoadInfo);
            }

            // allSaveLoad �� Ű ���� �ְ� �Ѵ�?
            SLList slList = new SLList();
            slList.data = allSaveLoad;

            string jsonData = JsonUtility.ToJson(slList, true);
            print(jsonData);

            // jsonData �� ���Ϸ� ����
            FileStream file = new FileStream(Application.dataPath + "/objectInfo.txt", FileMode.Create);
            // jsonData �� byte �迭�� �ٲ۴�.
            byte[] byteData = Encoding.UTF8.GetBytes(jsonData);
            // byteData �� file ����.
            file.Write(byteData, 0, byteData.Length);
            // file ����.
            file.Close();
            //Ű���� �˵��˵�
            //Ű���� �ʹ� ������
            //���ع��� ��λ��� ������ �⵵�� �ϴ����� �����ϻ� �츮���󸸼�

        }

        // 3 ��Ű ������ ���� �о�ͼ� ������Ʈ ����
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            // objectInfo.txt �� �о����.
            FileStream file = new FileStream(Application.dataPath + "/objectInfo.txt", FileMode.Open);
            // file �� ũ�⸸ŭ byte �迭�� ������.
            byte[] byteData = new byte[file.Length];
            // byteData �� file �� ������ �о����.
            file.Read(byteData, 0, byteData.Length);
            // file ����.
            file.Close();

            // byteData �� ���ڿ��� �ٲ���.
            string jsonData = Encoding.UTF8.GetString(byteData);

            // jsonData �� SLList ������ Parsing ����.
            SLList slList = JsonUtility.FromJson<SLList>(jsonData);
            
            // slList.data �� ������ŭ ������ �̿��ؼ� ������Ʈ�� ������.
            for(int i = 0; i < slList.data.Count; i++)
            {
                // type ���� ���ӿ�����Ʈ ������.
                GameObject go = GameObject.CreatePrimitive((PrimitiveType)slList.data[i].type);
                // ������� ������Ʈ�� ��ġ�� ����
                go.transform.position = slList.data[i].pos;
                // ������� ������Ʈ�� ȸ���� ����
                go.transform.rotation = slList.data[i].rot;
                // ������� ������Ʈ�� ũ�⸦ ����
                go.transform.localScale = slList.data[i].scale;
            }
        }
    }
}
