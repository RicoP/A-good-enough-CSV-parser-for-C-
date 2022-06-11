# A CSV for C# that is simply good enough

## How to use

```C#
    var csv = CSV.Parse(path, ';');

    for (int line = 0; line != csv.Lines; ++line) {
      if (csv["category"][line] != "knowledge") continue;

      var question = csv["question"][line];
      var answer1 = csv["answer_1"][line];
      var answer2 = csv["answer_2"][line];
      var answer3 = csv["answer_3"][line];
      var answer4 = csv["answer_4"][line]; 
      var trivia = csv["trivia"][line]; 

      //...
    }
```

