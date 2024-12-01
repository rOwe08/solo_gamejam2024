using System.Collections;
using UnityEngine;
using TMPro;

public class TitleTypingEffect : MonoBehaviour
{
    public TextMeshProUGUI titleText;  // Ссылка на компонент TextMeshProUGUI
    public string title = "ReGenesis...";  // Текст, который будет выводиться
    public float typingSpeed = 0.05f;  // Скорость набора текста
    public float cursorBlinkSpeed = 0.5f;  // Скорость мигания курсора
    public float eraseSpeed = 0.05f;  // Скорость обратного стирания текста

    public string displayedText = "";  // Текущий отображаемый текст
    private bool isCursorVisible = false;  // Флаг видимости курсора
    private string cursor = "|";  // Символ курсора

    void Start()
    {
        // Запуск набора текста при старте
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        // Печатаем текст символ за символом
        for (int i = 0; i < title.Length; i++)
        {
            displayedText += title[i];  // Добавляем следующий символ
            titleText.text = displayedText + cursor;  // Обновляем отображаемый текст с курсором
            yield return new WaitForSeconds(typingSpeed);  // Задержка между символами
        }

        // После набора текста начинаем мигание курсора
        StartCoroutine(BlinkCursor());
    }

    private IEnumerator BlinkCursor()
    {
        while (true)
        {
            // Добавляем или убираем курсор из текста
            if (isCursorVisible)
            {
                titleText.text = displayedText + cursor;  // Текст с курсором
            }
            else
            {
                titleText.text = displayedText;  // Текст без курсора
            }

            isCursorVisible = !isCursorVisible;  // Меняем видимость курсора
            yield return new WaitForSeconds(cursorBlinkSpeed);  // Задержка для мигания
        }
    }

    // Метод для обратного стирания текста
    public IEnumerator EraseText()
    {
        // Стираем текст символ за символом, включая курсор
        while (displayedText.Length > 0)
        {
            displayedText = displayedText.Substring(0, displayedText.Length - 1);  // Убираем последний символ
            titleText.text = displayedText;  // Обновляем текст на экране без курсора
            yield return new WaitForSeconds(eraseSpeed);  // Задержка между стиранием символов
        }

        // Убираем курсор после стирания
        titleText.text = displayedText;  // Обновляем текст, в котором курсора нет

        // Уничтожаем объект после стирания
        Destroy(gameObject);
    }
}
