using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BestRestaurant
{
  public class Restaurant
  {
    private string _name; //Restaurant Name
    private string _description;
    private int _id; //Restaurant ID

    public Restaurant(string name, string description, int Id = 0)
    {
      _name = name;
      _description = description;
      _id = Id;
    }

    public override bool Equals(System.Object otherRestaurant)
    {
      if (!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool idEquality = this.GetId() == newRestaurant.GetId();
        bool nameEquality = this.GetName() ==newRestaurant.GetName();
        return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
       return this._description.GetHashCode();
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public string GetDescription()
    {
      return _description;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO restaurants (name, description) OUTPUT INSERTED.id VALUES (@RestaurantName, @RestaurantDescription);", conn);

      SqlParameter[] insertParameters = new SqlParameter[]
      {
        new SqlParameter("@RestaurantName", _name),
        new SqlParameter("@RestaurantDescription", _description)
      };
      cmd.Parameters.AddRange(insertParameters);
      SqlDataReader rdr = cmd.ExecuteReader();

      // SqlParameter nameParameter = new SqlParameter("@RestaurantName", this.GetName());
      // // nameParameter.ParameterName = "@RestaurantName";
      // // nameParameter.Value = this.GetName;
      // SqlParameter descriptionParameter = new SqlParameter("@RestaurantDescription", this.GetDescription());
      // cmd.Parameters.Add(nameParameter);
      // cmd.Parameters.Add(descriptionParameter);
      // SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static Restaurant Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE id = @RestaurantId;", conn);

      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@RestaurantId";
      restaurantIdParameter.Value = id.ToString();
      cmd.Parameters.Add(restaurantIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundRestaurantId = 0;
      string foundRestaurantName = null;
      string foundRestaurantDescription = null;

      while(rdr.Read())
      {
        foundRestaurantId = rdr.GetInt32(0);
        foundRestaurantName = rdr.GetString(1);
        foundRestaurantDescription = rdr.GetString(2);
      }
      Restaurant foundRestaurant = new Restaurant(foundRestaurantName, foundRestaurantDescription, foundRestaurantId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundRestaurant;
    }

    public static List<Restaurant> GetAll()
    {
      List<Restaurant> allRestaurants = new List<Restaurant>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        string restaurantDescription = rdr.GetString(2);
        Restaurant newRestaurant = new Restaurant(restaurantName, restaurantDescription, restaurantId);
        allRestaurants.Add(newRestaurant);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }

      return allRestaurants;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
