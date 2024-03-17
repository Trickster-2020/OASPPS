using ENotes.Data;
using ENotes.Entities;
using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using Extreme.Mathematics;
using Microsoft.EntityFrameworkCore;


using Score = ENotes.Entities.Score;


namespace ENotes.Repository
{
    public class ScoreRepository : IScoreServices
    {
        private readonly EnotesDbContext enotesDbContext;
        
        public ScoreRepository(EnotesDbContext enotesDbContext)
        {
            this.enotesDbContext = enotesDbContext;
        }

        public async Task<bool> Delete_Score(string Id)
        {
            Score? score = await this.enotesDbContext.Scores.FindAsync(Id);
            if (score is not null)
            {
                this.enotesDbContext.Scores.Remove(score);
                await this.enotesDbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<ScoreDto> Get_Score(string id)
        {
            Score? score = await this.enotesDbContext.Scores.FindAsync(id);
            ScoreDto scoredto = new ScoreDto();
            
            scoredto.Student_ID = score.Student_ID;
            Student? student = await this.enotesDbContext.Students.FindAsync(score.Student_ID);
            if (student is not null)
            {
                scoredto.Student_Email = student.Email;
            }
            scoredto.Quiz = score.Quiz;
            scoredto.Assignment01 = score.Assignment01;
            scoredto.FirstTerm_Exam = score.FirstTerm_Exam;
            scoredto.Assignment02 = score.Assignment02;
            scoredto.Assignment03 = score.Assignment03;
            scoredto.SecondTerm_Exam = score.SecondTerm_Exam;
            scoredto.Final_Grade= score.Final_Grade;
            scoredto.Class = score.Class;
            return scoredto;
        }

        public async Task<List<ScoreDto>> Get_Scores()
        {
            List<Score>? scores = await this.enotesDbContext.Scores.ToListAsync();
            List<ScoreDto>? scoreDtos = new List<ScoreDto>();
            foreach(Score score in scores)
            {
                ScoreDto scoredto = new ScoreDto();

                scoredto.Student_ID = score.Student_ID;
                Student? student = await this.enotesDbContext.Students.FindAsync(score.Student_ID);
                if (student is not null)
                {
                    scoredto.Student_Email = student.Email;
                }
                scoredto.Quiz = score.Quiz;
                scoredto.Assignment01 = score.Assignment01;
                scoredto.FirstTerm_Exam = score.FirstTerm_Exam;
                scoredto.Assignment02 = score.Assignment02;
                scoredto.Assignment03 = score.Assignment03;
                scoredto.SecondTerm_Exam = score.SecondTerm_Exam;
                scoredto.Final_Grade = score.Final_Grade;
                scoredto.Class = score.Class;
                scoreDtos.Add(scoredto);
            }
            
            return scoreDtos;
        }

        public string NaiveBayes(ScoreDto score,List<Score> scores)
        {

            
                int good = 0;
                int weak = 0;

                //Initializing variables for sum of quizData
                int G_quizData = 0;
                int W_quizData = 0;

                //Initializing variables for assignment1
                int G_assignment1 = 0;
                int W_assignment1 = 0;

                //Initializing variables for firsttermdata
                int G_firsttermdata = 0;
                int W_firsttermdata = 0;

                //Initializing variables for assignment2
                int G_assignment2 = 0;
                int W_assignment2 = 0;

                //Initializing variables for assignment3
                int G_assignment3 = 0;
                int W_assignment3 = 0;

                //Initializing variables for secondtermdata
                int G_secondtermdata =0;
                int W_secondtermdata = 0;

                //Initializing variables for counting total data
                int total_data = scores.Count;
                
                foreach (var data in scores)
                {                   
                   if (data.Class == "G")
                    {
                        G_quizData += data.Quiz;
                        G_assignment1 += data.Assignment01;
                        G_firsttermdata += data.FirstTerm_Exam;
                        G_assignment2 += data.Assignment02;
                        G_assignment3 += data.Assignment03;
                        G_secondtermdata += data.SecondTerm_Exam;
                        good++;
                    }
                    else
                    {
                        W_quizData += data.Quiz;
                        W_assignment1 += data.Assignment01;
                        W_firsttermdata += data.FirstTerm_Exam;
                        W_assignment2 += data.Assignment02;
                        W_assignment3 += data.Assignment03;
                        W_secondtermdata += data.SecondTerm_Exam;
                        weak++;
                    }        
                }
                //Probability of good in target label
                double prob_G = Convert.ToDouble(good) / Convert.ToDouble(total_data);

                //Probability of weak in target label
                double prob_W = Convert.ToDouble(weak) / Convert.ToDouble(total_data);

                //Mean of Quiz
                double G_mean_Quiz = Convert.ToDouble(G_quizData) / Convert.ToDouble(good);
                double W_mean_Quiz = Convert.ToDouble(W_quizData) / Convert.ToDouble(weak);
                
                //Mean of Assignment1
                double G_mean_assignment1 = Convert.ToDouble(G_assignment1) / Convert.ToDouble(good);  
                double W_mean_assignment1 = Convert.ToDouble(W_assignment1) / Convert.ToDouble(weak);

                //Mean of firstterm
                double G_mean_firstterm = Convert.ToDouble(G_firsttermdata)/ Convert.ToDouble(good);  
                double W_mean_firstterm = Convert.ToDouble(W_firsttermdata) / Convert.ToDouble(weak);  

                //Mean of assignment2
                double G_mean_assignment2 = Convert.ToDouble(G_assignment2) / Convert.ToDouble(good); 
                double W_mean_assignment2 = Convert.ToDouble(W_assignment2) / Convert.ToDouble(weak); 

                //Mean of assignment3
                double G_mean_assignment3 = Convert.ToDouble(G_assignment3) / Convert.ToDouble(good);
                double W_mean_assignment3 = Convert.ToDouble(W_assignment3) / Convert.ToDouble(weak);

                //Mean of secondterm
                double G_mean_secondterm = Convert.ToDouble(G_secondtermdata) / Convert.ToDouble(good);
                double W_mean_secondterm = Convert.ToDouble(W_secondtermdata) / Convert.ToDouble(weak);


                //Initialization of Variables for calculating variance
                double temp = 0;

                //Initialization of Variables for calculating variance of quizdata
                double G_var_Quiz = 0;
                double W_var_Quiz = 0;

                //Initialization of Variables for calculating variance of assignment01
                double G_var_assignment01 = 0;
                double W_var_assignment01 = 0;

                //Initialization of Variables for calculating variance of assignment02
                double G_var_assignment02 = 0;
                double W_var_assignment02 = 0;

                //Initialization of Variables for calculating variance of assignment03
                double G_var_assignment03 = 0;
                double W_var_assignment03 = 0;

                //Initialization of Variables for calculating variance of firstterm
                double G_var_firstterm = 0;
                double W_var_firstterm = 0;

                //Initialization of Variables for calculating variance of secondterm
                double G_var_secondterm = 0;
                double W_var_secondterm = 0;

                double posterior_G = 1;
                double posterior_W = 1;

            foreach (var data in scores)
            {
                if (data.Class == "G")
                {
                    //Variance of Quiz
                    temp = Math.Pow(data.Quiz - G_mean_Quiz, 2) / (good - 1);
                    G_var_Quiz += temp;

                    //Variance of Assignment01
                    temp = Math.Pow(data.Assignment01 - G_mean_assignment1, 2) / (good - 1);
                    G_var_assignment01 += temp;

                    //Variance of Assignment02
                    temp = Math.Pow(data.Assignment02 - G_mean_assignment2, 2) / (good - 1);
                    G_var_assignment02 += temp;

                    //Variance of Assignment03
                    temp = Math.Pow(data.Assignment03 - G_mean_assignment3, 2) / (good - 1);
                    G_var_assignment03 += temp;

                    //Variance of Firstterm
                    temp = Math.Pow(data.FirstTerm_Exam - G_mean_firstterm, 2) / (good - 1);
                    G_var_firstterm += temp;

                    //Variance of Secondterm
                    temp = Math.Pow(data.SecondTerm_Exam - G_mean_secondterm, 2) / (good - 1);
                    G_var_secondterm += temp;

                }
                else
                {
                    temp = Math.Pow(data.Quiz - W_mean_Quiz, 2) / (weak - 1);
                    W_var_Quiz += temp;

                    //Variance of Assignment01
                    temp = Math.Pow(data.Assignment01 - W_mean_assignment1, 2) / (weak - 1);
                    W_var_assignment01 += temp;

                    //Variance of Assignment02
                    temp = Math.Pow(data.Assignment02 - W_mean_assignment2, 2) / (weak - 1);
                    W_var_assignment02 += temp;

                    //Variance of Assignment03
                    temp = Math.Pow(data.Assignment03 - W_mean_assignment3, 2) / (weak - 1);
                    W_var_assignment03 += temp;

                    //Variance of Firstterm
                    temp = Math.Pow(data.FirstTerm_Exam - W_mean_firstterm, 2) / (weak - 1);
                    W_var_firstterm += temp;

                    //Variance of Secondterm
                    temp = Math.Pow(data.SecondTerm_Exam - W_mean_secondterm, 2) / (weak - 1);
                    W_var_secondterm += temp;
                }

                //calculating probability(Quiz/Good)
                double temp1 = 1 / Math.Sqrt(2 * Math.PI * G_var_Quiz);
                temp1 = temp1 * Math.Exp(-Math.Pow(score.Quiz - G_mean_Quiz, 2) / (2 * G_var_Quiz));
                posterior_G = prob_G * temp1;

                //calculating probability(assignment1/Good)
                temp1 = 0;
                temp1 = 1 / Math.Sqrt(2 * Math.PI * G_var_assignment01);
                temp1 = temp1 * Math.Exp(-Math.Pow(score.Assignment01 - G_mean_assignment1, 2) / (2 * G_var_assignment01));
                posterior_G *= temp1;

                //calculating probability(firstterm/Good)
                temp1 = 0;
                temp1 = 1 / Math.Sqrt(2 * Math.PI * G_var_firstterm);
                temp1 = temp1 * Math.Exp(-Math.Pow(score.Assignment01 - G_mean_firstterm, 2) / (2 * G_var_firstterm));
                posterior_G *= temp1;

                //calculating probability(assignment2/Good)
                temp1 = 0;
                temp1 = 1 / Math.Sqrt(2 * Math.PI * G_var_assignment02);
                temp1 = temp1 * Math.Exp(-Math.Pow(score.Assignment02 - G_mean_assignment2, 2) / (2 * G_var_assignment02));
                posterior_G *= temp1;

                //calculating probability(assignment3/Good)
                temp1 = 0;
                temp1 = 1 / Math.Sqrt(2 * Math.PI * G_var_assignment03);
                temp1 = temp1 * Math.Exp(-Math.Pow(score.Assignment03 - G_mean_assignment3, 2) / (2 * G_var_assignment03));
                posterior_G *= temp1;

                //calculating probability(secondterm/Good)
                temp1 = 0;
                temp1 = 1 / Math.Sqrt(2 * Math.PI * G_var_secondterm);
                temp1 = temp1 * Math.Exp(-Math.Pow(score.SecondTerm_Exam - G_mean_secondterm, 2) / (2 * G_var_secondterm));
                posterior_G *= temp1;






                //Calculating posterion W
                //calculating probability(Quiz/Weak)
                double temp2 = 1 / Math.Sqrt(2 * Math.PI * W_var_Quiz);
                temp2 = temp2 * Math.Exp(-Math.Pow(score.Quiz - W_mean_Quiz, 2) / (2 * W_var_Quiz));
                posterior_W = prob_G * temp2;

                //calculating probability(assignment1/Weak)
                temp2 = 0;
                temp2 = 1 / Math.Sqrt(2 * Math.PI * W_var_assignment01);
                temp2 = temp2 * Math.Exp(-Math.Pow(score.Assignment01 - W_mean_assignment1, 2) / (2 * W_var_assignment01));
                posterior_W *= temp2;

                //calculating probability(firstterm/Weak)
                temp2 = 0;
                temp2 = 1 / Math.Sqrt(2 * Math.PI * W_var_firstterm);
                temp2 = temp2 * Math.Exp(-Math.Pow(score.Assignment01 - W_mean_firstterm, 2) / (2 * W_var_firstterm));
                posterior_W *= temp2;

                //calculating probability(assignment2/Weak)
                temp2 = 0;
                temp2 = 1 / Math.Sqrt(2 * Math.PI * W_var_assignment02);
                temp2 = temp2 * Math.Exp(-Math.Pow(score.Assignment02 - W_mean_assignment2, 2) / (2 * W_var_assignment02));
                posterior_W *= temp2;

                //calculating probability(assignment3/Good)
                temp2 = 0;
                temp2 = 1 / Math.Sqrt(2 * Math.PI * W_var_assignment03);
                temp2 = temp2 * Math.Exp(-Math.Pow(score.Assignment03 - W_mean_assignment3, 2) / (2 * W_var_assignment03));
                posterior_W *= temp2;

                //calculating probability(secondterm/Weak)
                temp2 = 0;
                temp2 = 1 / Math.Sqrt(2 * Math.PI * W_var_secondterm);
                temp2 = temp2 * Math.Exp(-Math.Pow(score.SecondTerm_Exam - W_mean_secondterm, 2) / (2 * W_var_secondterm));
                posterior_W *= temp2;
            }

            if(posterior_G >= posterior_W)
            {
                return "G";
            }
            else
            {
                return "W";
            } 
        }

        public async Task<bool> Post_Score(ScoreDto score)
        {
            score.Class = null;
            Score? scoredto = new Score();
            List<Score>? scores = await this.enotesDbContext.Scores.ToListAsync();

            scoredto.Student_ID = score.Student_ID;
            scoredto.Quiz = score.Quiz;
            scoredto.Assignment01 = score.Assignment01;
            scoredto.FirstTerm_Exam = score.FirstTerm_Exam;
            scoredto.Assignment02 = score.Assignment02;
            scoredto.Assignment03 = score.Assignment03;
            scoredto.SecondTerm_Exam = score.SecondTerm_Exam;
            scoredto.Final_Grade = MultipleLinearRegression(scores, score);
            scoredto.Class = NaiveBayes(score,scores);
            
            enotesDbContext.Scores.Add(scoredto);
            await enotesDbContext.SaveChangesAsync();
            return true;

        }

        public async Task<bool> Update_Score(ScoreDto score)
        {
            Score? scoredto = await enotesDbContext.Scores.FindAsync(score.Student_ID);


            scoredto.Student_ID = score.Student_ID;
            scoredto.Quiz = score.Quiz;
            scoredto.Assignment01 = score.Assignment01;
            scoredto.FirstTerm_Exam = score.FirstTerm_Exam;
            scoredto.Assignment02 = score.Assignment02;
            scoredto.Assignment03 = score.Assignment03;
            scoredto.SecondTerm_Exam = score.SecondTerm_Exam;
            scoredto.Final_Grade= score.Final_Grade;
            scoredto.Class = score.Class;

            
            await enotesDbContext.SaveChangesAsync();
            return true;
        }

        public int MultipleLinearRegression(List<Score> scores,ScoreDto scoreDto)   
        {
            int row = scores.Count();
            int column = 7;


            double[,] y = new double[row, 1];
            double[,] x = new double[row, column];

            int i = 0;
            foreach (Score score in scores)
            {              
                    y[i, 0] = score.Final_Grade;
                    x[i, 0] = 1;
                    x[i, 1] = score.Quiz;
                    x[i, 2] = score.Assignment01;
                    x[i, 3] = score.FirstTerm_Exam;
                    x[i, 4] = score.Assignment02;
                    x[i, 5] = score.Assignment03;
                    x[i, 6] = score.SecondTerm_Exam;
                i++;
            }
            var X = Matrix.Create(x);
            var Y = Matrix.Create(y);
            var XT = X.Transpose();
            var XT_mul_X = XT.Multiply(X);
            var XT_mul_X_In = XT_mul_X.GetInverse();
            var XT_mul_X_In_mul_XT = XT_mul_X_In.Multiply(XT);
            var C = XT_mul_X_In_mul_XT.Multiply(Y);
            var a = C.ToArray();

            var ans = a[0] + (a[1] * scoreDto.Quiz) + (a[2] * scoreDto.Assignment01) + (a[3] * scoreDto.FirstTerm_Exam) + (a[4] * scoreDto.Assignment02) + (a[5] * scoreDto.Assignment03) + (a[6] * scoreDto.SecondTerm_Exam);
            return Convert.ToInt32(ans);
        }
    }
}
