using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using System.Collections;
using CI.QuickSave;

public class UIManager : MonoBehaviour
{
    public Button btnResume;
    public Button btnMenu;
    public Button btnShop;
    public Button btnCloseShop;
    public Button btnlogout;
    public GameObject menu;
    public GameObject shop;
    public GameObject blocker;
    public Text coin;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
        shop.SetActive(false);
        blocker.SetActive(false);
        btnMenu.onClick.AddListener(showMenu);
        btnResume.onClick.AddListener(resume);
        btnlogout.onClick.AddListener(logout);
        btnShop.onClick.AddListener(ShowShop);
        btnCloseShop.onClick.AddListener(CloseShop);

        FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
        if (user == null)
        {
            SceneManager.LoadScene("LoginScene");
        }
        StartCoroutine(refCoin());
        /*  QuickSaveWriter.Create("myFarmStables").Write("Chickens", 0).Commit();
         QuickSaveWriter.Create("myFarmWarehouse")
            .Write("Gold", 800)
            .Write("Carrot", 3)
            .Write("Cabbage", 2)
            .Write("Radish", 4)
            .Write("Strawberry", 1)
            .Write("Corn", 9)
            .Write("Eggs", 5)
            .Commit();*/
        if (!QuickSaveReader.Create("myFarmWarehouse").Exists("Gold"))
        {
            QuickSaveWriter.Create("myFarmWarehouse")
            .Write("Gold", 800)
            .Write("Carrot", 0)
            .Write("Cabbage", 0)
            .Write("Radish", 0)
            .Write("Strawberry", 0)
            .Write("Corn", 0)
            .Write("Eggs", 0)
            .Commit();
        }
    }

    IEnumerator refCoin()
    {
        while (true)
        {
            if (QuickSaveReader.Create("myFarmWarehouse").Exists("Gold"))
                coin.text = QuickSaveReader.Create("myFarmWarehouse").Read<int>("Gold").ToString();
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }

    void showMenu()
    {
        menu.SetActive(true);
        blocker.SetActive(true);
    }

    void ShowShop()
    {
        shop.SetActive(true);
        blocker.SetActive(true);
    }

    void resume()
    {
        menu.SetActive(false);
        blocker.SetActive(false);
    }

    void CloseShop()
    {
        shop.SetActive(false);
        blocker.SetActive(false);
    }

    void logout()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            FirebaseAuth.DefaultInstance.SignOut();
            SceneManager.LoadScene("LoginScene");
        }
    }
}
