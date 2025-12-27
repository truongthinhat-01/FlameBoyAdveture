// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class GameManager : BaseManager<GameManager>
// {
//     public GameState currentState;

//     protected override void Awake()
//     {
//         base.Awake();
//         DontDestroyOnLoad(gameObject);
//     }

//     void Start()
//     {
//         SetState(GameState.Menu);
//     }

//     public void SetState(GameState newState)
//     {
//         currentState = newState;

//         switch (currentState)
//         {
//             case GameState.Menu:
//                 UIManager.Instance.ShowMenu();
//                 break;

//             case GameState.SelectMap:
//                 UIManager.Instance.ShowMapSelect();
//                 break;

//             case GameState.Setting:
//                 UIManager.Instance.ShowSetting();
//                 break;

//             case GameState.Playing:
//                 UIManager.Instance.ShowHUD();
//                 break;
//         }
//     }

//     public void LoadMap(string sceneName)
//     {
//         SceneManager.LoadScene(sceneName);
//         SetState(GameState.Playing);
//     }
// }
