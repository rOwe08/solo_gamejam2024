using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SimulationManager : MonoBehaviour
{
    public Human man;       // Мужчина, выбранный игроком
    public Human woman;     // Женщина, выбранная игроком

    public Image epochImage;  // UI-элемент для отображения картинок эпохи
    public Text eventLogText; // Текст для отображения 5 ключевых событий

    // Массивы спрайтов для каждой эпохи (успех и провал)
    public Sprite[] successEpochSprites; // Успешные эпохи
    public Sprite[] failEpochSprites;    // Проваленные эпохи

    private int currentEpoch = 0;  // Индикатор текущей эпохи
    private int totalEpochs = 7;   // Всего 7 эпох

    public List<Era> eras;

    void Start()
    {
        StartSimulation();
    }

    // Начало симуляции
    public void StartSimulation()
    {
        currentEpoch = 0;
        ShowNextEpoch();
    }

    // Показ следующей эпохи
    public void ShowNextEpoch()
    {
        if (currentEpoch < totalEpochs)
        {
            // Вычисляем общий рейтинг обоих персонажей
            float manRating = man.GetOverallRating();
            float womanRating = woman.GetOverallRating();
            float averageRating = (manRating + womanRating) / 2;

            // Если эпоха не первая или последняя, проверяем выживание
            if (currentEpoch != 0 && currentEpoch != totalEpochs - 1)
            {
                bool civilizationSurvived = DidCivilizationSurvive(averageRating);

                // Меняем спрайт в зависимости от того, выжила ли цивилизация
                epochImage.sprite = civilizationSurvived ? successEpochSprites[currentEpoch] : failEpochSprites[currentEpoch];

                // Показываем события эпохи
                ShowEpochEvents(civilizationSurvived);
            }
            else
            {
                // Первая эпоха (начало) и последняя эпоха (победа)
                epochImage.sprite = successEpochSprites[currentEpoch];
                ShowEpochEvents(true);  // Первая и последняя эпоха всегда успешны
            }

            // Переходим к следующей эпохе после задержки
            StartCoroutine(WaitAndProceedToNextEpoch());
        }
        else
        {
            // Конец симуляции
            EndSimulation();
        }
    }

    // Проверка, выжила ли цивилизация на основе рейтинга
    private bool DidCivilizationSurvive(float averageRating)
    {
        // Пример условия выживания: если средний рейтинг выше 5, цивилизация выживает
        return averageRating >= 5f;
    }

    // Показ 5 ключевых событий эпохи
    private void ShowEpochEvents(bool survived)
    {
        eventLogText.text = "";  // Очищаем текст

        // Пример событий: 5 случайных событий для каждой эпохи
        for (int i = 1; i <= 5; i++)
        {
            if (survived)
            {
                eventLogText.text += $"Событие {i}: Цивилизация преодолела это испытание.\n";
            }
            else
            {
                eventLogText.text += $"Событие {i}: Цивилизация потерпела неудачу.\n";
            }
        }
    }

    // Переход к следующей эпохе с задержкой
    private IEnumerator WaitAndProceedToNextEpoch()
    {
        yield return new WaitForSeconds(3f);  // Задержка перед сменой эпохи
        currentEpoch++;
        ShowNextEpoch();
    }

    // Конец симуляции
    private void EndSimulation()
    {
        eventLogText.text = "Конец симуляции. Цивилизация достигла своей конечной эпохи!";
        // Здесь можно добавить логику для окончания игры или возврата в главное меню
    }
}
