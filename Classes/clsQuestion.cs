﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.SessionState;
using static Quizes_System.clsQuestion;

namespace Quizes_System
{
    public class clsQuestion
    {
        public enum enQuestionLevel { Easy = 1, Med = 2, Hard = 3, Mix = 4 };
        public enum enOperationType { Add = 1, Sub = 2, Mul = 3, Div = 4, mix = 5 };

        public enum enResult { right = 1, wrong = 2 };
        public int Num1 { set; get; }
        public int Num2 { set; get; }
        public double RightAnswer { set; get; }

        public enResult result;
        public enOperationType OperationType;
        public enQuestionLevel QuestionLevel;


        private static Random _Random = new Random();

        public clsQuestion(int num1, int num2, enQuestionLevel QuestionLevel, enOperationType OperationType)
        {
            this.Num1 = num1;
            this.Num2 = num2;
            this.QuestionLevel = QuestionLevel;
            this.OperationType = OperationType;
            this.RightAnswer = CalculateAnswer();
        }

        private double CalculateAnswer()
        {
            switch (OperationType)
            {
                case enOperationType.Add:
                    return (Num1 + Num2);

                case enOperationType.Sub:
                    return (Num1 - Num2);

                case enOperationType.Mul:
                    return (Num1 * Num2);

                case enOperationType.Div:
                    return Num2 != 0 ? Math.Round(((double)Num1 / Num2) ,2) : 0;
            }
            return 0;
        }

        public static enOperationType GetRandomOperationType() => (enOperationType)_Random.Next(1, 4);

        private static clsQuestion GenerateEachQuestion(int From, int To, enQuestionLevel QuestionLevel, enOperationType OperationType)
        {
            int Number1 = _Random.Next(From, To);
            int Number2 = _Random.Next(From, To);
            return new clsQuestion(Number1, Number2, QuestionLevel, OperationType);
        }

        public static clsQuestion GenerateQuestion(enQuestionLevel QuestionLevel, enOperationType OperationType)
        {

            if (OperationType == enOperationType.mix)
            {
                OperationType = GetRandomOperationType();
            }
            switch (QuestionLevel)
            {
                case enQuestionLevel.Easy:
                    return GenerateEachQuestion(1, 10, QuestionLevel, OperationType);

                case enQuestionLevel.Med:
                    return GenerateEachQuestion(10, 100, QuestionLevel, OperationType);

                case enQuestionLevel.Hard:
                    return GenerateEachQuestion(100, 200, QuestionLevel, OperationType);

                case enQuestionLevel.Mix:
                    return GenerateEachQuestion(200, 500, QuestionLevel, OperationType);
            }
            return null;
        }

        public static char GetOperationName(enOperationType OperationType)
        {
            switch (OperationType)
            {
                case enOperationType.Add:
                    return '+';

                case enOperationType.Sub:
                    return '-';

                case enOperationType.Mul:
                    return '*';

                case enOperationType.Div:
                    return '/';
            }
            return '+';

        }

        public static enOperationType GetOperationType(string Name)
        {
            switch (Name)
            {
                case "Addition":
                    return enOperationType.Add;

                case "Subtraction":
                    return enOperationType.Sub;

                case "Multiplication":
                    return enOperationType.Mul;

                case "Division":
                    return enOperationType.Div;
            }
            return enOperationType.mix;
        }

        public static enQuestionLevel GetLevel(string Name)
        {
            switch (Name)
            {
                case "Easy":
                    return enQuestionLevel.Easy;

                case "Medium":
                    return enQuestionLevel.Med;

                case "Hard":
                    return enQuestionLevel.Hard;
            }
            return enQuestionLevel.Mix;
        }

        public static List<double> GenerateOptions(double RightAnswer)
        {
            // Here we use HashSet because we want to prevent creating two similar options
            var options = new HashSet<double> { RightAnswer };

            while (options.Count < 4)
            {
                options.Add(_Random.Next(((int)RightAnswer - 10) , (int)(RightAnswer + 10)));
            }
            options.Add(Math.Round((RightAnswer),2));

            return options.OrderBy(x => _Random.Next()).ToList();
        }



    }

}
