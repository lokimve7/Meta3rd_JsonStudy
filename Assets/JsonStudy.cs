using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ü or Ŭ����
[System.Serializable]
public struct UserInfo
{
    public string name;
    public int age;
    public float height;
    public bool gender;
    public string[] interest;
}

[System.Serializable]
public struct AllUserInfo
{
    public List<UserInfo> data;
}

public class JsonStudy : MonoBehaviour
{
    void Start()
    {
        #region ���� ���� - ����ü -> Json, Json -> ����ü
        // ���� ����
        UserInfo myInfo = new UserInfo();
        myInfo.name = "������";
        myInfo.age = 28;
        myInfo.height = 180.5f;
        myInfo.interest = new string[] { "����", "ġŲ", "Ű����" };

        //����ü or Ŭ���� �����͸� Json ���� �ٲ���.
        string jsonMyInfo = JsonUtility.ToJson(myInfo, true);      
        print(jsonMyInfo);

        // Jons �� ����ü or Ŭ���� �����ͷ� �ٲ���.
        UserInfo receiveInfo = JsonUtility.FromJson<UserInfo>(jsonMyInfo);
        print(receiveInfo.name);
        print(receiveInfo.age);
        print(receiveInfo.height);
        for(int i = 0; i < receiveInfo.interest.Length; i++)
        {
            print(receiveInfo.interest[i]);
        }

        #endregion

        #region �ټ��� ����

        // ��� UserInfo �� ���� ����
        List<UserInfo> allUser = new List<UserInfo>();

        UserInfo info = new UserInfo();
        info.name = "��Ÿ���� 01";
        info.gender = true;
        info.age = 22;
        info.interest = new string[] {"����", "����", "�"};

        // ���� �߰�
        allUser.Add(info);

        info = new UserInfo();
        info.name = "��Ÿ���� 02";
        info.gender = false;
        info.age = 40;
        info.interest = new string[] { "����", "�ǾƳ�", "������" };

        // ���� �߰�
        allUser.Add(info);

        info = new UserInfo();
        info.name = "��Ÿ���� 03";
        info.gender = true;
        info.age = 60;
        info.interest = new string[] { "��ǻ��", "�ܼ�", "�ڵ���" };

        // ���� �߰�
        allUser.Add(info);

        // allUser �� ���� �� �ִ� AllUserInfo �� �ϳ� ���� ����
        AllUserInfo allUserInfo = new AllUserInfo();
        allUserInfo.data = allUser;


        // �迭(����Ʈ) �����͸� Json ���� �ٲ���.
        string jsonAllUser = JsonUtility.ToJson(allUserInfo, true);
        print(jsonAllUser);

        // Json �� �迭(����Ʈ) �����ͷ� �ٲ���.
        AllUserInfo receiveAllUserInfo = JsonUtility.FromJson<AllUserInfo>(jsonAllUser);
       
        #endregion
    }

    void Update()
    {
        
    }
}

/*
 {
"data" : [
    {
	    "name" : "������",
	    "age" : 28,
	    "height" : 180.5,
	    "address" : "����� ���Ǳ�",
	    "gender" : true,
	    "interest" : ["����", "ġŲ(�߰�����)", "Ű����"],
	    "�Ƴ�����" : { "name" : "���ѳ�", "age" :  26}
    },

     {
	    "name" : "������",
	    "age" : 28,
	    "height" : 180.5,
	    "address" : "����� ���Ǳ�",
	    "gender" : true,
	    "interest" : ["����", "ġŲ(�߰�����)", "Ű����"],
	    "�Ƴ�����" : { "name" : "���ѳ�", "age" :  26}
    },

     {
	    "name" : "������",
	    "age" : 28,
	    "height" : 180.5,
	    "address" : "����� ���Ǳ�",
	    "gender" : true,
	    "interest" : ["����", "ġŲ(�߰�����)", "Ű����"],
	    "�Ƴ�����" : { "name" : "���ѳ�", "age" :  26}
    }

]
}
*/