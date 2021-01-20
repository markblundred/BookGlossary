using InterviewTestMvc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewTestMvc.DataServices
{
    public class ReviewDataService
    {
        private readonly string connectionString;

        public ReviewDataService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<ReviewModel> GetReviews(long bookId)
        {
            var reviewModels = new List<ReviewModel>();

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var dataSet = new DataSet();
                using var sqlCommand = new SqlCommand($"SELECT * FROM [dbo].[Review] WHERE [BookId]={ bookId }", sqlConnection);
                using var sqlAdapter = new SqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dataSet);

                foreach (DataTable table in dataSet.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        var review = new ReviewModel()
                        {
                            Id = Convert.ToInt64(row["Id"]),
                            Name = Convert.ToString(row["Name"]),
                            Rating = Convert.ToInt32(row["Rating"]),
                            Review = Convert.ToString(row["Review"]),
                            ReviewedOn = Convert.ToDateTime(row["ReviewedOn"])
                        };

                        reviewModels.Add(review);
                    }
                }
            }

            return reviewModels;
        }
    }
}
