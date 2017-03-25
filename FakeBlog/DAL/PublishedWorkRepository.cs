using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FakeBlog.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace FakeBlog.DAL
{
    public class PublishedWorkRepository : IPublishedWork
    {
        IDbConnection _blogConnection;

        public PublishedWorkRepository(IDbConnection blogConnection)
        {
            _blogConnection = blogConnection;
        }

        //add new published work
        #region Add New Published Work
        public void AddWork(string name, string body, ApplicationUser owner)
        {
            try
            {
                var addWorkCommand = _blogConnection.CreateCommand();
                addWorkCommand.CommandText = @"
                    INSERT INTO PublishedWork(Name, body, owner)
                    VALUES(@name, @owner, @body)
                ";
                var nameParameter = new SqlParameter("name", SqlDbType.VarChar);
                nameParameter.Value = name;
                addWorkCommand.Parameters.Add(nameParameter);

                var bodyParameter = new SqlParameter("body", SqlDbType.Int);
                bodyParameter.Value = body;
                addWorkCommand.Parameters.Add(bodyParameter);

                var ownerIdParameter = new SqlParameter("owner", SqlDbType.Int);
                ownerIdParameter.Value = owner.Id;
                addWorkCommand.Parameters.Add(ownerIdParameter);

                _blogConnection.Open();
                addWorkCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _blogConnection.Close();
            }
        }
        #endregion

        //Get published work
        #region Get Published Work
        public PublishedWork GetPublishedWork(int publishedWorkId)
        {
            try
            {
                var getPublishedWorkCommand = _blogConnection.CreateCommand();
                getPublishedWorkCommand.CommandText = @"
                    SELECT PublishedWorkId, Name, Body, DateCreated, IsDraft, Owner
                    FROM PublishedWork
                    WHERE PublishedWorkId = @publishedWorkId
                ";

                var publishedWorkIdParameter = new SqlParameter("publishedWorkId", SqlDbType.Int);
                publishedWorkIdParameter.Value = publishedWorkId;
                getPublishedWorkCommand.Parameters.Add(publishedWorkIdParameter);

                _blogConnection.Open();
                var reader = getPublishedWorkCommand.ExecuteReader();
                reader.Read();

                while (reader.Read())
                {
                    return new PublishedWork
                    {
                        PublishedWorkId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Body = reader.GetString(2),
                        DateCreated = reader.GetDateTime(3),
                        Owner = new ApplicationUser { Id = reader.GetString(5) },
                        IsDraft = reader.GetBoolean(4)
                    };
   
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _blogConnection.Close();
            }
            return null;
        }
        #endregion

        //Edit Blog Contents
        #region Edit Blog Contents
        public void EditBlog(int publishedWorkId, string editedBody)
        {
            try
            {
                var editBlogCommand = _blogConnection.CreateCommand();
                editBlogCommand.CommandText = @"
                    UPDATE PublishedWork
                    SET Body = @editedBody
                    WHERE publishedWorkId = @publishedWorkId
                ";
                var bodyParameter = new SqlParameter("editedBody", SqlDbType.VarChar);
                bodyParameter.Value = editedBody;
                editBlogCommand.Parameters.Add(bodyParameter);

                var publishedWorkIdParameter = new SqlParameter("publishedWorkId", SqlDbType.Int);
                publishedWorkIdParameter.Value = publishedWorkId;
                editBlogCommand.Parameters.Add(publishedWorkIdParameter);

                _blogConnection.Open();
                var rowsAffected = editBlogCommand.ExecuteNonQuery();

                if(rowsAffected != 1)
                {
                    throw new Exception($"query didnt work, {rowsAffected} rows were affected");
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _blogConnection.Close();
            }
        }
        #endregion

        //delete published work
        #region Delete Published Work
        public bool RemovePublishedWork(int publishedWorkId)
        {
            try
            {
                var removePublishedWorkCommand = _blogConnection.CreateCommand();
                removePublishedWorkCommand.CommandText = @"
                    DELETE
                    FROM PublishedWork
                    WHERE PublishedWorkId = @publishedWorkId
                ";

                removePublishedWorkCommand.Parameters.Add(new SqlParameter("publishedWorkId", SqlDbType.Int) { Value = publishedWorkId });

                _blogConnection.Open();
                removePublishedWorkCommand.ExecuteNonQuery();

                return true;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _blogConnection.Close();
            }

            return false;
        }
        #endregion

    }
}