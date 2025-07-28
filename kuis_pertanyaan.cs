using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class questions : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        [TextArea] public string questionText;
        public string[] answers;
        public int correctAnswerIndex;
        [TextArea] public string correctReferenceText;
        [TextArea] public string wrongReferenceText;
        [TextArea] public string answerLabelText = "Answer:"; 
    }

    public Text questionText; 
    public Text referenceText; 
    public Text answerLabel; 
    public Button[] answerButtons; 
    public List<Question> questions; 
    public GameObject congratulationPanel; 
    public Button backToMenuButton; 

    private int currentQuestionIndex;
    private List<int> shuffledIndices; 

    void Start()
    {
        currentQuestionIndex = 0;
        ShowQuestion();
        referenceText.gameObject.SetActive(false); 
        if (congratulationPanel != null)
        {
            congratulationPanel.SetActive(false); 
        }

        if (backToMenuButton != null)
        {
            backToMenuButton.onClick.AddListener(GotoMainMenu); 
        }
    }

    public void ShowQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            Question currentQuestion = questions[currentQuestionIndex];
            questionText.text = currentQuestion.questionText;

            if (answerLabel != null)
            {
                answerLabel.text = currentQuestion.answerLabelText; 
            }

            shuffledIndices = ShuffleAnswers(currentQuestion.answers.Length);

            for (int i = 0; i < answerButtons.Length; i++)
            {
                if (i < currentQuestion.answers.Length)
                {
                    answerButtons[i].gameObject.SetActive(true);
                    int shuffledIndex = shuffledIndices[i];
                    answerButtons[i].GetComponentInChildren<Text>().text = currentQuestion.answers[shuffledIndex];

                    answerButtons[i].onClick.RemoveAllListeners();
                    answerButtons[i].onClick.AddListener(() => OnAnswerSelected(shuffledIndex));
                }
                else
                {
                    answerButtons[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            ShowCongratulationPopup();
        }
    }

    private List<int> ShuffleAnswers(int length)
    {
        List<int> indices = new List<int>();
        for (int i = 0; i < length; i++) indices.Add(i);

        for (int i = 0; i < indices.Count; i++)
        {
            int randomIndex = Random.Range(i, indices.Count);
            int temp = indices[i];
            indices[i] = indices[randomIndex];
            indices[randomIndex] = temp;
        }

        return indices;
    }

    public void OnAnswerSelected(int index)
    {
        Question currentQuestion = questions[currentQuestionIndex];
        if (index == currentQuestion.correctAnswerIndex)
        {
            Debug.Log("Jawaban benar!");
            referenceText.text = currentQuestion.correctReferenceText;
            referenceText.gameObject.SetActive(true); 
            StartCoroutine(HideReferenceTextAfterDelay(1f)); 
            StartCoroutine(NextQuestionWithDelay(2f));
        }
        else
        {
            Debug.Log("Jawaban salah!");
            referenceText.text = currentQuestion.wrongReferenceText;
            referenceText.gameObject.SetActive(true); 
            StartCoroutine(HideReferenceTextAfterDelay(1f)); 
            StartCoroutine(ResetGameWithDelay(2f)); 
        }
    }

    private IEnumerator HideReferenceTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        referenceText.gameObject.SetActive(false); 
    }

    private IEnumerator NextQuestionWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentQuestionIndex++;
        ShowQuestion();
    }

    private IEnumerator ResetGameWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetGame();
    }

    private void ResetGame()
    {
        Debug.Log("Permainan di-reset.");
        currentQuestionIndex = 0; 
        referenceText.gameObject.SetActive(false); 
        ShowQuestion(); 
    }

    private void ShowCongratulationPopup()
    {
        Debug.Log("Semua pertanyaan selesai. Selamat!");
        if (congratulationPanel != null)
        {
            congratulationPanel.SetActive(true); 
        }

        StartCoroutine(GotoMainMenuAfterDelay(5f));
    }

    public void GotoMainMenu()
    {
        Debug.Log("Kembali ke Main Menu...");
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator GotoMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GotoMainMenu();
    }
}
