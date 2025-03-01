using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginMng : MonoBehaviour
{
    [SerializeField] TMP_InputField inputID;
    [SerializeField] TMP_InputField inputPassword;
    [SerializeField] TMP_InputField inputUserName;
    [SerializeField] TextMeshProUGUI statusText;

    [SerializeField] GameObject userNamePanel;
    [SerializeField] TextMeshProUGUI nameStatusText;

    private const string USERKEY = "USER_";
    private const string USERRPASSWORDKEY = "_PASSWORD";

    public void ClickLogin()
    {
        string userID = inputID.text.Trim();
        string userPassword = inputPassword.text.Trim();

        if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(userPassword))
        {
            SetstatusText("���̵�� ��й�ȣ�� ��� �Է����ּ���", Color.red);
            return;
        }

        Login(userID, userPassword);
    }

    private void Login(string _id, string _password)
    {
        string savedID = PlayerPrefs.GetString(USERKEY + _id, "");
        string savedPassword = PlayerPrefs.GetString(USERKEY + _id + USERRPASSWORDKEY, "");

        if (savedID == _id && savedPassword == _password)
        {
            SetstatusText("���� ���� ��...", Color.white);

            UserData loadData = DataMng.instance.UserDataLoader.LoadUserData(_id);

            if (loadData.first)
            {
                userNamePanel.SetActive(true);
            }
            else
            {
                SceneMng.Instance.ChangeScene(EScene.LOBBY);
            }
        }
        else
        {
            SetstatusText("���̵� �Ǵ� ��й�ȣ�� �ùٸ��� �ʽ��ϴ�", Color.red);
        }
    }

    public void ClickRegister()
    {
        string userID = inputID.text.Trim();
        string userPassword = inputPassword.text.Trim();

        if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(userPassword))
        {
            SetstatusText("���̵�� ��й�ȣ�� ��� �Է����ּ���", Color.red);
            return;
        }

        if (PlayerPrefs.HasKey(USERKEY + userID))
        {
            SetstatusText("�̹� �����ϴ� ���̵��Դϴ�. �ٸ� ���̵� ������ּ���.", Color.red);
            return;
        }

        if (inputID.text.Length < 1) return;

        PlayerPrefs.SetString(USERKEY + userID, userID);
        PlayerPrefs.SetString(USERKEY + userID + USERRPASSWORDKEY, userPassword);
        PlayerPrefs.Save();

        UserData newUserData = new UserData()
        {
            userID = userID,
            userName = "",
            userLevel = 1,
            curExp = 0,
            maxExp = 100,
            gold = 1000,
            gem = 50,
            paper = 10,
            first = true
        };

        DataMng.instance.UserDataLoader.curUserData = newUserData;
        DataMng.instance.UserDataLoader.SaveUserData();
        SetstatusText("ȸ�������� �Ϸ�Ǿ����ϴ�.", Color.white);
    }

    public void ClickRegisterCheck()
    {
        if (!string.IsNullOrEmpty(inputUserName.text) && inputUserName.text.Length > 1)
        {
            DataMng.instance.UserDataLoader.curUserData.userName = inputUserName.text;
            DataMng.instance.UserDataLoader.curUserData.first = false;
            SceneMng.Instance.ChangeScene(EScene.LOBBY);
            DataMng.instance.UserDataLoader.SaveUserData();
        }
        else
        {
            SetstatusText(nameStatusText.text, Color.red);
        }
    }

    public void ClickRegisterCancle()
    {
        userNamePanel.SetActive(false);
    }

    private void LoadUserData(string _id)
    {
        string userName = PlayerPrefs.GetString(USERKEY + _id, "Unknown");
    }

    private void SetstatusText(string _text, Color color)
    {
        statusText.text = _text;
        statusText.color = color;
    }
}
