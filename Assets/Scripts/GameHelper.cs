using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class GameHelper : MonoBehaviour {

    public Text timer_txt;
    public Text question_txt;

    public Image figure_img;

    public Figure[] figure;

    public Colors colors;

    public float timeInLevel; //Запланированное время
    public float time; //Оставшееся время
    public float addTime = 10f; //Время за верный ответ

    public bool answer; //Верный ли ответ
    int figureColor; //Цвет фигуры
    int figureType; //Тип фигуры

    public bool isGame;
    public bool isPause;

    public AnimationCurve curve;

    public UnityEvent createQuestionEvent;
    ////
    public Transform parent;
    public GameObject prefab;
    float CurveRandom()
    {
        return curve.Evaluate(Random.value);
    }
    private void Awake()
    {
        for (int i=0; i < figure.Length; i++)
        {
            figure[i].SetValue();
        }

        SetListener();
        StartGame();
    }
   
    void SetListener()
    {
        createQuestionEvent.AddListener(FigureColor);
        createQuestionEvent.AddListener(TextColor);
        createQuestionEvent.AddListener(FigureType);
    }
    
    public void StartGame()
    {
        time = timeInLevel;
        isGame = true;
    }
    private void FixedUpdate()
    {
        if (isPause || !isGame)
            return;
        time -= Time.fixedDeltaTime;


        timer_txt.text = System.Math.Round(time, 1) + " c" ;
    }

    public void ClickButton(bool _answer)
    {
        
        bool res = _answer == answer;
        if (res)
        {
            print("Yes");
            time += addTime;
        }
        else
        {
            print("No");
        }
        GameObject gm = Instantiate(prefab, parent) as GameObject;
        Image im = gm.GetComponent<Image>();
        im.color = res ? Color.green : Color.red;

        CreateQuestion();
    }
    
    void CreateQuestion()
    {
       
        float rand = CurveRandom(); //будет лт ответ не верным
        
        answer = rand >= 0.5f;

        createQuestionEvent.Invoke();
        CreateText(answer, figureColor, figureType);//Сформировать вопрос

    }

    
    void FigureColor() //Рандомный цвет фигуры
    {
        figureColor = colors.RandomColorId();
        figure_img.color = colors.colors[figureColor];

    }
    void TextColor() //Рандомй цвет текста
    {
        int idColor = colors.RandomColorId();
        question_txt.color = colors.colors[idColor];
    }
   
    void FigureType() //Рандомная фигура
    {
        figureType = Random.Range(0, figure.Length);
        figure_img.sprite = figure[figureType].sprite;
        
    }
    void CreateText(bool _answer, int _idColor, int idFigure) //Сформировать вопрос
    {
        
        string colorName = colors.GetColorName(_answer, _idColor);

        string figureName = figure[idFigure].Question(_answer);

        string text = "Это " + colorName + " " + figureName + " ?";
        question_txt.text = text;
    }
}

[System.Serializable]
public class Colors
{
    public Color[] colors =
     {
        Color.black,
        Color.blue,
        Color.gray,
        Color.green,
        Color.magenta,
        Color.red,
        Color.white,
        Color.yellow
    };
  public  string[] colorsName =
   {
        "чёрный",
        "синий",
        "серый",
        "зелёный",
        "розовый",
        "красный",
        "белый",
        "жёлтый"
    };
    
    public string GetColorName(bool _answer, int id)
    {
        return _answer ? colorsName[id] : colorsName[RandomColorId(id)];

    }
   public int RandomColorId()
    {
        return Random.Range(0, colors.Length);
    }
   public int RandomColorId(int id)
    {
        int res = Random.Range(0, colors.Length);
        if (res == id)
        {
            return RandomColorId(id);
        }
        return res;
    }
}
[System.Serializable]
public class Figure
{

    public Sprite sprite;

    public bool isSquare;
    public bool isCircle;
    public bool isTriangle; //треугольник
    public bool isRectangle;//Прямоугольник
    public bool isPentagon;//Пятиугольник
    public bool isHexagon; //Шестиугольника
    public bool isRhombus; //ромб

    public List<bool> isFigure = new List<bool>();

    string[] Name = new string[]
    {
        "квадрат",
        "круг",
        "треугольник",
        "прямоугольник",
        "пятиугольник",
        "шестиугольник",
        "ромб"
        
    };
    public void SetValue()
    {
        isFigure.Add(isSquare);
        isFigure.Add(isCircle);
        isFigure.Add(isTriangle);
        isFigure.Add(isRectangle);
        isFigure.Add(isPentagon);
        isFigure.Add(isHexagon);
        isFigure.Add(isRhombus);
    }
    public string Question(bool _answer) //Получить имя фигуры
    {
        int r = Rand(_answer);
        return Name[r];
    }
    int Rand(bool b)
    {
        int r = Random.Range(0, isFigure.Count);
        if (isFigure[r] == b)
        {
            return r;
        }
        return Rand(b);
    }
}
public class TempClass
{
    Dictionary<string, Color> colors = new Dictionary<string, Color>
    {
        {"black", Color.black },
        {"blue", Color.blue},
        {"cyan", Color.cyan},
        {"gray", Color.gray},
        {"green", Color.green},
        {"magenta", Color.magenta},
        {"red", Color.red},
        {"white", Color.white},
        {"yellow", Color.yellow}
    };
    Dictionary<string, string> colorsRu = new Dictionary<string, string>
    {
        {"black", "чёрный" },
        {"blue", "синий"},
        {"cyan", "сине-зелёный"},
        {"gray", "серый"},
        {"green", "зелёный"},
        {"magenta", "розовый"},
        {"red", "красный"},
        {"white", "белый"},
        {"yellow", "жёлтый"}
    };
    string[] colorsNameEn =
   {
        "black",
        "blue",
        "gray",
        "green",
        "magenta",
        "red",
        "white",
        "yellow"
    };
}
