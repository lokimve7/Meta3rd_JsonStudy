using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 구조체 or 클래스
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
        #region 단일 정보 - 구조체 -> Json, Json -> 구조체
        // 나의 정보
        UserInfo myInfo = new UserInfo();
        myInfo.name = "김현진";
        myInfo.age = 28;
        myInfo.height = 180.5f;
        myInfo.interest = new string[] { "게임", "치킨", "키보드" };

        //구조체 or 클래스 데이터를 Json 으로 바꾸자.
        string jsonMyInfo = JsonUtility.ToJson(myInfo, true);      
        print(jsonMyInfo);

        // Jons 을 구조체 or 클래스 데이터로 바꾸자.
        UserInfo receiveInfo = JsonUtility.FromJson<UserInfo>(jsonMyInfo);
        print(receiveInfo.name);
        print(receiveInfo.age);
        print(receiveInfo.height);
        for(int i = 0; i < receiveInfo.interest.Length; i++)
        {
            print(receiveInfo.interest[i]);
        }

        #endregion

        #region 다수의 정보

        // 모든 UserInfo 를 담을 변수
        List<UserInfo> allUser = new List<UserInfo>();

        UserInfo info = new UserInfo();
        info.name = "메타버스 01";
        info.gender = true;
        info.age = 22;
        info.interest = new string[] {"독서", "게임", "운동"};

        // 정보 추가
        allUser.Add(info);

        info = new UserInfo();
        info.name = "메타버스 02";
        info.gender = false;
        info.age = 40;
        info.interest = new string[] { "게임", "피아노", "격투기" };

        // 정보 추가
        allUser.Add(info);

        info = new UserInfo();
        info.name = "메타버스 03";
        info.gender = true;
        info.age = 60;
        info.interest = new string[] { "컴퓨터", "콘솔", "자동차" };

        // 정보 추가
        allUser.Add(info);

        // allUser 를 담을 수 있는 AllUserInfo 를 하나 만들어서 셋팅
        AllUserInfo allUserInfo = new AllUserInfo();
        allUserInfo.data = allUser;


        // 배열(리스트) 데이터를 Json 으로 바꾸자.
        string jsonAllUser = JsonUtility.ToJson(allUserInfo, true);
        print(jsonAllUser);

        // Json 을 배열(리스트) 데이터로 바꾸자.
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
	    "name" : "김현진",
	    "age" : 28,
	    "height" : 180.5,
	    "address" : "서울시 관악구",
	    "gender" : true,
	    "interest" : ["게임", "치킨(닭가슴살)", "키보드"],
	    "아내정보" : { "name" : "강한나", "age" :  26}
    },

     {
	    "name" : "김현진",
	    "age" : 28,
	    "height" : 180.5,
	    "address" : "서울시 관악구",
	    "gender" : true,
	    "interest" : ["게임", "치킨(닭가슴살)", "키보드"],
	    "아내정보" : { "name" : "강한나", "age" :  26}
    },

     {
	    "name" : "김현진",
	    "age" : 28,
	    "height" : 180.5,
	    "address" : "서울시 관악구",
	    "gender" : true,
	    "interest" : ["게임", "치킨(닭가슴살)", "키보드"],
	    "아내정보" : { "name" : "강한나", "age" :  26}
    }

]
}
*/