using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ButtonScaler : MonoBehaviour
{
    public float scaleMultiplier = 1.5f;   // Множитель для увеличения кнопки
    public float scaleDuration = 0.2f;     // Время анимации увеличения
    public float disappearDuration = 0.5f; // Время до исчезновения кнопки после увеличения
    public float rotationDuration = 0.8f;  // Время анимации вращения
    public int numberOfRotations = 3;      // Количество полных оборотов пропеллера
    public TextMeshProUGUI buttonText;     // Ссылка на текст кнопки
    public IntroductionManager introductionManager; // Ссылка на IntroductionManager

    private RectTransform buttonRectTransform;
    private bool isClicked = false;

    void Start()
    {
        // Получаем компонент RectTransform для кнопки
        buttonRectTransform = GetComponent<RectTransform>();

        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        if (isClicked)
            return; // Если уже нажали, ничего не делаем

        isClicked = true;

        // Запускаем анимацию вращения, увеличения кнопки и исчезновения текста
        RotateAndScaleButtonWithTextFade();
    }

    private void RotateAndScaleButtonWithTextFade()
    {
        // Устанавливаем начальный масштаб кнопки и тексту полную непрозрачность
        buttonRectTransform.localScale = Vector3.one;
        buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, 1f);

        // Рассчитываем угол поворота для полного количества оборотов
        float totalRotation = 360f * numberOfRotations;

        // Анимация вращения с плавным замедлением
        buttonRectTransform.DORotate(Vector3.forward * totalRotation, rotationDuration, RotateMode.LocalAxisAdd)
            .SetEase(Ease.OutQuad) // Плавное замедление при остановке
            .OnComplete(() =>
            {
                // После вращения запускаем анимацию увеличения кнопки
                buttonRectTransform.DOScale(scaleMultiplier, scaleDuration).SetEase(Ease.Linear);

                // Анимируем исчезновение текста
                buttonText.DOFade(0f, scaleDuration).SetEase(Ease.Linear);

                // После анимации увеличения кнопки и исчезновения текста, вызываем StartIntroduction
                DOVirtual.DelayedCall(scaleDuration + disappearDuration, () =>
                {
                    // Запускаем функцию StartIntroduction в IntroductionManager
                    if (introductionManager != null)
                    {
                        introductionManager.StartIntroduction();
                    }
                    Destroy(gameObject); // Удаляем кнопку после окончания
                });
            });
    }
}
