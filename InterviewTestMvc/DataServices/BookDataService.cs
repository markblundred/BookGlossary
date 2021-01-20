using InterviewTestMvc.Models;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace InterviewTestMvc.DataServices
{
    public class BookDataService
    {
        private readonly string connectionString;

        public BookDataService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<BookModel> GetBooks()
        {
            var bookModels = new List<BookModel>();

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var bookData = LoadDataFromTable(sqlConnection, "[dbo].[Book]");
                var authorDataSet = LoadDataFromTable(sqlConnection, "[dbo].[Author]");
                var genreDataSet = LoadDataFromTable(sqlConnection, "[dbo].[Genre]");
                var bookGenreDataSet = LoadDataFromTable(sqlConnection, "[dbo].[BookGenre]");

                var authorModels = new List<AuthorModel>();
                var genreModels = new List<GenreModel>();

                foreach (DataTable table in authorDataSet.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        var authorModel = new AuthorModel
                        {
                            Id = Convert.ToInt64(row["Id"]),
                            Forename = Convert.ToString(row["Forename"]),
                            Surname = Convert.ToString(row["Surname"]),
                            Initials = Convert.ToString(row["Initials"])
                        };

                        authorModels.Add(authorModel);
                    }
                }

                foreach (DataTable table in bookData.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        var bookModel = new BookModel
                        {
                            Id = Convert.ToInt64(row["Id"]),
                            Title = Convert.ToString(row["Title"]),
                            FirstPublished = Convert.ToString(row["FirstPublished"]),
                            ISBN = Convert.ToString(row["ISBN"]),
                            Genres = new List<GenreModel>()
                        };

                        // Establish the one-to-many relationship with books and authors
                        var authorId = Convert.ToInt64(row["AuthorId"]);
                        bookModel.Author = authorModels.SingleOrDefault(x => x.Id == authorId);

                        sqlConnection.Open();
                        using (var sqlCommand = new SqlCommand($"SELECT COUNT(*) FROM [dbo].[Review] WHERE [BookId]={ bookModel.Id }", sqlConnection))
                        {
                            bookModel.NumberOfReviews = Convert.ToInt32(sqlCommand.ExecuteScalar());
                        }
                        sqlConnection.Close();

                        bookModels.Add(bookModel);
                    }
                }

                foreach (DataTable table in genreDataSet.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        var genreModel = new GenreModel
                        {
                            Id = Convert.ToInt64(row["Id"]),
                            Description = Convert.ToString(row["description"]),
                            Books = new List<BookModel>()
                        };

                        genreModels.Add(genreModel);
                    }
                }

                // Load the relationship between books and genres and pair them together

                foreach (DataTable table in bookGenreDataSet.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        var book = bookModels.SingleOrDefault(x => x.Id == Convert.ToInt64(row["BookId"]));
                        var genre = genreModels.SingleOrDefault(x => x.Id == Convert.ToInt64(row["GenreId"]));

                        if (genre != null && book != null)
                        {
                            book.Genres.Add(genre);
                            genre.Books.Add(book);
                        }
                    }
                }
            }

            return bookModels;
        }

        private DataSet LoadDataFromTable(SqlConnection sqlConnection, string tableName)
        {
            var result = new DataSet();
            using (var sqlCommand = new SqlCommand($"SELECT * FROM { tableName }", sqlConnection))
            {
                using var sqlAdapter = new SqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(result);
            }

            return result;
        }
    }
}
