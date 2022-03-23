using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace GIKIT
{
    public enum UIScreenType {LoadingScreen, GameScreen, ManagerScreen, MenuScreen, QuestScreen, AllQuestsScreen, SupercashScreen, UpgradeScreen, WelcomeScreen,ChallengesScreen, IncomeBoostScreen, DailyFreeSpinner, DoubleCashScreen, StockMarketScreen }
    public enum TransitionPosition {Left, Right, Top, Bottom, Custom }

    public  class UIManager : MonoBehaviour
    {
        [SerializeField] UIScreenType startScreen = UIScreenType.GameScreen;
        [SerializeField] int mainMenuIndex = 0;
        [SerializeField] RectTransform left, right, top, bottom;

        static UIManager  instance = null;
        public static UIManager Instance { get { return instance; } }
        public int MainMenuIndex { get { return mainMenuIndex; } }

        Dictionary<UIScreenType, UIScreen> screens = new Dictionary<UIScreenType, UIScreen>();
        UIScreen currentScreen = null;
        UIScreen previousScreen = null;

        /// <summary>
        /// Helper Functions below
        /// </summary>

        private void Awake()
        {
            if (instance == null) { instance = this; }
            else if (instance!=this)
            {
                Destroy(gameObject);
            }
           
            DontDestroyOnLoad(this);

            GetAllChildScreens();
            screens.TryGetValue(startScreen, out currentScreen);
            SwitchScreen(currentScreen);

        }


        private void GetAllChildScreens()
        {
            UIScreen[] allScreens = GetComponentsInChildren<UIScreen>();
            foreach (UIScreen item in allScreens)
            {
                if (!screens.ContainsKey(item.Type))
                {
                    screens.Add(item.Type, item);

                    //turning off all screens by default
                    //item.gameObject.SetActive(false);
                }

               
            }
        }

        // This function is used via other scripts
        public void SwitchScreen(UIScreenType screenType, bool disableTransition = false)
        {
            if (screens.ContainsKey(screenType))
            {
                UIScreen newScreen = screens[screenType];

                previousScreen = currentScreen;
                currentScreen = newScreen;

                // checking whether to use out ease or not
                DoTransition(previousScreen, newScreen);
                Debug.Log("Switching Screen");


            }
            else
            {
                Debug.Log("Screen Does Not Exist");
            }
        }
               

        //this function is used for editor
        public void SwitchScreen(UIScreen newScreen)
        {
            if (newScreen)
            {
                // saving previous screen for back functionality

                previousScreen = currentScreen;
                currentScreen = newScreen;
                

                // checking whether to use out ease or not
                DoTransition(previousScreen,newScreen);


            }
            else
            {
                Debug.Log("No Target Screen");
            }
        }

        private void DoTransition(UIScreen previousScreen, UIScreen currentScreen)
        {
            Ease outEase = Ease.OutExpo;

            if (previousScreen.UseOutEase)
            {
                outEase = previousScreen.OutEase;
            }

            if (previousScreen)
            {
                previousScreen.Close();
                previousScreen.GetComponent<RectTransform>().DOLocalMove(previousScreen.EndPosition, previousScreen.OutTransitionTime).SetEase(outEase);
            }

            if (currentScreen)
            {
                currentScreen.Show();
                currentScreen.GetComponent<RectTransform>().DOLocalMove(Vector3.zero, currentScreen.InTransitionDuration).SetEase(currentScreen.InEase);
                //currentScreen.transform.SetAsLastSibling();
            }

            
           




        }

     



        public void Back()
        {
            if (previousScreen != null)
            {
                SwitchScreen(previousScreen);
            }


        }

        public UIScreen GetScreen(UIScreenType screenType)
        {
            UIScreen screen = null;
            screens.TryGetValue(screenType,out screen);
            return screen;
        }

        public Vector3 GetPosition(TransitionPosition pos)
        {
            Vector3 position = Vector3.zero;

            switch (pos)
            {
                case TransitionPosition.Left:
                    position = left.localPosition;
                    break;
                case TransitionPosition.Right:
                    position = right.localPosition;
                    break;
                case TransitionPosition.Top:
                    position = top.localPosition;
                    break;
                case TransitionPosition.Bottom:
                    position = bottom.localPosition;
                    break;
                case TransitionPosition.Custom:
                    break;
                
            }

            return position;
        }


    }
}

