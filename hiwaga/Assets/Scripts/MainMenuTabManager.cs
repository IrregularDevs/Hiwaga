using UnityEngine;
using UnityEngine.UI; // For Button component

public class MainMenuTabManager : MonoBehaviour
{
   public GameObject optionsTab; // Reference to the options tab GameObject
   public GameObject sureExitTab; // Reference to the sure exit tab GameObject
   public GameObject creditsTab; // Reference to the credits tab GameObject
   private GameObject currentOpenTab; // Currently open tab

   void Start() // Initialize and hide all tabs at start
   {
       // Ensure all tabs are hidden at the start
       if (optionsTab != null)
           optionsTab.SetActive(false);
       if (sureExitTab != null)
           sureExitTab.SetActive(false);
       if (creditsTab != null)
           creditsTab.SetActive(false);
   }

   private void OpenTab(GameObject tab) // Open one tab and close the previously open tab
   {
       if (tab == null)
           return;

       if (currentOpenTab != null && currentOpenTab != tab)
       {
           currentOpenTab.SetActive(false);
       }

       currentOpenTab = tab;
       currentOpenTab.SetActive(true);
   }

   public void CloseCurrentTab() // Close the current open tab
   {
       if (currentOpenTab == null)
           return;

       currentOpenTab.SetActive(false);
       currentOpenTab = null;
   }

   public void OnOptionsButtonClicked() // Open the options tab
   {
       OpenTab(optionsTab);
   }

   public void OnExitButtonClicked() // Open the sure exit tab
   {
       OpenTab(sureExitTab);
   }

   public void OnCreditsButtonClicked() // Open the credits tab
   {
       OpenTab(creditsTab);
   }

}
