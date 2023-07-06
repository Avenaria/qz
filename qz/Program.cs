using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace qz
{
    internal class Program
    {
        public class Question
        {
            public int QuestionNumber { get; set; }
            public string Description { get; set; }
            public string Answer { get; set; }
        }
        public class Quiz
        {
            public void AddQuestion(Question q)
            {
                q.QuestionNumber = Questions.Count;
                q.Description += " (" + (q.QuestionNumber + 1) + ")".ToString();
                Questions.Add(q);

                if (Questions.Count > QuizLength)
                {
                    Console.WriteLine("You have too many questions, please change your QuestionLength.");
                }
            }
            public void ReadQuestions(int customIndex = 0)
            {
                if (customIndex == QuizLength)
                {
                    Console.WriteLine(Environment.NewLine + "Quiz has finished.");
                    for (int x = 0; x < Questions.Count; x++)
                        Console.WriteLine($"Question {x + 1} - {Questions[x].Description} | Answer - {Questions[x].Answer}");
                    Console.WriteLine($"You got a score of {Score}/{QuizLength}");
                }
                else
                    while (customIndex < Questions.Count)
                    {
                        Timer timer = new Timer(QuestionTimeLimit);
                        timer.Start();
                        timer.Elapsed += (sender, e) =>
                        {
                            Console.WriteLine("Time limit over...");
                            timer.Stop();
                            ReadQuestions(customIndex + 1);
                        };

                        Console.WriteLine(Questions[customIndex].Description);
                        string input = Console.ReadLine();
                        if (input.ToLower().Trim() == Questions[customIndex].Answer.ToLower().Trim())
                        {
                            Console.WriteLine("Correct answer");
                            timer.Stop();
                            customIndex++;
                            Score++;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect answer");
                            break;
                        }
                    }
            }
            public List<Question> Questions = new List<Question>();
            public int Score { get; set; }
            public int QuizLength { get; set; }
            public bool HasTimeLimit { get; set; }

            private int _QuestionTimeLimit;
            public int QuestionTimeLimit
            {
                get
                {
                    return _QuestionTimeLimit;
                }
                set
                {
                    if (HasTimeLimit == true)
                        _QuestionTimeLimit = value;
                    else
                        Console.WriteLine("You haven't assigned a time limit value.");
                }
            }
            static class Program
            {
                static void Main(string[] args)
                {
                    Quiz q = new Quiz()
                    {
                        HasTimeLimit = true,
                        QuizLength = 4,
                        QuestionTimeLimit = 30000,
                    };
                    q.AddQuestion(new Question() { Description = "What is the capital of Canada?", Answer = "Ottawa", });
                    q.AddQuestion(new Question() { Description = "What is the capital of Australia?", Answer = "Canberra", });
                    q.AddQuestion(new Question() { Description = "What is the capital of New Zealand?", Answer = "Wellington", });
                    q.AddQuestion(new Question() { Description = "What is the capital of Mexico?", Answer = "Mexico City", });
                    q.ReadQuestions();

                    Console.ReadLine();
                }
            }

        }
    }
}
