using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurant
{
  public class RestaurantTest : IDisposable
  {
    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurants_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_RestaurantsEmptyAtFirst_0()
    {
      int result = Restaurant.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      //Arrange, Act
      Restaurant firstRestaurant = new Restaurant("Killer burger", "desc1");
      Restaurant secondRestaurant = new Restaurant("Killer burger", "desc1");

      //Assert
      Assert.Equal(firstRestaurant, secondRestaurant);
    }

    [Fact]
    public void Test_Save_SavesRestaurantToDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Killer burger", "desc1");
      testRestaurant.Save();

      //Act
      List<Restaurant> result = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant>{testRestaurant};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToRestaurantObject()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Departure", "desc2");
      testRestaurant.Save();

      //Act
      Restaurant savedRestaurant = Restaurant.GetAll()[0];

      int result = savedRestaurant.GetId();
      int testId = testRestaurant.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsRestaurantInDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Departure", "desc2");
      testRestaurant.Save();

      //Act
      Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());

      //Assert
      Assert.Equal(testRestaurant, foundRestaurant);
    }


    public void Dispose()
    {
      Restaurant.DeleteAll();
      // Restaurant.DeleteAll();
    }
  }
}
