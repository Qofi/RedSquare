using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHelper : MonoBehaviour {

    public Text question_txt;

    public Image figure_img;
    public Image[] points_img;
    Queue<Image> queue = new Queue<Image>();

    public bool answer; //Верный ответ

    public int maxPoints;
    public int idPoints = 0;
    public int currentPoints = 0;

     Color[] colors =
     {
        Color.black,
        Color.blue,
        Color.cyan,
        Color.gray,
        Color.green,
        Color.magenta,
        Color.red,
        Color.white,
        Color.yellow    
    };
     string[] colorsNameEn =
    {
        "black",
        "blue",
        "cyan",
        "gray",
        "green",
        "magenta",
        "red",
        "white",
        "yellow"
    };
     string[] colorsNameRu =
    {
        "чёрный",
        "синий",
        "сине-зелёный",
        "серый",
        "зелёный",
        "розовый",
        "красный",
        "белый",
        "жёлтый"
    };

    public string[] colorsName;

    private void Start()
    {
        colorsName = colorsNameRu;
        ClearPoints();
        CreateQuestion();
    }
    void ClearPoints()
    {
        maxPoints = maxPoints >= points_img.Length ? points_img.Length : maxPoints;
        for (int i = 0; i < maxPoints; i++)
        {
            points_img[i].gameObject.SetActive(true);
            queue.Enqueue(points_img[i]);
        }
    }
    
    public void ClickButton(bool _answer)
    {
        bool res = _answer == answer;
        if (res)
        {
            print("Yes");
        }
        else
        {
            print("No");
        }
        Image img = queue.Dequeue();
        img.color = res ? Color.green : Color.red;
        CreateQuestion();
    }
    int GetRandomID()
    {
        return Random.Range(0, colors.Length);
    }
    void CreateQuestion()
    {
        int idColorText = GetRandomID();
        int idColorFigure = GetRandomID();
        int idColorExiger = GetRandomID();

        if (Random.value > 0.5f)
        {
            idColorFigure = idColorExiger;
        }

        question_txt.text = "Этот квадрат "  + colorsName[idColorExiger] + " ?";
        question_txt.color = colors[idColorText];

        figure_img.color = colors[idColorFigure];

        answer = idColorFigure == idColorExiger;
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
}
