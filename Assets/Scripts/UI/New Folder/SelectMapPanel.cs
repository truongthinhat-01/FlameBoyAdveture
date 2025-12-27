using UnityEngine;

public class SelectMapPanel : MonoBehaviour
{
    string selectedMap;

    public void SelectMap(string mapName)
    {
        selectedMap = mapName;
    }

    public void Play()
    {
        // if (!string.IsNullOrEmpty(selectedMap))
        //     UIManager.Instance.LoadScene(selectedMap);
        if (string.IsNullOrEmpty(selectedMap)) return;
    if (!UIManager.HasInstance) return;

    UIManager.Instance.ShowLoading();

    // Gửi tên map sang UIManager
    UIManager.Instance.LoadSelectedMap(selectedMap);
    }

    public void Back()
    {
        UIManager.Instance.ShowMenu();
    }
}
