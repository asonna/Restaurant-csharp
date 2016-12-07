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
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurant_test; Integrated Security= SSPI;";
    }

    [Fact]
    public void Test_RestaurantsEmptyAtFirst_0()
    {
      int result = Restaurant.GetAll().Count;
      Assert.Equal(0, result)

    }


  }
}
