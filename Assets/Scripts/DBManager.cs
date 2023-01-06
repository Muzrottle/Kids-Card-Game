using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class DBManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

    public GameController gameController;
    public GameObject scoreElement;
    public Transform scoreboardContent;

    void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });

    }

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        StartCoroutine(CheckLevel());
    }

    public void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public IEnumerator CheckLevel()
    {

        var DBTask = DBreference.Child("Levels").Child("-NKPkGCc0WGM4MuZ3xrC").Child(SceneManager.GetActiveScene().name).GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);


        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Levels Get
            Debug.Log("3");

            DataSnapshot level = DBTask.Result;

            Debug.Log(level.Exists);

            if (level.Exists)
            {
                Debug.Log("4");
                
                Dictionary<string, object> Levels = new Dictionary<string, object>();
                Levels["level"] = SceneManager.GetActiveScene().name;

                string key = DBreference.Push().Key;

                DBreference.Child("Levels").Child(key).UpdateChildrenAsync(Levels);

                //var DBtask = DBreference.Child("Levels").Child(SceneManager.GetActiveScene().name).Push();
            }
        }
    }

    public void SaveScore()
    {
        string username = auth.CurrentUser.DisplayName;
        //string level = SceneManager.GetActiveScene().name;
        //int scoreAmount = gameController.score;

        StartCoroutine(UpdateUsernameAuth(username));
        StartCoroutine(UpdateUsernameDatabase(username));

        StartCoroutine(UpdateLevel(SceneManager.GetActiveScene().name));
        StartCoroutine(UpdateScore(gameController.score));

        //Dictionary<string, object> score = new Dictionary<string, object>();
        //score["username"] = username;
        //score["score"] = scoreAmount;
        //score["level"] = level;

        //Debug.Log(username + " " + level + " " + scoreAmount);

        //string key = DBreference.Push().Key;

        //DBreference.Child(key).UpdateChildrenAsync(score);
    }

    private IEnumerator UpdateUsernameAuth(string _username)
    {
        //Create a user profile and set the username
        UserProfile profile = new UserProfile { DisplayName = _username };
        User = auth.CurrentUser;
        //Call the Firebase auth update user profile function passing the profile with the username
        var ProfileTask = User.UpdateUserProfileAsync(profile);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        if (ProfileTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
        }
        else
        {
            //Auth username is now updated
        }
    }

    private IEnumerator UpdateUsernameDatabase(string _username)
    {
        //Set the currently logged in user username in the database
        var DBTask = DBreference.Child("Users").Child(User.UserId).Child("username").SetValueAsync(_username);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }

    private IEnumerator UpdateLevel(string _level)
    {
        //Set the currently logged in user xp
        var DBTask = DBreference.Child("Scores").Child(User.UserId).Child("level").SetValueAsync(_level);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Level is now updated
        }
    }

    private IEnumerator UpdateScore(int _score)
    {
        //Set the currently logged in user kills
        var DBTask = DBreference.Child("Scores").Child(User.UserId).Child("score").SetValueAsync(_score);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Kills are now updated
        }
    }

    public IEnumerator LoadScoreboardData()
    {
        //Get all the users data ordered by kills amount
        var DBTask = DBreference.Child("Scores").OrderByChild("score").GetValueAsync();
        var DBTaskUsers = DBreference.Child("Users").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
        Debug.Log("Part 1");

        yield return new WaitUntil(predicate: () => DBTaskUsers.IsCompleted);
        Debug.Log("Part 2");


        if (DBTask.Exception != null && DBTaskUsers.Exception != null)
        {
            Debug.Log("Part 3");

            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            Debug.LogWarning(message: $"Failed to register user task with {DBTaskUsers.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            DataSnapshot snapshotUsers = DBTaskUsers.Result;
            Debug.Log("Part 4");


            //Destroy any existing scoreboard elements
            foreach (Transform child in scoreboardContent.transform)
            {
                Destroy(child.gameObject);
            }

            //Loop through every users UID
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                //string username = childSnapshot.Child("username").Value.ToString();
                Debug.Log("Part 5");

                string UID = childSnapshot.Key.ToString();
                Debug.Log(UID);

                string username = snapshotUsers.Child(UID).Child("username").Value.ToString();
                int score = int.Parse(childSnapshot.Child("score").Value.ToString());
                //int deaths = int.Parse(childSnapshot.Child("deaths").Value.ToString());
                //int xp = int.Parse(childSnapshot.Child("xp").Value.ToString());
                Debug.Log("Part 6");

                Debug.Log(username + "       " + score);
                Debug.Log("Part 7");

                //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(scoreElement, scoreboardContent);
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(username, score);
            }

            ////Go to scoareboard screen
            //UIManager.instance.ScoreboardScreen();
        }
    }

    //private IEnumerator Initilization()
    //{
    //    var task = FirebaseApp.CheckAndFixDependenciesAsync();
    //    while (!task.IsCompleted)
    //    {
    //        yield return null;
    //    }

    //    if (task.IsCanceled || task.IsFaulted)
    //    {
    //        Debug.LogError("Database Error: " + task.Exception);
    //    }

    //    var dependencyStatus = task.Result;

    //    if (dependencyStatus == DependencyStatus.Available)
    //    {
    //        usersRef = FirebaseDatabase.DefaultInstance.GetReference("Users");
    //        scoresRef = FirebaseDatabase.DefaultInstance.GetReference("Scores");
    //        Debug.Log("init Completed");
    //    }
    //    else
    //    {
    //        Debug.LogError("Database error: ");
    //    }
    //}

    //public void SaveUser()
    //{
    //    string username = usernameInput.text;
    //    string password = passwordInput.text;

    //    Dictionary<string, object> user = new Dictionary<string, object>();
    //    user["username"] = username;
    //    user["password"] = password;

    //    string key = usersRef.Push().Key;

    //    usersRef.Child(key).UpdateChildrenAsync(user);
    //}

    //public void SaveScore()
    //{
    //    string username = auth.CurrentUser.DisplayName;
    //    string level = SceneManager.GetActiveScene().name;
    //    string scoreAmount = gameController.score.ToString();

    //    Dictionary<string, object> score = new Dictionary<string, object>();
    //    score["username"] = username;
    //    score["score"] = scoreAmount;
    //    score["level"] = level;

    //    string key = DBReference.Push().Key;

    //    DBReference.Child(key).UpdateChildrenAsync(score);
    //}

    //public void GetScore()
    //{
    //    StartCoroutine(GetScoreData());
    //}

    //public IEnumerator GetScoreData()
    //{
    //    string level = SceneManager.GetActiveScene().name;
    //    var task = DBReference.Child(level).GetValueAsync();

    //    while (!task.IsCompleted)
    //    {
    //        yield return null;
    //    }

    //    if (task.IsCanceled || task.IsFaulted)
    //    {
    //        Debug.LogError("Database Error: " + task.Exception);
    //        yield break;
    //    }

    //    string scores = "";

    //    DataSnapshot snapshot = task.Result;

    //    foreach (DataSnapshot score in snapshot.Children)
    //    {
    //        //scores += score.Child(level) + score.Child(username );
    //    }
    //}

    //public void GetData()
    //{
    //    StartCoroutine(GetUserData());
    //}

    //public IEnumerator GetUserData()
    //{
    //    string name = usernameInput.text;
    //    var task = usersRef.Child(name).GetValueAsync();

    //    while (!task.IsCompleted)
    //    {
    //        yield return null;
    //    }

    //    if (task.IsCanceled || task.IsFaulted)
    //    {
    //        Debug.LogError("Database Error: " + task.Exception);
    //        yield break;
    //    }

    //    DataSnapshot snapshot = task.Result;

    //    foreach (DataSnapshot user in snapshot.Children)
    //    {
    //        if (user.Key == "password")
    //        {
    //            Debug.Log("Password: " + user.Value.ToString());
    //        }

    //        if (user.Key == "username")
    //        {
    //            Debug.Log("Username: " + user.Value.ToString());
    //        }
    //    }
    //}

    // Update is called once per frame
}
